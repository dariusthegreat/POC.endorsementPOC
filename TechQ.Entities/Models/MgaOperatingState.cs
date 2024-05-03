using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class MgaOperatingState
{
    public int InsuranceCoId { get; set; }

    public string InsuranceCoStateId { get; set; }

    public virtual FilteringMgaIc InsuranceCo { get; set; }
}
