namespace whatever_api.Model;

public partial class UserGetResponse
{
    public string UserLogin { get; set; }

    public string UserName { get; set; }

    public string UserSurname { get; set; }

    public string UserPhone { get; set; }

    public Usersex UserSex { get; set; }
    
    public int UserRoleId { get; set; }

    public bool UserStatus { get; set; }
}