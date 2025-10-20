using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class Service
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string? ServiceDescription { get; set; }

    /// <summary>
    /// В минутах
    /// </summary>
    public int ServiceDuration { get; set; }

    public decimal ServicePrice { get; set; }

    public int ServiceCategoryId { get; set; }

    public bool? ServiceStatus { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<MasterService> MasterServices { get; set; } = new List<MasterService>();

    public virtual Category ServiceCategory { get; set; } = null!;
}
