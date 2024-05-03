using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class Note
{
    public long NoteId { get; set; }

    public DateTime? NoteDatetime { get; set; }

    public long? QuoteId { get; set; }

    public int? AgencyId { get; set; }

    public int? AgencyAgentId { get; set; }

    public string AgencyAgentFirstName { get; set; }

    public string AgencyAgentLastName { get; set; }

    public int? InsurerId { get; set; }

    public int? InsurerAgentId { get; set; }

    public string InsurerAgentFirstName { get; set; }

    public string InsurerAgentLastName { get; set; }

    public string Note1 { get; set; }

    public virtual Agency Agency { get; set; }

    public virtual AgencyAgent AgencyAgent { get; set; }

    public virtual Insurer Insurer { get; set; }

    public virtual InsurerAgent InsurerAgent { get; set; }

    public virtual Quote Quote { get; set; }
}
