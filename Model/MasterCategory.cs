using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class MasterCategory
{
    public int Mcid { get; set; }

    public string Mcmasterlogin { get; set; } = null!;

    public int Mccategoryid { get; set; }

    public virtual Category Mccategory { get; set; } = null!;

    public virtual User McmasterloginNavigation { get; set; } = null!;
}
