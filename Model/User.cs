using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class User
{
    public string UserLogin { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserSurname { get; set; } = null!;

    public string UserPhone { get; set; } = null!;

    public string UserSex { get; set; } = null!;

    public int UserRoleId { get; set; }

    public string? UserPassword { get; set; }

    public bool? UserStatus { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Master> Masters { get; set; } = new List<Master>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual Role UserRole { get; set; } = null!;
}
