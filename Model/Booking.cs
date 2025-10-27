using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class Booking
{
    public int Bookingid { get; set; }

    public string? Bookinguserlogin { get; set; }

    public int? Bookingserviceid { get; set; }

    public string? Bookingmasterlogin { get; set; }

    public DateTime Bookingstart { get; set; }

    public DateTime? Bookingfinish { get; set; }

    public Bookingstatus Bookingstatus { get; set; }

    public DateTime Bookingbookedat { get; set; }

    public virtual User? BookingmasterloginNavigation { get; set; }

    public virtual Service? Bookingservice { get; set; }

    public virtual User? BookinguserloginNavigation { get; set; }
}
