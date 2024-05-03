using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class Application
{
    public long ApplicationId { get; set; }

    public DateTime ApplicationStartDate { get; set; }

    public DateTime? ApplicationUpdateDate { get; set; }

    public int? AgencyId { get; set; }

    public long? AgencyClientId { get; set; }

    public string AgencyApplicationStatus { get; set; }

    public string InsurerApplicationStatus { get; set; }

    public string DesiredCoverageType { get; set; }

    public DateOnly? DesiredQuoteStartDate { get; set; }

    public string StatusComment { get; set; }

    public DateOnly? CancellationDate { get; set; }

    public string CancellationComment { get; set; }

    public string RenewalFlag { get; set; }

    public string RenewalBy { get; set; }

    public long? ParentApplicationId { get; set; }

    public virtual Agency Agency { get; set; }

    public virtual AgencyClient AgencyClient { get; set; }

    public virtual ICollection<ClientDocument> ClientDocuments { get; set; } = new List<ClientDocument>();

    public virtual ICollection<CoverageRequest> CoverageRequests { get; set; } = new List<CoverageRequest>();

    public virtual ICollection<InsurerDocument> InsurerDocuments { get; set; } = new List<InsurerDocument>();

    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
}
