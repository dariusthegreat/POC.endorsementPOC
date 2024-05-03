using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class CoverageRequestEndorsement
{
    public long Id { get; set; }

    public long? EndorsementId { get; set; }

    public string EndorsementStatus { get; set; }

    public string EndorsementStatusDate { get; set; }

    public long CoverageId { get; set; }

    public long? AgencyClientId { get; set; }

    public int? VehicleId { get; set; }

    public string AutoLiabilityLimit { get; set; }

    public string AutoLiabilityDeductible { get; set; }

    public string PhysicalDamageCoverage { get; set; }

    public string PhysicalDamageDeductible { get; set; }

    public string CargoLimit { get; set; }

    public string CargoDeductible { get; set; }

    public string MedicalPayment { get; set; }

    public string ReeferCoverage { get; set; }

    public string UninsuredMotorist { get; set; }

    public string HiredAuto { get; set; }

    public string HiredAutoCost { get; set; }

    public string NonownedAuto { get; set; }

    public long? ApplicationId { get; set; }

    public string TrailerInterchangeCoverage { get; set; }

    public string TowingLimit { get; set; }

    public string RentalLimit { get; set; }

    public string DebrisRemoval { get; set; }

    public string RemoveGaragingWarranty { get; set; }

    public DateOnly? EndorsementRequestDate { get; set; }

    public DateOnly? PolicyChangeEffectiveDate { get; set; }

    public string OperationType { get; set; }
}
