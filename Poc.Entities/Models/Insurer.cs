using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class Insurer
{
    public int InsurerId { get; set; }

    public string InsurerCoName { get; set; }

    public int? NaicNumber { get; set; }

    public string InsurerAddress1 { get; set; }

    public string InsurerAddress2 { get; set; }

    public string InsurerCity { get; set; }

    public string InsurerState { get; set; }

    public string InsurerZipcode { get; set; }

    public string InsurerEmail { get; set; }

    public string InsurerPhoneNumber { get; set; }

    public string InsurerType { get; set; }

    public string InsurerEmailDomain { get; set; }

    public string UnderwritersViewAll { get; set; }

    public virtual ICollection<DocumentAccepted> DocumentAccepteds { get; set; } = new List<DocumentAccepted>();

    public virtual ICollection<FilteringInsurerCriterion> FilteringInsurerCriteria { get; set; } = new List<FilteringInsurerCriterion>();

    public virtual ICollection<InsurerAgent> InsurerAgents { get; set; } = new List<InsurerAgent>();

    public virtual ICollection<InsurerDocument> InsurerDocuments { get; set; } = new List<InsurerDocument>();

    public virtual ICollection<Market> Markets { get; set; } = new List<Market>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();

    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
}
