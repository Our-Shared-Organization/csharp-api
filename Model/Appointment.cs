using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int ClientId { get; set; }

    public int ServiceId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public DateTime EndTime { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User Client { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
