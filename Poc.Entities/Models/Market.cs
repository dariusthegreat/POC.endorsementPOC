using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class Market
{
    public int AgencyId { get; set; }

    public int InsurerId { get; set; }

    public string InsurerAssignedAgencyId { get; set; }

    public int? InsurerAgentId { get; set; }

    public string AgencyInsurerCode { get; set; }

    public string RelationshipActive { get; set; }

    public virtual Agency Agency { get; set; }

    public virtual Insurer Insurer { get; set; }

    public virtual InsurerAgent InsurerAgent { get; set; }
}
