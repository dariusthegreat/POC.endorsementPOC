using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class EndorsementStatus
{
    public long? EndorsementId { get; set; }

    public string EndorsementStatus1 { get; set; }

    public string EndorsementStatusDate { get; set; }
}
