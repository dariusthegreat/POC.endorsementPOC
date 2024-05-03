using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class InsurerDocument
{
    public long InsurerDocumentId { get; set; }

    public long? ApplicationId { get; set; }

    public long? QuoteId { get; set; }

    public int? InsurerId { get; set; }

    public long? AgencyClientId { get; set; }

    public DateOnly? InsurerDocumentDate { get; set; }

    public virtual AgencyClient AgencyClient { get; set; }

    public virtual Application Application { get; set; }

    public virtual Insurer Insurer { get; set; }

    public virtual Quote Quote { get; set; }
}
