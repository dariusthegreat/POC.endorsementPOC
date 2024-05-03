using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public long? AgencyClientId { get; set; }

    public short? VehicleTypeId { get; set; }

    public short? VehicleYear { get; set; }

    public string VehicleMake { get; set; }

    public string VehicleModel { get; set; }

    public string VehicleVinNumber { get; set; }

    public string VehicleGvw { get; set; }

    public decimal? VehicleValue { get; set; }

    public virtual AgencyClient AgencyClient { get; set; }

    public virtual ICollection<CoverageRequest> CoverageRequests { get; set; } = new List<CoverageRequest>();

    public virtual VehicleType VehicleType { get; set; }

    public virtual ICollection<Safety> Safeties { get; set; } = new List<Safety>();
}
