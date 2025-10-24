using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class User
{
    public string Userlogin { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Usersurname { get; set; } = null!;

    public string Userphone { get; set; } = null!;
    
    public Usersex? Usersex { get; set; }

    public int Userroleid { get; set; }

    public string Userpassword { get; set; } = null!;

    public bool Userstatus { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Master> Masters { get; set; } = new List<Master>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual Role Userrole { get; set; } = null!;
}
