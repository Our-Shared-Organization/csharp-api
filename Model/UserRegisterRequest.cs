namespace whatever_api.Model;

public partial class UserRegisterRequest
{
    public string UserName { get; set; }

    public string UserSurname { get; set; }

    public string UserPhone { get; set; }

    public string UserSex { get; set; }
    
    public string UserPassword { get; set; }
}