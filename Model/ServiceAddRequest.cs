namespace whatever_api.Model;

public partial class ServiceAddRequest
{
    public string serviceName { get; set; } = null!;

    public string? serviceDescription { get; set; }
    
    public int serviceDuration { get; set; }

    public decimal servicePrice { get; set; }

    public int serviceCategoryId { get; set; }

    public bool? serviceStatus { get; set; }
}