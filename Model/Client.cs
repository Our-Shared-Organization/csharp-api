using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class Client
{
    public int ClientId { get; set; }

    public string ClientName { get; set; } = null!;

    public string ClientSurname { get; set; } = null!;

    public string ClientEmail { get; set; } = null!;

    public string? ClientPhone { get; set; }

    public string ClientPassword { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
