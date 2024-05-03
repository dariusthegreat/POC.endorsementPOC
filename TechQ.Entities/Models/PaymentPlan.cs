using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class PaymentPlan
{
    public short PaymentPlanId { get; set; }

    public string PaymentPlanDescription { get; set; }

    public decimal? MonthlyCharge { get; set; }

    public virtual ICollection<Agency> Agencies { get; set; } = new List<Agency>();
}
