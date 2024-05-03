using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class VehicleEndorsement
{
    public long Id { get; set; }

    public long? EndorsementId { get; set; }

    public string EndorsementStatus { get; set; }

    public string EndorsementStatusDate { get; set; }

    public int VehicleId { get; set; }

    public long? AgencyClientId { get; set; }

    public short? VehicleTypeId { get; set; }

    public short? VehicleYear { get; set; }

    public string VehicleMake { get; set; }

    public string VehicleModel { get; set; }

    public string VehicleVinNumber { get; set; }

    public string VehicleGvw { get; set; }

    public decimal? VehicleValue { get; set; }

    public DateOnly? EndorsementRequestDate { get; set; }

    public DateOnly? PolicyChangeEffectiveDate { get; set; }

    public string OperationType { get; set; }
}
