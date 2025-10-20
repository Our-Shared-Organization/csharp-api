using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public class MasterGetResponse
{
    public int MasterId { get; set; }

    public string MasterUserLogin { get; set; }

    public string MasterSpecialization { get; set; }
    
    public int MasterExperience { get; set; }

    public bool MasterStatus { get; set; }
}