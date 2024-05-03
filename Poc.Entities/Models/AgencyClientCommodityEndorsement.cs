using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class AgencyClientCommodityEndorsement
{
    public long Id { get; set; }

    public long? EndorsementId { get; set; }

    public string EndorsementStatus { get; set; }

    public string EndorsementStatusDate { get; set; }

    public long? AgencyClientId { get; set; }

    public short? CommodityId { get; set; }

    public DateOnly? EndorsementRequestDate { get; set; }

    public DateOnly? PolicyChangeEffectiveDate { get; set; }

    public string OperationType { get; set; }
}
