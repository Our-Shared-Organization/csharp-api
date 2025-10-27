using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class Service
{
    public int Serviceid { get; set; }

    public string Servicename { get; set; } = null!;

    public string? Servicedescription { get; set; }

    public int Serviceduration { get; set; }

    public decimal Serviceprice { get; set; }

    public int Servicecategoryid { get; set; }

    public bool Servicestatus { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Category Servicecategory { get; set; } = null!;
}
