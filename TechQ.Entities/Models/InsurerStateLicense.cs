using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class InsurerStateLicense
{
    public int InsurerId { get; set; }

    public string StateId { get; set; }

    public string InsurerLicenseNumber { get; set; }

    public DateOnly? LicenseNumberEffectiveDate { get; set; }

    public DateOnly? LicenseNumberExpirationDate { get; set; }

    public virtual Insurer Insurer { get; set; }
}
