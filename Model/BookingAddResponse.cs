namespace whatever_api.Model;

public partial class BookingAddResponse
{
    public int BookingId { get; set; }
    public string? BookingUserLogin { get; set; }
    public int? BookingServiceId { get; set; }
    public int? BookingMasterId { get; set; }
    public DateTime BookingStart { get; set; }
    public DateTime? BookingFinish { get; set; }
    public Bookingstatus BookingStatus { get; set; }
}