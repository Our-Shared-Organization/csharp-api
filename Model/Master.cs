using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class Master
{
    public int Masterid { get; set; }

    public string Masteruserlogin { get; set; } = null!;

    public string? Masterspecialization { get; set; }

    public int Masterexperience { get; set; }

    public bool Masterstatus { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<MasterService> MasterServices { get; set; } = new List<MasterService>();

    public virtual User MasteruserloginNavigation { get; set; } = null!;

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
