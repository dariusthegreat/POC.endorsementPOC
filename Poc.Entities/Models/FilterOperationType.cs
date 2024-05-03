using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class FilterOperationType
{
    public string OperationTypeName { get; set; }

    public virtual ICollection<FilteringInsurerCriterion> FilteringInsurerCriteria { get; set; } = new List<FilteringInsurerCriterion>();
}
