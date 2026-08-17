using Microsoft.EntityFrameworkCore;

public class HoldServices(ApplicationDbContext context) : IHoldServices
{
    public async Task<ServicesResponse<HoldResponse>> CreateHoldAsync(CreateHoldRequest dto, Guid userId)
    {
        var hasActiveQueueTicket = await context.QueueTickets.AnyAsync(q =>
            q.UserId == userId && q.Status == QueueStatus.Active);

        if (!hasActiveQueueTicket)
        {
            return new ServicesResponse<HoldResponse>(false, "You must have an active queue turn to hold seats");
        }

        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var fareClass = await context.FarClasses.FindAsync(dto.FareClassId);

            if (fareClass == null)
            {
                return new ServicesResponse<HoldResponse>(false, "Fare class not found");
            }

            int available = fareClass.TotalSeats + fareClass.OverbookingLimit - fareClass.BookedSeats;

            if (available < dto.Quantity)
            {
                return new ServicesResponse<HoldResponse>(false, "Not enough seats available");
            }

            fareClass.BookedSeats += dto.Quantity;
            fareClass.RowVersion++;   // Fix #2: نزود التوكن يدويًا عشان الـ concurrency check يشتغل فعليًا

            var hold = new SeatHold
            {
                UserId = userId,
                FareClassId = dto.FareClassId,
                Quantity = dto.Quantity,
                Status = HoldStatus.Active,
                HeldAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5)
            };

            context.SeatHolds.Add(hold);

            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            var response = new HoldResponse
            {
                Id = hold.Id,
                Quantity = hold.Quantity,
                TotalPrice = hold.Quantity * fareClass.Price,
                SecondsRemaining = (int)(hold.ExpiresAt - DateTime.UtcNow).TotalSeconds,
                Status = hold.Status
            };

            return new ServicesResponse<HoldResponse>(true, "Seats held successfully", response);
        }
        catch (DbUpdateConcurrencyException)
        {
            await transaction.RollbackAsync();
            return new ServicesResponse<HoldResponse>(false, "Seats were just taken by someone else, please try again");
        }
    }

    public async Task<HoldResponse> GetByIdAsync(Guid holdId, Guid userId)
    {
        var hold = await context.SeatHolds
            .Include(h => h.FareClass)
            .FirstOrDefaultAsync(h => h.Id == holdId);

        if (hold == null)
        {
            throw new ItemNotFoundException("Hold not found");
        }

        if (hold.UserId != userId)
        {
            throw new UnauthorizedAccessException("This hold does not belong to you");
        }

        if (hold.Status == HoldStatus.Active && hold.ExpiresAt < DateTime.UtcNow)
        {
            // Fix #3: لما الحجز ينتهي طبيعي، لازم نرجّع المقاعد بالظبط
            // زي ما بيحصل في ReleaseAsync — مكانتش موجودة قبل كده
            hold.Status = HoldStatus.Expired;
            hold.RowVersion++;
            hold.FareClass.BookedSeats -= hold.Quantity;
            hold.FareClass.RowVersion++;
            await context.SaveChangesAsync();
        }

        return hold.HoldToResponseMapper();
    }

    public async Task<ServicesResponse> ReleaseAsync(Guid holdId, Guid userId)
    {
        using var transaction = await context.Database.BeginTransactionAsync();

        var hold = await context.SeatHolds
            .Include(h => h.FareClass)
            .FirstOrDefaultAsync(h => h.Id == holdId);

        if (hold == null)
        {
            return new ServicesResponse(false, "Hold not found");
        }

        if (hold.UserId != userId)
        {
            return new ServicesResponse(false, "This hold does not belong to you");
        }

        if (hold.Status != HoldStatus.Active)
        {
            return new ServicesResponse(false, "Only active holds can be released");
        }

        hold.Status = HoldStatus.Released;
        hold.RowVersion++;
        hold.FareClass.BookedSeats -= hold.Quantity;
        hold.FareClass.RowVersion++;

        var result = await context.SaveChangesAsync();
        await transaction.CommitAsync();

        return result > 0 ? new ServicesResponse(true, "Hold released successfully")
            : new ServicesResponse(false, "Failed to release hold");
    }
}
