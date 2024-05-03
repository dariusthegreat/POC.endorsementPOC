using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class Quote
{
    public long QuoteId { get; set; }

    public DateTime? QuoteReceivedDate { get; set; }

    public DateTime? QuoteStartDate { get; set; }

    public DateTime? QuoteUpdateDate { get; set; }

    public DateTime? QuoteExpirationDate { get; set; }

    public long? ApplicationId { get; set; }

    public int? AgencyId { get; set; }

    public int? InsurerId { get; set; }

    public int? InsurerAgentId { get; set; }

    public string ProvidedCoverageType { get; set; }

    public string QuoteBound { get; set; }

    public DateOnly? QuoteBoundDate { get; set; }

    public string AgencyQuoteStatus { get; set; }

    public string RejectionReason { get; set; }

    public virtual Agency Agency { get; set; }

    public virtual Application Application { get; set; }

    public virtual ICollection<ClientDocument> ClientDocuments { get; set; } = new List<ClientDocument>();

    public virtual Insurer Insurer { get; set; }

    public virtual InsurerAgent InsurerAgent { get; set; }

    public virtual ICollection<InsurerDocument> InsurerDocuments { get; set; } = new List<InsurerDocument>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
}
