using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class ClientDocument
{
    public long ClientDocumentsId { get; set; }

    public long? ApplicationId { get; set; }

    public long? QuoteId { get; set; }

    public long? AgencyClientId { get; set; }

    public DateOnly? ClientDocumentDate { get; set; }

    public virtual AgencyClient AgencyClient { get; set; }

    public virtual Application Application { get; set; }

    public virtual Quote Quote { get; set; }
}
