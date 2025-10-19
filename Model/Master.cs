using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class Master
{
    public int MasterId { get; set; }

    public int MasterUserId { get; set; }

    public string? MasterSpecialization { get; set; }

    /// <summary>
    /// Опыт работы в месяцах
    /// </summary>
    public int? MasterExperience { get; set; }

    public decimal? MasterRating { get; set; }

    public bool? MasterStatus { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<MasterService> MasterServices { get; set; } = new List<MasterService>();

    public virtual User MasterUser { get; set; } = null!;
}
