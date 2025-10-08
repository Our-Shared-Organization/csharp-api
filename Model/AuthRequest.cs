namespace whatever_api.Model;

public partial class AuthRequest
{
    public string phone { get; set; } = null!;

    public string? password { get; set; }
}