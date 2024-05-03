using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class AgencyAgent
{
    public int AgencyAgentId { get; set; }

    public string UserId { get; set; }

    public int? AgencyId { get; set; }

    public string AgencyAgentFirstName { get; set; }

    public string AgencyAgentLastName { get; set; }

    public int? AgencyAgentNpn { get; set; }

    public string AgencyAgentIsLoggedIn { get; set; }

    public string AgencyAgentActiveSessionId { get; set; }

    public string AgencyAgentEmail { get; set; }

    public string AgencyAgentPhone { get; set; }

    public string AgencyAgentType { get; set; }

    public string AgencyAgentStatus { get; set; }

    public virtual Agency Agency { get; set; }

    public virtual ICollection<AgencyClient> AgencyClients { get; set; } = new List<AgencyClient>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
