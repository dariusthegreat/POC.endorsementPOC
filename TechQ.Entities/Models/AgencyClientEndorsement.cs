using System;
using System.Collections.Generic;

namespace TechQ.Entities.Models;

public partial class AgencyClientEndorsement
{
    public long? EndorsementId { get; set; }

    public string EndorsementStatus { get; set; }

    public string EndorsementStatusDate { get; set; }

    public long? AgencyClientId { get; set; }

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

    public short? CommodityId { get; set; }

    public string InformationValid { get; set; }

    public byte? InsuredTypeId { get; set; }

    public DateOnly? EndorsementRequestDate { get; set; }

    public DateOnly? PolicyChangeEffectiveDate { get; set; }

    public string OperationType { get; set; }
}
