using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class Insurance
{
    public long InsuranceId { get; set; }

    public long? AgencyClientId { get; set; }

    public string InsuranceCoName { get; set; }

    public DateOnly? InsuranceEffectiveDate { get; set; }

    public DateOnly? InsuranceExpirationDate { get; set; }

    public string InsurancePolicyNo { get; set; }

    public virtual AgencyClient AgencyClient { get; set; }
}
