using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class DocumentAccepted
{
    public int DocumentAcceptedId { get; set; }

    public int? DocumentId { get; set; }

    public int? AgencyId { get; set; }

    public int? InsurerId { get; set; }

    public string Accepted { get; set; }

    public DateOnly? AcceptedDate { get; set; }

    public virtual Agency Agency { get; set; }

    public virtual Document Document { get; set; }

    public virtual Insurer Insurer { get; set; }
}
