using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int ClientId { get; set; }

    public int StaffId { get; set; }

    public int ServiceId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public DateTime EndTime { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
