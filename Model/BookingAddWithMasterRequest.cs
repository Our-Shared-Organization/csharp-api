namespace whatever_api.Model;

public partial class BookingAddWithMasterRequest
{
    public int? BookingUserId { get; set; }
    public int? BookingServiceId { get; set; }
    public int? BookingMasterId { get; set; }
    public DateTime BookingStart { get; set; }
    public DateTime BookingFinish { get; set; }
    public string BookingStatus { get; set; } = null!;
}