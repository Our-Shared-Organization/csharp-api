using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class Rating
{
    public int Ratingid { get; set; }

    public string Ratinguserlogin { get; set; } = null!;

    public int Ratingmasterid { get; set; }

    public string Ratingtext { get; set; } = null!;

    public int Ratingstars { get; set; }

    public virtual Master Ratingmaster { get; set; } = null!;

    public virtual User RatinguserloginNavigation { get; set; } = null!;
}
