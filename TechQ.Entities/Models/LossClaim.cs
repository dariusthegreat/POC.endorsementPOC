using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class LossClaim
{
    public int ClaimId { get; set; }

    public long? AgencyClientId { get; set; }

    public string InsuranceCoName { get; set; }

    public short? PolicyYear { get; set; }

    public string PolicyNumber { get; set; }

    public DateOnly? DateOfLoss { get; set; }

    public string TypeOfLoss { get; set; }

    public virtual AgencyClient AgencyClient { get; set; }
}
