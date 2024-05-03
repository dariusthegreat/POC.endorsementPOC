using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class Safety
{
    public byte SafetyId { get; set; }

    public string SafetyDescription { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
