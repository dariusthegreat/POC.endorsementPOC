using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class CoverageType
{
    public short CoverageTypeId { get; set; }

    public string CoverageTypeDescription { get; set; }

    public virtual ICollection<CoverageRequest> CoverageRequests { get; set; } = new List<CoverageRequest>();
}
