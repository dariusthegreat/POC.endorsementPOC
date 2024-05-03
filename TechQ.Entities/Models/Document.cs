using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class Document
{
    public int DocumentId { get; set; }

    public string DocumentDescription { get; set; }

    public string InsurerMustAccept { get; set; }

    public string AgencyMustAccept { get; set; }

    public virtual ICollection<DocumentAccepted> DocumentAccepteds { get; set; } = new List<DocumentAccepted>();
}
