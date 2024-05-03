using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class FilterOperationType
{
    public string OperationTypeName { get; set; }

    public virtual ICollection<FilteringInsurerCriterion> FilteringInsurerCriteria { get; set; } = new List<FilteringInsurerCriterion>();
}
