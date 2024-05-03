using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class AgencyStateLicense
{
    public int AgencyId { get; set; }

    public string StateId { get; set; }

    public string AgencyLicenseNumber { get; set; }

    public string ResidentLicenseNumber { get; set; }

    public DateOnly? LicenseNumberEffectiveDate { get; set; }

    public DateOnly? LicenseNumberExpirationDate { get; set; }

    public virtual Agency Agency { get; set; }
}
