using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class MasterService
{
    public int Msid { get; set; }

    public int Msmasterid { get; set; }

    public int Msserviceid { get; set; }

    public virtual Master Msmaster { get; set; } = null!;

    public virtual Service Msservice { get; set; } = null!;
}
