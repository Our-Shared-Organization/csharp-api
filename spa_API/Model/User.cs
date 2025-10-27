using System;
using System.Collections.Generic;

namespace spa_API.Model;

public partial class User
{
    public string Userlogin { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Usersurname { get; set; } = null!;

    public string Userphone { get; set; } = null!;

    public string Usersex { get; set; } = null!;

    public int Userroleid { get; set; }

    public string Userpassword { get; set; } = null!;

    public bool? Userstatus { get; set; }

    public string? Useremail { get; set; }

    public string? Userspecialization { get; set; }

    public int? Userexperience { get; set; }

    public virtual ICollection<Booking> BookingBookingmasterloginNavigations { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingBookinguserloginNavigations { get; set; } = new List<Booking>();

    public virtual ICollection<MasterCategory> MasterCategories { get; set; } = new List<MasterCategory>();

    public virtual ICollection<Rating> RatingRatingmasterloginNavigations { get; set; } = new List<Rating>();

    public virtual ICollection<Rating> RatingRatinguserloginNavigations { get; set; } = new List<Rating>();

    public virtual Role Userrole { get; set; } = null!;
}
