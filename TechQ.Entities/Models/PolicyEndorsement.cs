using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class PolicyEndorsement
{
    public long Id { get; set; }

    public long? EndorsementId { get; set; }

    public string EndorsementStatus { get; set; }

    public string EndorsementStatusDate { get; set; }

    public string PolicyNumber { get; set; }

    public long? QuoteId { get; set; }

    public int? InsurerId { get; set; }

    public int? AgencyId { get; set; }

    public long? AgencyClientId { get; set; }

    public DateOnly? PolicyStartDate { get; set; }

    public DateOnly? PolicyEndDate { get; set; }

    public DateOnly? EndorsementRequestDate { get; set; }

    public DateOnly? PolicyChangeEffectiveDate { get; set; }

    public string OperationType { get; set; }
}
