using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class InsuranceCompanyState
{
    public int InsuranceCoId { get; set; }

    public string StateCode { get; set; }

    public virtual FilteringMgaIc InsuranceCo { get; set; }
}
