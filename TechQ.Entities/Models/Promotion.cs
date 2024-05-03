using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class Promotion
{
    public short PromotionId { get; set; }

    public string PromotionDescription { get; set; }

    public string PromotionType { get; set; }

    public decimal? PromotionAmount { get; set; }

    public byte? PromotionDiscount { get; set; }

    public DateOnly? PromotionStartDate { get; set; }

    public DateOnly? PromotionEndDate { get; set; }

    public virtual ICollection<Agency> Agencies { get; set; } = new List<Agency>();
}
