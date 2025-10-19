using System;
using System.Collections.Generic;

namespace whatever_api.Model;

/// <summary>
/// Сеанс
/// </summary>
public partial class Booking
{
    public int BookingId { get; set; }

    public int? BookingUserId { get; set; }

    public int? BookingServiceId { get; set; }

    public int? BookingMasterId { get; set; }

    public DateTime BookingStart { get; set; }

    public DateTime? BookingFinish { get; set; }

    public string BookingStatus { get; set; } = null!;

    public DateTime BookingBookedAt { get; set; }

    public virtual Master? BookingMaster { get; set; }

    public virtual Service? BookingService { get; set; }

    public virtual User? BookingUser { get; set; }
}
