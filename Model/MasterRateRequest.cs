namespace whatever_api.Model;

public class MasterRateRequest
{
    public string RatingUserLogin { get; set; }
    public int RatingMasterId { get; set; }
    public string RatingText { get; set; }
    public int RatingStars  { get; set; }
}