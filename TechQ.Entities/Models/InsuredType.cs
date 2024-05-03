using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class InsuredType
{
    public byte InsuredTypeId { get; set; }

    public string InsuredTypeDescription { get; set; }

    public virtual ICollection<AgencyClient> AgencyClients { get; set; } = new List<AgencyClient>();
}
