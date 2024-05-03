using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class AgencyClient
{
    public long AgencyClientId { get; set; }

    public int? AgencyId { get; set; }

    public int? AgencyAgentId { get; set; }

    public string UsDotNumber { get; set; }

    public string DmvFilingState { get; set; }

    public string Mc91Number { get; set; }

    public string Mcs90Number { get; set; }

    public string IccCargoBmc34Number { get; set; }

    public string TexasFormT { get; set; }

    public string OtherInformation { get; set; }

    public string LegalCompanyName { get; set; }

    public string DbaName { get; set; }

    public string OwnerFirstName { get; set; }

    public string OwnerLastName { get; set; }

    public DateOnly? OwnerDoB { get; set; }

    public string OwnerDlNumber { get; set; }

    public string OwnerExcluded { get; set; }

    public string CompanyEmail { get; set; }

    public string CompanyPhone { get; set; }

    public string CompanyPhoneType { get; set; }

    public string CompanyTaxId { get; set; }

    public string MailingAddressStreet { get; set; }

    public string MailingAddressCity { get; set; }

    public string MailingAddressState { get; set; }

    public string MailingAddressZip { get; set; }

    public string PhysicalAddressStreet { get; set; }

    public string PhysicalAddressCity { get; set; }

    public string PhysicalAddressState { get; set; }

    public string PhysicalAddressZip { get; set; }

    public short? YearsOfPriorInsurance { get; set; }

    public string CarrierOperation { get; set; }

    public string TypeOfOperation { get; set; }

    public string RadiusOfOperation { get; set; }

    public string InformationValid { get; set; }

    public byte? InsuredTypeId { get; set; }

    public virtual AgencyAgent AgencyAgent { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<ClientDocument> ClientDocuments { get; set; } = new List<ClientDocument>();

    public virtual ClientOperation ClientOperation { get; set; }

    public virtual ICollection<CoverageRequest> CoverageRequests { get; set; } = new List<CoverageRequest>();

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual ICollection<Garage> Garages { get; set; } = new List<Garage>();

    public virtual ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();

    public virtual InsuredType InsuredType { get; set; }

    public virtual ICollection<InsurerDocument> InsurerDocuments { get; set; } = new List<InsurerDocument>();

    public virtual ICollection<LossClaim> LossClaims { get; set; } = new List<LossClaim>();

    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    public virtual ICollection<Commodity> Commodities { get; set; } = new List<Commodity>();
}
