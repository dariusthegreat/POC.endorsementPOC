using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class Commodity
{
    public short CommodityId { get; set; }

    public string CommodityDescription { get; set; }

    public virtual ICollection<AgencyClient> AgencyClients { get; set; } = new List<AgencyClient>();
}
