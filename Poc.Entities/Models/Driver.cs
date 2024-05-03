using System;
using System.Collections.Generic;
using TechQ.Core.Extensions;

namespace Poc.Entities.Models;

public partial class Driver
{
    public int DriverId { get; set; }

    public long? AgencyClientId { get; set; }

    public string DriverFirstName { get; set; }

    public string DriverMiddleName { get; set; }

    public string DriverLastName { get; set; }

    public DateOnly? DriverDob { get; set; }

    public string DriverLicenseNumber { get; set; }

    public string DriverLicenseState { get; set; }

    public string DriverLicenseClass { get; set; }

    public DateOnly? DriverExperienceStartDate { get; set; }

    public DateOnly? DriverHireDate { get; set; }

    public string DriverType { get; set; }

    public string DriverExcluded { get; set; }

    public virtual AgencyClient AgencyClient { get; set; }
}

public partial class Driver
{
    public int DriverExperienceYears => (DriverExperienceStartDate ?? DateTime.Today.ToDateOnly()).GetYearsTo(DateTime.Today.ToDateOnly());
}