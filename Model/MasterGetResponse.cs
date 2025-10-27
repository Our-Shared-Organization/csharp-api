using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public class MasterGetResponse
{
    public string MasterLogin { get; set; }

    public string MasterSpecialization { get; set; }
    
    public int MasterExperience { get; set; }

    public bool MasterStatus { get; set; }
}