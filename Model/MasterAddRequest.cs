namespace whatever_api.Model;

public partial class MasterAddRequest
{
    public int MasterUserId { get; set; }
    public string? MasterSpecialization { get; set; }
    public int? MasterExperience { get; set; }
}