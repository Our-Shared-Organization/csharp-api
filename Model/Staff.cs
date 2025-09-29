using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class Staff
{
    public int StaffId { get; set; }

    public string StaffName { get; set; } = null!;

    public string StaffSurname { get; set; } = null!;

    public string StaffEmail { get; set; } = null!;

    public string? StaffPhone { get; set; }

    public string? StaffRole { get; set; }

    public string StaffPassword { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
