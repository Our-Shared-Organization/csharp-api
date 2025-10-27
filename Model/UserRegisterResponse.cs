namespace whatever_api.Model;

public partial class UserRegisterResponse
{
    public string UserLogin { get; set; }

    public string UserName { get; set; } = null!;

    public string UserSurname { get; set; } = null!;

    public string UserPhone { get; set; } = null!;

    public Usersex UserSex { get; set; }

    public int UserRoleId { get; set; }

    public string? UserPassword { get; set; }

    public bool UserStatus { get; set; }
}