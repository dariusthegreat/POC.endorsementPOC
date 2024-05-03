using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class NewInsurer
{
    public int NewLeadId { get; set; }

    public int? AgencyId { get; set; }

    public string InsurerName { get; set; }

    public string InsurerPhone { get; set; }

    public string InsurerUnderwriterEmail { get; set; }

    public string InsurerServicingDeptEmail { get; set; }

    public string AgencyCode { get; set; }

    public virtual Agency Agency { get; set; }
}
