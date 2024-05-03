using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class AgentStateLicense
{
    public int AgencyId { get; set; }

    public string StateId { get; set; }

    public string AgentLicenseNumber { get; set; }

    public string ResidentLicenseNumber { get; set; }

    public DateOnly? LicenseNumberEffectiveDate { get; set; }

    public DateOnly? LicenseNumberExpirationDate { get; set; }

    public virtual AgencyAgent Agency { get; set; }
}
