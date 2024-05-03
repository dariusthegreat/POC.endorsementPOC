using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class FilteringMgaIc
{
    public int InsuranceCoId { get; set; }

    public string InsuranceCoName { get; set; }

    public virtual ICollection<FilteringInsurerCriterion> FilteringInsurerCriteria { get; set; } = new List<FilteringInsurerCriterion>();

    public virtual ICollection<InsuranceCompanyState> InsuranceCompanyStates { get; set; } = new List<InsuranceCompanyState>();

    public virtual ICollection<MgaOperatingState> MgaOperatingStates { get; set; } = new List<MgaOperatingState>();

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual ICollection<Insurer> Mgas { get; set; } = new List<Insurer>();
}
