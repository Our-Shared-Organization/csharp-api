using System;
using System.Collections.Generic;

namespace spa_API.Model;

public partial class Rating
{
    public int Ratingid { get; set; }

    public string Ratinguserlogin { get; set; } = null!;

    public string Ratingmasterlogin { get; set; } = null!;

    public string Ratingtext { get; set; } = null!;

    public int Ratingstars { get; set; }

    public virtual User RatingmasterloginNavigation { get; set; } = null!;

    public virtual User RatinguserloginNavigation { get; set; } = null!;
}
