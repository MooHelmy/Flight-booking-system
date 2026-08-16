public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; } // جاي من الـ Claims
}
// UserResponse  // ده وظيفته تسمح لـ Client بإرجاع User من الـ Claims