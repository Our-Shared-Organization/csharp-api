using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class Master
{
    public int MasterId { get; set; }

    public string MasterUserLogin { get; set; } = null!;

    public string? MasterSpecialization { get; set; }

    /// <summary>
    /// Опыт работы в месяцах
    /// </summary>
    public int MasterExperience { get; set; }

    public bool? MasterStatus { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<MasterService> MasterServices { get; set; } = new List<MasterService>();

    public virtual User MasterUserLoginNavigation { get; set; } = null!;

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
