using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class Category
{
    public int Categoryid { get; set; }

    public string Categoryname { get; set; } = null!;

    public string? Categorydescription { get; set; }

    public bool? Categorystatus { get; set; }

    public virtual ICollection<MasterCategory> MasterCategories { get; set; } = new List<MasterCategory>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
