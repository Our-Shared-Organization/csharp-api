namespace whatever_api.Model;

public partial class BookingAddResponse
{
    public int BookingId { get; set; }

    public int? BookingUserId { get; set; }

    public int? BookingServiceId { get; set; }

    public DateTime BookingStart { get; set; }

    public DateTime? BookingFinish { get; set; }

    public string BookingStatus { get; set; }
}