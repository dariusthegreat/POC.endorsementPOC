using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class InsuredType
{
    public byte InsuredTypeId { get; set; }

    public string InsuredTypeDescription { get; set; }

    public virtual ICollection<AgencyClient> AgencyClients { get; set; } = new List<AgencyClient>();
}
