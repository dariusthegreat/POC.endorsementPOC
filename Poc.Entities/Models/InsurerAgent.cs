using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class InsurerAgent
{
    public int InsurerAgentId { get; set; }

    public int? ManagerInsurerAgentId { get; set; }

    public string UserId { get; set; }

    public int? InsurerId { get; set; }

    public string InsurerAgentFirstName { get; set; }

    public string InsurerAgentLastName { get; set; }

    public string InsurerAgentEmail { get; set; }

    public string InsurerAgentPhone { get; set; }

    public string InsurerAgentTitle { get; set; }

    public string InsurerAgentStatus { get; set; }

    public virtual Insurer Insurer { get; set; }

    public virtual ICollection<Market> Markets { get; set; } = new List<Market>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
}
