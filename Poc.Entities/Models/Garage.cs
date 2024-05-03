using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class Garage
{
    public int GarageId { get; set; }

    public long? AgencyClientId { get; set; }

    public string GaragingAddressStreet { get; set; }

    public string GaragingAddressCity { get; set; }

    public string GaragingAddressState { get; set; }

    public string GaragingAddressZip { get; set; }

    public virtual AgencyClient AgencyClient { get; set; }
}
