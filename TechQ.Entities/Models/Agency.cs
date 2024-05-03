using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class Agency
{
    public int AgencyId { get; set; }

    public string AgencyName { get; set; }

    public int? AgencyNpn { get; set; }

    public string AgencyMailingAddress1 { get; set; }

    public string AgencyMailingAddress2 { get; set; }

    public string AgencyMailingCity { get; set; }

    public string AgencyMailingState { get; set; }

    public string AgencyMailingZipcode { get; set; }

    public string AgencyPhysicalAddress1 { get; set; }

    public string AgencyPhysicalAddress2 { get; set; }

    public string AgencyPhysicalCity { get; set; }

    public string AgencyPhysicalState { get; set; }

    public string AgencyPhysicalZipcode { get; set; }

    public string PrimaryContactFirstName { get; set; }

    public string PrimaryContactMiddleName { get; set; }

    public string PrimaryContactLastName { get; set; }

    public string PrimaryContactAddress1 { get; set; }

    public string PrimaryContactAddress2 { get; set; }

    public string PrimaryContactCity { get; set; }

    public string PrimaryContactState { get; set; }

    public string PrimaryContactZipcode { get; set; }

    public string PrimaryContactEmail { get; set; }

    public string PrimaryContactPhone { get; set; }

    public int? PrimaryContactNpn { get; set; }

    public string AgencyFax { get; set; }

    public string EnoInsuranceCoName { get; set; }

    public string EnoPolicyNumber { get; set; }

    public decimal? EnoCoverageAmount { get; set; }

    public DateOnly? EnoPolicyEffectiveDate { get; set; }

    public DateOnly? EnoPolicyExpirationDate { get; set; }

    public string PoolProgramFlag { get; set; }

    public string AgencyLicenseNumber { get; set; }

    public string AgencyTaxId { get; set; }

    public DateOnly? LicenseEffectiveDate { get; set; }

    public DateOnly? LicenseExpirationDate { get; set; }

    public DateOnly? MembershipActivationDate { get; set; }

    public DateOnly? MembershipExpirationDate { get; set; }

    public string ImpDocumentsAccepted { get; set; }

    public string AgencyStatus { get; set; }

    public virtual ICollection<AgencyAgent> AgencyAgents { get; set; } = new List<AgencyAgent>();

    public virtual ICollection<AgencyStateLicense> AgencyStateLicenses { get; set; } = new List<AgencyStateLicense>();

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<DocumentAccepted> DocumentAccepteds { get; set; } = new List<DocumentAccepted>();

    public virtual ICollection<Market> Markets { get; set; } = new List<Market>();

    public virtual ICollection<NewInsurer> NewInsurers { get; set; } = new List<NewInsurer>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();

    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();

    public virtual ICollection<PaymentPlan> PaymentPlans { get; set; } = new List<PaymentPlan>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
}
