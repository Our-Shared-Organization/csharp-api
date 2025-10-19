namespace whatever_api.Model;

public partial class ServiceAddResponse
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string? ServiceDescription { get; set; }
    
    public int ServiceDuration { get; set; }

    public decimal ServicePrice { get; set; }

    public int ServiceCategoryId { get; set; }

    public bool? ServiceStatus { get; set; }
}
