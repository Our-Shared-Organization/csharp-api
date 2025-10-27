using System;
using System.Collections.Generic;

namespace spa_API.Model;

public partial class Service
{
    public int Serviceid { get; set; }

    public string Servicename { get; set; } = null!;

    public string? Servicedescription { get; set; }

    public int Serviceduration { get; set; }

    public decimal Serviceprice { get; set; }

    public int Servicecategoryid { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Category Servicecategory { get; set; } = null!;
}
