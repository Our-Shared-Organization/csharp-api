using System;
using System.Collections.Generic;

namespace whatever_api.Model;

public partial class Rating
{
    public int RatingId { get; set; }

    public string RatingUserLogin { get; set; } = null!;

    public int RatingMasterId { get; set; }

    public string RatingText { get; set; } = null!;

    public int RatingStars { get; set; }

    public virtual Master RatingMaster { get; set; } = null!;

    public virtual User RatingUserLoginNavigation { get; set; } = null!;
}
