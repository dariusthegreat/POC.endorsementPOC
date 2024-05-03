using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class VehicleType
{
    public short VehicleTypeId { get; set; }

    public string VehicleTypeDescription { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
