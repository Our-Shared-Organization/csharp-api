namespace whatever_api.Model;

public partial class BookingAddRequest
{
    public string? BookingUserLogin { get; set; }

    public int? BookingServiceId { get; set; }

    public DateTime BookingStart { get; set; }

    public DateTime BookingFinish { get; set; }

    public string BookingStatus { get; set; } = null!;
}