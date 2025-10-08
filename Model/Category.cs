using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? CategoryDescription { get; set; }

    public bool? CategoryStatus { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
