using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class FilteringMgaIc
{
    public int InsuranceCoId { get; set; }

    public string InsuranceCoName { get; set; }

    public virtual ICollection<FilteringInsurerCriterion> FilteringInsurerCriteria { get; set; } = new List<FilteringInsurerCriterion>();
}
