using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class MasterService
{
    public int MsId { get; set; }

    public int MsMasterId { get; set; }

    public int MsServiceId { get; set; }

    public virtual Master MsMaster { get; set; } = null!;

    public virtual Service MsService { get; set; } = null!;
}
