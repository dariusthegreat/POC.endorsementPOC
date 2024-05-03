using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class FilteringInsurerCriterion
{
    public int FilteringInsurerCriteriaId { get; set; }

    public int MgaId { get; set; }

    public int InsuranceCoId { get; set; }

    public string FieldName { get; set; }

    public string OperationTypeName { get; set; }

    public string FieldComparisonValue { get; set; }

    public virtual FilteringMgaIc InsuranceCo { get; set; }

    public virtual Insurer Mga { get; set; }

    public virtual FilterOperationType OperationTypeNameNavigation { get; set; }
}
