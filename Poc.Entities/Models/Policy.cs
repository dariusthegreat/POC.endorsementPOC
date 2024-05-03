using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class Policy
{
    public string PolicyNumber { get; set; }

    public long QuoteId { get; set; }

    public int InsurerId { get; set; }

    public int? AgencyId { get; set; }

    public long? AgencyClientId { get; set; }

    public DateOnly? PolicyStartDate { get; set; }

    public DateOnly? PolicyEndDate { get; set; }

    public virtual Agency Agency { get; set; }

    public virtual AgencyClient AgencyClient { get; set; }

    public virtual Insurer Insurer { get; set; }

    public virtual Quote Quote { get; set; }
}
