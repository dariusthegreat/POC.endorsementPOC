using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class DriverEndorsement
{
    public long Id { get; set; }

    public long? EndorsementId { get; set; }

    public string EndorsementStatus { get; set; }

    public string EndorsementStatusDate { get; set; }

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

    public DateOnly? EndorsementRequestDate { get; set; }

    public DateOnly? PolicyChangeEffectiveDate { get; set; }

    public string OperationType { get; set; }
}
