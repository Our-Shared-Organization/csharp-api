namespace whatever_api.Model;

public class UserRegisterRequest
{
    public string Userlogin { get; set; }

    public string Username { get; set; }

    public string Usersurname { get; set; }

    public string Userphone { get; set; }

    public string Usersex { get; set; }

    public int Userroleid { get; set; }

    public string Userpassword { get; set; }

    public bool? Userstatus { get; set; }

    public string? Useremail { get; set; }

    public string? Userspecialization { get; set; }

    public int? Userexperience { get; set; }
}