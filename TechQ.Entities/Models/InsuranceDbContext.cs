using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TechQ.Entities.Models;

public partial class InsuranceDbContext : DbContext
{
    public InsuranceDbContext()
    {
    }

    public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcordFile> AcordFiles { get; set; }

    public virtual DbSet<Agency> Agencies { get; set; }

    public virtual DbSet<AgencyAgent> AgencyAgents { get; set; }

    public virtual DbSet<AgencyClient> AgencyClients { get; set; }

    public virtual DbSet<AgencyClientCommodityEndorsement> AgencyClientCommodityEndorsements { get; set; }

    public virtual DbSet<AgencyClientEndorsement> AgencyClientEndorsements { get; set; }

    public virtual DbSet<AgencyStateLicense> AgencyStateLicenses { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ClientDocument> ClientDocuments { get; set; }

    public virtual DbSet<ClientOperation> ClientOperations { get; set; }

    public virtual DbSet<Commodity> Commodities { get; set; }

    public virtual DbSet<CoverageRequest> CoverageRequests { get; set; }

    public virtual DbSet<CoverageRequestEndorsement> CoverageRequestEndorsements { get; set; }

    public virtual DbSet<CoverageType> CoverageTypes { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentAccepted> DocumentAccepteds { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<DriverEndorsement> DriverEndorsements { get; set; }

    public virtual DbSet<EndorsementStatus> EndorsementStatuses { get; set; }

    public virtual DbSet<FilterOperationType> FilterOperationTypes { get; set; }

    public virtual DbSet<FilteringInsurerCriterion> FilteringInsurerCriteria { get; set; }

    public virtual DbSet<FilteringMgaIc> FilteringMgaIcs { get; set; }

    public virtual DbSet<Garage> Garages { get; set; }

    public virtual DbSet<Insurance> Insurances { get; set; }

    public virtual DbSet<InsuranceCompanyState> InsuranceCompanyStates { get; set; }

    public virtual DbSet<InsuredType> InsuredTypes { get; set; }

    public virtual DbSet<Insurer> Insurers { get; set; }

    public virtual DbSet<InsurerAgent> InsurerAgents { get; set; }

    public virtual DbSet<InsurerDocument> InsurerDocuments { get; set; }

    public virtual DbSet<InsurerStateLicense> InsurerStateLicenses { get; set; }

    public virtual DbSet<LossClaim> LossClaims { get; set; }

    public virtual DbSet<Market> Markets { get; set; }

    public virtual DbSet<MgaOperatingState> MgaOperatingStates { get; set; }

    public virtual DbSet<NewInsurer> NewInsurers { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<PaymentPlan> PaymentPlans { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<PolicyEndorsement> PolicyEndorsements { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Quote> Quotes { get; set; }

    public virtual DbSet<Safety> Safeties { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleEndorsement> VehicleEndorsements { get; set; }

    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcordFile>(entity =>
        {
            entity.HasKey(e => e.AcordFileId).HasName("pk_acord_files");

            entity.ToTable("acord_files");

            entity.Property(e => e.AcordFileId).HasColumnName("acord_file_id");
            entity.Property(e => e.AcordFileDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("acord_file_description");
        });

        modelBuilder.Entity<Agency>(entity =>
        {
            entity.HasKey(e => e.AgencyId).HasName("pk_agency");

            entity.ToTable("agency");

            entity.Property(e => e.AgencyId).HasColumnName("agency_id");
            entity.Property(e => e.AgencyFax)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("agency_fax");
            entity.Property(e => e.AgencyLicenseNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("agency_license_number");
            entity.Property(e => e.AgencyMailingAddress1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("agency_mailing_address1");
            entity.Property(e => e.AgencyMailingAddress2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("agency_mailing_address2");
            entity.Property(e => e.AgencyMailingCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("agency_mailing_city");
            entity.Property(e => e.AgencyMailingState)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("agency_mailing_state");
            entity.Property(e => e.AgencyMailingZipcode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("agency_mailing_zipcode");
            entity.Property(e => e.AgencyName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("agency_name");
            entity.Property(e => e.AgencyNpn).HasColumnName("agency_npn");
            entity.Property(e => e.AgencyPhysicalAddress1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("agency_physical_address1");
            entity.Property(e => e.AgencyPhysicalAddress2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("agency_physical_address2");
            entity.Property(e => e.AgencyPhysicalCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("agency_physical_city");
            entity.Property(e => e.AgencyPhysicalState)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("agency_physical_state");
            entity.Property(e => e.AgencyPhysicalZipcode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("agency_physical_zipcode");
            entity.Property(e => e.AgencyStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("agency_status");
            entity.Property(e => e.AgencyTaxId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("agency_tax_id");
            entity.Property(e => e.EnoCoverageAmount)
                .HasColumnType("money")
                .HasColumnName("eno_coverage_amount");
            entity.Property(e => e.EnoInsuranceCoName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("eno_insurance_co_name");
            entity.Property(e => e.EnoPolicyEffectiveDate).HasColumnName("eno_policy_effective_date");
            entity.Property(e => e.EnoPolicyExpirationDate).HasColumnName("eno_policy_expiration_date");
            entity.Property(e => e.EnoPolicyNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("eno_policy_number");
            entity.Property(e => e.ImpDocumentsAccepted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("imp_documents_accepted");
            entity.Property(e => e.LicenseEffectiveDate).HasColumnName("license_effective_date");
            entity.Property(e => e.LicenseExpirationDate).HasColumnName("license_expiration_date");
            entity.Property(e => e.MembershipActivationDate).HasColumnName("membership_activation_date");
            entity.Property(e => e.MembershipExpirationDate).HasColumnName("membership_expiration_date");
            entity.Property(e => e.PoolProgramFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pool_program_flag");
            entity.Property(e => e.PrimaryContactAddress1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("primary_contact_address1");
            entity.Property(e => e.PrimaryContactAddress2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primary_contact_address2");
            entity.Property(e => e.PrimaryContactCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primary_contact_city");
            entity.Property(e => e.PrimaryContactEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("primary_contact_email");
            entity.Property(e => e.PrimaryContactFirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("primary_contact_first_name");
            entity.Property(e => e.PrimaryContactLastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("primary_contact_last_name");
            entity.Property(e => e.PrimaryContactMiddleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("primary_contact_middle_name");
            entity.Property(e => e.PrimaryContactNpn).HasColumnName("primary_contact_npn");
            entity.Property(e => e.PrimaryContactPhone)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("primary_contact_phone");
            entity.Property(e => e.PrimaryContactState)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primary_contact_state");
            entity.Property(e => e.PrimaryContactZipcode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("primary_contact_zipcode");

            entity.HasMany(d => d.PaymentPlans).WithMany(p => p.Agencies)
                .UsingEntity<Dictionary<string, object>>(
                    "AgencyPaymentPlan",
                    r => r.HasOne<PaymentPlan>().WithMany()
                        .HasForeignKey("PaymentPlanId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_agency_payment_plan__payment_plan"),
                    l => l.HasOne<Agency>().WithMany()
                        .HasForeignKey("AgencyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_agency_payment_plan__agency"),
                    j =>
                    {
                        j.HasKey("AgencyId", "PaymentPlanId").HasName("pk_agency_payment_plan");
                        j.ToTable("agency_payment_plan");
                        j.IndexerProperty<int>("AgencyId").HasColumnName("Agency_Id");
                        j.IndexerProperty<short>("PaymentPlanId").HasColumnName("Payment_Plan_Id");
                    });

            entity.HasMany(d => d.Promotions).WithMany(p => p.Agencies)
                .UsingEntity<Dictionary<string, object>>(
                    "AgencyPromotion",
                    r => r.HasOne<Promotion>().WithMany()
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_agency_promotion__promotion"),
                    l => l.HasOne<Agency>().WithMany()
                        .HasForeignKey("AgencyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_agency_promotion__agency"),
                    j =>
                    {
                        j.HasKey("AgencyId", "PromotionId").HasName("pk_agency_promotion");
                        j.ToTable("agency_promotion");
                        j.IndexerProperty<int>("AgencyId").HasColumnName("Agency_Id");
                        j.IndexerProperty<short>("PromotionId").HasColumnName("Promotion_Id");
                    });
        });

        modelBuilder.Entity<AgencyAgent>(entity =>
        {
            entity.HasKey(e => e.AgencyAgentId).HasName("pk_agency_agents");

            entity.ToTable("agency_agents");

            entity.Property(e => e.AgencyAgentId).HasColumnName("agency_agent_id");
            entity.Property(e => e.AgencyAgentActiveSessionId)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("agency_agent_active_session_id");
            entity.Property(e => e.AgencyAgentEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("agency_agent_email");
            entity.Property(e => e.AgencyAgentFirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("agency_agent_first_name");
            entity.Property(e => e.AgencyAgentIsLoggedIn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("agency_agent_is_logged_in");
            entity.Property(e => e.AgencyAgentLastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("agency_agent_last_name");
            entity.Property(e => e.AgencyAgentNpn).HasColumnName("agency_agent_npn");
            entity.Property(e => e.AgencyAgentPhone)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("agency_agent_phone");
            entity.Property(e => e.AgencyAgentStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("agency_agent_status");
            entity.Property(e => e.AgencyAgentType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("agency_agent_type");
            entity.Property(e => e.AgencyId).HasColumnName("agency_id");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Agency).WithMany(p => p.AgencyAgents)
                .HasForeignKey(d => d.AgencyId)
                .HasConstraintName("fk_agency_agents__agency");
        });

        modelBuilder.Entity<AgencyClient>(entity =>
        {
            entity.HasKey(e => e.AgencyClientId).HasName("pk_agency_client");

            entity.ToTable("agency_client");

            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.AgencyAgentId).HasColumnName("agency_agent_id");
            entity.Property(e => e.AgencyId).HasColumnName("agency_id");
            entity.Property(e => e.CarrierOperation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("carrier_operation");
            entity.Property(e => e.CompanyEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("company_email");
            entity.Property(e => e.CompanyPhone)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("company_phone");
            entity.Property(e => e.CompanyPhoneType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("company_phone_type");
            entity.Property(e => e.CompanyTaxId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("company_tax_id");
            entity.Property(e => e.DbaName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("dba_name");
            entity.Property(e => e.DmvFilingState)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("dmv_filing_state");
            entity.Property(e => e.IccCargoBmc34Number)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("icc_cargo_bmc34_number");
            entity.Property(e => e.InformationValid)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("information_valid");
            entity.Property(e => e.InsuredTypeId).HasColumnName("insured_type_id");
            entity.Property(e => e.LegalCompanyName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("legal_company_name");
            entity.Property(e => e.MailingAddressCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mailing_address_city");
            entity.Property(e => e.MailingAddressState)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("mailing_address_state");
            entity.Property(e => e.MailingAddressStreet)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mailing_address_street");
            entity.Property(e => e.MailingAddressZip)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("mailing_address_zip");
            entity.Property(e => e.Mc91Number)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("mc91_number");
            entity.Property(e => e.Mcs90Number)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("mcs90_number");
            entity.Property(e => e.OtherInformation)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("other_information");
            entity.Property(e => e.OwnerDlNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Owner_DL_Number");
            entity.Property(e => e.OwnerDoB).HasColumnName("Owner_DoB");
            entity.Property(e => e.OwnerExcluded)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Owner_Excluded");
            entity.Property(e => e.OwnerFirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("owner_first_name");
            entity.Property(e => e.OwnerLastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("owner_last_name");
            entity.Property(e => e.PhysicalAddressCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("physical_address_city");
            entity.Property(e => e.PhysicalAddressState)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("physical_address_state");
            entity.Property(e => e.PhysicalAddressStreet)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("physical_address_street");
            entity.Property(e => e.PhysicalAddressZip)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("physical_address_zip");
            entity.Property(e => e.RadiusOfOperation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("radius_of_operation");
            entity.Property(e => e.TexasFormT)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("texas_form_t");
            entity.Property(e => e.TypeOfOperation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type_of_operation");
            entity.Property(e => e.UsDotNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("us_dot_number");
            entity.Property(e => e.YearsOfPriorInsurance).HasColumnName("years_of_prior_insurance");

            entity.HasOne(d => d.AgencyAgent).WithMany(p => p.AgencyClients)
                .HasForeignKey(d => d.AgencyAgentId)
                .HasConstraintName("fk_agency_client__agency_agents");

            entity.HasOne(d => d.InsuredType).WithMany(p => p.AgencyClients)
                .HasForeignKey(d => d.InsuredTypeId)
                .HasConstraintName("fk_agency_client__insured_type");

            entity.HasMany(d => d.Commodities).WithMany(p => p.AgencyClients)
                .UsingEntity<Dictionary<string, object>>(
                    "AgencyClientCommodity",
                    r => r.HasOne<Commodity>().WithMany()
                        .HasForeignKey("CommodityId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_agency_client_commodity__commodity"),
                    l => l.HasOne<AgencyClient>().WithMany()
                        .HasForeignKey("AgencyClientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_agency_client_commodity__agency_client"),
                    j =>
                    {
                        j.HasKey("AgencyClientId", "CommodityId").HasName("pk_agency_client_commodity");
                        j.ToTable("agency_client_commodity");
                        j.IndexerProperty<long>("AgencyClientId").HasColumnName("agency_client_id");
                        j.IndexerProperty<short>("CommodityId").HasColumnName("commodity_id");
                    });
        });

        modelBuilder.Entity<AgencyClientCommodityEndorsement>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("agency_client_commodity_endorsement");

            entity.HasIndex(e => new { e.EndorsementId, e.EndorsementStatus }, "CIDX__dbo__agency_client_commodity_endorsement__Index1").IsClustered();

            entity.HasIndex(e => new { e.AgencyClientId, e.CommodityId }, "IDX__dbo__agency_client_commodity_endorsement__Index1");

            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.CommodityId).HasColumnName("commodity_id");
            entity.Property(e => e.EndorsementId).HasColumnName("Endorsement_Id");
            entity.Property(e => e.EndorsementRequestDate).HasColumnName("Endorsement_Request_Date");
            entity.Property(e => e.EndorsementStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Endorsement_Status");
            entity.Property(e => e.EndorsementStatusDate)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Endorsement_Status_Date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.OperationType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Operation_Type");
            entity.Property(e => e.PolicyChangeEffectiveDate).HasColumnName("Policy_Change_Effective_Date");
        });

        modelBuilder.Entity<AgencyClientEndorsement>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("agency_client_endorsement");

            entity.HasIndex(e => new { e.EndorsementId, e.EndorsementStatus }, "CIDX__dbo__agency_client_endorsement__Index1").IsClustered();

            entity.HasIndex(e => e.AgencyClientId, "IDX__dbo__agency_client_endorsement__Index1");

            entity.Property(e => e.AgencyAgentId).HasColumnName("agency_agent_id");
            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.AgencyId).HasColumnName("agency_id");
            entity.Property(e => e.CarrierOperation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("carrier_operation");
            entity.Property(e => e.CommodityId).HasColumnName("commodity_id");
            entity.Property(e => e.CompanyEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("company_email");
            entity.Property(e => e.CompanyPhone)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("company_phone");
            entity.Property(e => e.CompanyPhoneType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("company_phone_type");
            entity.Property(e => e.CompanyTaxId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("company_tax_id");
            entity.Property(e => e.DbaName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("dba_name");
            entity.Property(e => e.DmvFilingState)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("dmv_filing_state");
            entity.Property(e => e.EndorsementId).HasColumnName("Endorsement_Id");
            entity.Property(e => e.EndorsementRequestDate).HasColumnName("Endorsement_Request_Date");
            entity.Property(e => e.EndorsementStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Endorsement_Status");
            entity.Property(e => e.EndorsementStatusDate)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Endorsement_Status_Date");
            entity.Property(e => e.IccCargoBmc34Number)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("icc_cargo_bmc34_number");
            entity.Property(e => e.InformationValid)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("information_valid");
            entity.Property(e => e.InsuredTypeId).HasColumnName("insured_type_id");
            entity.Property(e => e.LegalCompanyName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("legal_company_name");
            entity.Property(e => e.MailingAddressCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mailing_address_city");
            entity.Property(e => e.MailingAddressState)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("mailing_address_state");
            entity.Property(e => e.MailingAddressStreet)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mailing_address_street");
            entity.Property(e => e.MailingAddressZip)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("mailing_address_zip");
            entity.Property(e => e.Mc91Number)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("mc91_number");
            entity.Property(e => e.Mcs90Number)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("mcs90_number");
            entity.Property(e => e.OperationType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Operation_Type");
            entity.Property(e => e.OtherInformation)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("other_information");
            entity.Property(e => e.OwnerDlNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Owner_DL_Number");
            entity.Property(e => e.OwnerDoB).HasColumnName("Owner_DoB");
            entity.Property(e => e.OwnerExcluded)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Owner_Excluded");
            entity.Property(e => e.OwnerFirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("owner_first_name");
            entity.Property(e => e.OwnerLastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("owner_last_name");
            entity.Property(e => e.PhysicalAddressCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("physical_address_city");
            entity.Property(e => e.PhysicalAddressState)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("physical_address_state");
            entity.Property(e => e.PhysicalAddressStreet)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("physical_address_street");
            entity.Property(e => e.PhysicalAddressZip)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("physical_address_zip");
            entity.Property(e => e.PolicyChangeEffectiveDate).HasColumnName("Policy_Change_Effective_Date");
            entity.Property(e => e.RadiusOfOperation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("radius_of_operation");
            entity.Property(e => e.TexasFormT)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("texas_form_t");
            entity.Property(e => e.TypeOfOperation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type_of_operation");
            entity.Property(e => e.UsDotNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("us_dot_number");
            entity.Property(e => e.YearsOfPriorInsurance).HasColumnName("years_of_prior_insurance");
        });

        modelBuilder.Entity<AgencyStateLicense>(entity =>
        {
            entity.HasKey(e => new { e.AgencyId, e.StateId }).HasName("pk_agent_state_license");

            entity.ToTable("agency_state_license");

            entity.Property(e => e.AgencyId).HasColumnName("agency_id");
            entity.Property(e => e.StateId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("state_id");
            entity.Property(e => e.AgencyLicenseNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("agency_license_number");
            entity.Property(e => e.LicenseNumberEffectiveDate).HasColumnName("license_number_effective_date");
            entity.Property(e => e.LicenseNumberExpirationDate).HasColumnName("license_number_expiration_date");
            entity.Property(e => e.ResidentLicenseNumber)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("resident_license_number");

            entity.HasOne(d => d.Agency).WithMany(p => p.AgencyStateLicenses)
                .HasForeignKey(d => d.AgencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_agency_state_license__agency");
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("pk_application");

            entity.ToTable("application");

            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.AgencyApplicationStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("agency_application_status");
            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.AgencyId).HasColumnName("agency_id");
            entity.Property(e => e.ApplicationStartDate)
                .HasColumnType("datetime")
                .HasColumnName("application_start_date");
            entity.Property(e => e.ApplicationUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("application_update_date");
            entity.Property(e => e.CancellationComment)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("cancellation_comment");
            entity.Property(e => e.CancellationDate).HasColumnName("cancellation_date");
            entity.Property(e => e.DesiredCoverageType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("desired_coverage_type");
            entity.Property(e => e.DesiredQuoteStartDate).HasColumnName("desired_quote_start_date");
            entity.Property(e => e.InsurerApplicationStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("insurer_application_status");
            entity.Property(e => e.ParentApplicationId).HasColumnName("parent_application_Id");
            entity.Property(e => e.RenewalBy)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("renewal_by");
            entity.Property(e => e.RenewalFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("renewal_flag");
            entity.Property(e => e.StatusComment)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("status_comment");

            entity.HasOne(d => d.AgencyClient).WithMany(p => p.Applications)
                .HasForeignKey(d => d.AgencyClientId)
                .HasConstraintName("fk_application__agency_client");

            entity.HasOne(d => d.Agency).WithMany(p => p.Applications)
                .HasForeignKey(d => d.AgencyId)
                .HasConstraintName("fk_application__agency");
        });

        modelBuilder.Entity<ClientDocument>(entity =>
        {
            entity.HasKey(e => e.ClientDocumentsId).HasName("pk_client_documents");

            entity.ToTable("client_documents");

            entity.Property(e => e.ClientDocumentsId).HasColumnName("client_documents_id");
            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.ClientDocumentDate).HasColumnName("client_document_date");
            entity.Property(e => e.QuoteId).HasColumnName("quote_id");

            entity.HasOne(d => d.AgencyClient).WithMany(p => p.ClientDocuments)
                .HasForeignKey(d => d.AgencyClientId)
                .HasConstraintName("fk_client_documents__agency_client");

            entity.HasOne(d => d.Application).WithMany(p => p.ClientDocuments)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("fk_client_documents__application");

            entity.HasOne(d => d.Quote).WithMany(p => p.ClientDocuments)
                .HasForeignKey(d => d.QuoteId)
                .HasConstraintName("fk_client_documents__quote");
        });

        modelBuilder.Entity<ClientOperation>(entity =>
        {
            entity.HasKey(e => e.AgencyClientId).HasName("pk_client_operation");

            entity.ToTable("client_operation");

            entity.Property(e => e.AgencyClientId)
                .ValueGeneratedNever()
                .HasColumnName("agency_client_id");
            entity.Property(e => e.AbortedPolicies)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("aborted_policies");
            entity.Property(e => e.CargoInsuranceRequired)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cargo_insurance_required");
            entity.Property(e => e.ComplyDotInspection)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("comply_dot_inspection");
            entity.Property(e => e.DisciplinaryPlanDocumented)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("disciplinary_plan_documented");
            entity.Property(e => e.DriverIncentiveProgram)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("driver_incentive_program");
            entity.Property(e => e.DriverMvrObtained)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("driver_mvr_obtained");
            entity.Property(e => e.DriversDotCompliant)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("drivers_dot_compliant");
            entity.Property(e => e.HaulExtraTrailers)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("haul_extra_trailers");
            entity.Property(e => e.HaulHazardMaterial)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("haul_hazard_material");
            entity.Property(e => e.HaulOversizeOverweight)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("haul_oversize_overweight");
            entity.Property(e => e.HaveSafetyProgram)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("have_safety_program");
            entity.Property(e => e.HowsmydrivingProgram)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("howsmydriving_program");
            entity.Property(e => e.LeasedVehicleOthers)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("leased_vehicle_others");
            entity.Property(e => e.Minimum5YearsDriving)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("minimum_5_years_driving");
            entity.Property(e => e.NoCellWhileDriving)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("no_cell_while_driving");
            entity.Property(e => e.OperateMultiState)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("operate_multi_state");
            entity.Property(e => e.OperateSameVehicle)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("operate_same_vehicle");
            entity.Property(e => e.OvernightCommoditiesStorage)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("overnight_commodities_storage");
            entity.Property(e => e.OwnAnotherCompany)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("own_another_company");
            entity.Property(e => e.OwnAnotherCompanyExplanation)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("own_another_company_explanation");
            entity.Property(e => e.ProvideWorkerComp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("provide_worker_comp");
            entity.Property(e => e.RefrigeratedUnits)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("refrigerated_units");
            entity.Property(e => e.RefrigerationCoverageDesired)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("refrigeration_coverage_desired");
            entity.Property(e => e.ReportVehiclesDrivers)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("report_vehicles_drivers");
            entity.Property(e => e.TeamDrivers)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("team_drivers");
            entity.Property(e => e.Under21Drivers)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("under_21_drivers");
            entity.Property(e => e.UndisclosedVehicles)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("undisclosed_vehicles");

            entity.HasOne(d => d.AgencyClient).WithOne(p => p.ClientOperation)
                .HasForeignKey<ClientOperation>(d => d.AgencyClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_client_operation__agency_client");
        });

        modelBuilder.Entity<Commodity>(entity =>
        {
            entity.HasKey(e => e.CommodityId).HasName("pk_commodity");

            entity.ToTable("commodity");

            entity.Property(e => e.CommodityId).HasColumnName("commodity_id");
            entity.Property(e => e.CommodityDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("commodity_description");
        });

        modelBuilder.Entity<CoverageRequest>(entity =>
        {
            entity.HasKey(e => e.CoverageId).HasName("pk_coverage_request");

            entity.ToTable("coverage_request");

            entity.Property(e => e.CoverageId).HasColumnName("coverage_id");
            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.AutoLiabilityDeductible)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("auto_liability_deductible");
            entity.Property(e => e.AutoLiabilityLimit)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("auto_liability_limit");
            entity.Property(e => e.CargoDeductible)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Cargo_Deductible");
            entity.Property(e => e.CargoLimit)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Cargo_Limit");
            entity.Property(e => e.CoverageTypeId).HasColumnName("coverage_type_id");
            entity.Property(e => e.DebrisRemoval)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("debris_removal");
            entity.Property(e => e.HiredAuto)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("hired_auto");
            entity.Property(e => e.HiredAutoCost)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("hired_auto_cost");
            entity.Property(e => e.MedicalPayment)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("medical_payment");
            entity.Property(e => e.NonownedAuto)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nonowned_auto");
            entity.Property(e => e.NonownedTrailerCoverage)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("nonowned_trailer_coverage");
            entity.Property(e => e.PhysicalDamageCoverage)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("physical_damage_coverage");
            entity.Property(e => e.PhysicalDamageDeductible)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("physical_damage_deductible");
            entity.Property(e => e.ReeferCoverage)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("reefer_coverage");
            entity.Property(e => e.ReeferDeductible)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("reefer_deductible");
            entity.Property(e => e.RemoveGaragingWarranty)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("remove_garaging_warranty");
            entity.Property(e => e.RentalLimit)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("rental_limit");
            entity.Property(e => e.TowingLimit)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("towing_limit");
            entity.Property(e => e.TrailerInterchageDeductible)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("trailer_interchage_deductible");
            entity.Property(e => e.TrailerInterchangeCoverage)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("trailer_interchange_coverage");
            entity.Property(e => e.UninsuredMotorist)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("uninsured_motorist");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.AgencyClient).WithMany(p => p.CoverageRequests)
                .HasForeignKey(d => d.AgencyClientId)
                .HasConstraintName("fk_coverage_request__agency_client");

            entity.HasOne(d => d.Application).WithMany(p => p.CoverageRequests)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("fk_coverage_request__application");

            entity.HasOne(d => d.CoverageType).WithMany(p => p.CoverageRequests)
                .HasForeignKey(d => d.CoverageTypeId)
                .HasConstraintName("fk_coverage_request__coverage_type");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.CoverageRequests)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("fk_coverage_request__vehicle");
        });

        modelBuilder.Entity<CoverageRequestEndorsement>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("coverage_request_endorsement");

            entity.HasIndex(e => new { e.EndorsementId, e.EndorsementStatus }, "CIDX__dbo__coverage_request_endorsement__Index1").IsClustered();

            entity.HasIndex(e => e.CoverageId, "IDX__dbo__coverage_request_endorsement__Index1");

            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.AutoLiabilityDeductible)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("auto_liability_deductible");
            entity.Property(e => e.AutoLiabilityLimit)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("auto_liability_limit");
            entity.Property(e => e.CargoDeductible)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cargo_deductible");
            entity.Property(e => e.CargoLimit)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cargo_limit");
            entity.Property(e => e.CoverageId).HasColumnName("coverage_id");
            entity.Property(e => e.CoverageTypeId).HasColumnName("coverage_type_id");
            entity.Property(e => e.DebrisRemoval)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("debris_removal");
            entity.Property(e => e.EndorsementId).HasColumnName("Endorsement_Id");
            entity.Property(e => e.EndorsementRequestDate).HasColumnName("Endorsement_Request_Date");
            entity.Property(e => e.EndorsementStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Endorsement_Status");
            entity.Property(e => e.EndorsementStatusDate)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Endorsement_Status_Date");
            entity.Property(e => e.HiredAuto)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("hired_auto");
            entity.Property(e => e.HiredAutoCost)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("hired_auto_cost");
            entity.Property(e => e.MedicalPayment)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("medical_payment");
            entity.Property(e => e.NonownedAuto)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nonowned_auto");
            entity.Property(e => e.NonownedTrailerCoverage)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("nonowned_trailer_coverage");
            entity.Property(e => e.OperationType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Operation_Type");
            entity.Property(e => e.PhysicalDamageCoverage)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("physical_damage_coverage");
            entity.Property(e => e.PhysicalDamageDeductible)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("physical_damage_deductible");
            entity.Property(e => e.PolicyChangeEffectiveDate).HasColumnName("Policy_Change_Effective_Date");
            entity.Property(e => e.ReeferCoverage)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("reefer_coverage");
            entity.Property(e => e.ReeferDeductible)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("reefer_deductible");
            entity.Property(e => e.RentalLimit)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("rental_limit");
            entity.Property(e => e.TowingLimit)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("towing_limit");
            entity.Property(e => e.TrailerInterchageDeductible)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("trailer_interchage_deductible");
            entity.Property(e => e.TrailerInterchangeCoverage)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("trailer_interchange_coverage");
            entity.Property(e => e.UninsuredMotorist)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("uninsured_motorist");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
        });

        modelBuilder.Entity<CoverageType>(entity =>
        {
            entity.HasKey(e => e.CoverageTypeId).HasName("pk_coverage_type");

            entity.ToTable("coverage_type");

            entity.Property(e => e.CoverageTypeId).HasColumnName("coverage_type_id");
            entity.Property(e => e.CoverageTypeDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("coverage_type_description");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("pk_documents");

            entity.ToTable("documents");

            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.AgencyMustAccept)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("agency_must_accept");
            entity.Property(e => e.DocumentDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("document_description");
            entity.Property(e => e.InsurerMustAccept)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("insurer_must_accept");
        });

        modelBuilder.Entity<DocumentAccepted>(entity =>
        {
            entity.HasKey(e => e.DocumentAcceptedId).HasName("pk_document_accepted");

            entity.ToTable("document_accepted");

            entity.Property(e => e.DocumentAcceptedId).HasColumnName("document_accepted_id");
            entity.Property(e => e.Accepted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("accepted");
            entity.Property(e => e.AcceptedDate).HasColumnName("accepted_date");
            entity.Property(e => e.AgencyId).HasColumnName("agency_id");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.InsurerId).HasColumnName("insurer_id");

            entity.HasOne(d => d.Agency).WithMany(p => p.DocumentAccepteds)
                .HasForeignKey(d => d.AgencyId)
                .HasConstraintName("fk_document_accepted__agency");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentAccepteds)
                .HasForeignKey(d => d.DocumentId)
                .HasConstraintName("fk_document_accepted__documents");

            entity.HasOne(d => d.Insurer).WithMany(p => p.DocumentAccepteds)
                .HasForeignKey(d => d.InsurerId)
                .HasConstraintName("fk_document_accepted__insurer");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("pk_driver");

            entity.ToTable("driver");

            entity.Property(e => e.DriverId).HasColumnName("driver_id");
            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.DriverDob).HasColumnName("driver_dob");
            entity.Property(e => e.DriverExcluded)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("driver_excluded");
            entity.Property(e => e.DriverExperienceStartDate).HasColumnName("driver_experience_start_date");
            entity.Property(e => e.DriverFirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("driver_first_name");
            entity.Property(e => e.DriverHireDate).HasColumnName("driver_hire_date");
            entity.Property(e => e.DriverLastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("driver_last_name");
            entity.Property(e => e.DriverLicenseClass)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("driver_license_class");
            entity.Property(e => e.DriverLicenseNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("driver_license_number");
            entity.Property(e => e.DriverLicenseState)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("driver_license_state");
            entity.Property(e => e.DriverMiddleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("driver_middle_name");
            entity.Property(e => e.DriverType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("driver_type");

            entity.HasOne(d => d.AgencyClient).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.AgencyClientId)
                .HasConstraintName("fk_driver__agency_client");
        });

        modelBuilder.Entity<DriverEndorsement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__driver_e__3214EC279E05D5C6");

            entity.ToTable("driver_endorsement");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.DriverDob).HasColumnName("driver_dob");
            entity.Property(e => e.DriverExcluded)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("driver_excluded");
            entity.Property(e => e.DriverExperienceStartDate).HasColumnName("driver_experience_start_date");
            entity.Property(e => e.DriverFirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("driver_first_name");
            entity.Property(e => e.DriverHireDate).HasColumnName("driver_hire_date");
            entity.Property(e => e.DriverId).HasColumnName("driver_id");
            entity.Property(e => e.DriverLastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("driver_last_name");
            entity.Property(e => e.DriverLicenseClass)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("driver_license_class");
            entity.Property(e => e.DriverLicenseNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("driver_license_number");
            entity.Property(e => e.DriverLicenseState)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("driver_license_state");
            entity.Property(e => e.DriverMiddleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("driver_middle_name");
            entity.Property(e => e.DriverType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("driver_type");
            entity.Property(e => e.EndorsementId).HasColumnName("Endorsement_Id");
            entity.Property(e => e.EndorsementRequestDate).HasColumnName("Endorsement_Request_Date");
            entity.Property(e => e.EndorsementStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Endorsement_Status");
            entity.Property(e => e.EndorsementStatusDate)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("Endorsement_Status_Date");
            entity.Property(e => e.OperationType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Operation_Type");
            entity.Property(e => e.PolicyChangeEffectiveDate).HasColumnName("Policy_Change_Effective_Date");
        });

        modelBuilder.Entity<EndorsementStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("endorsement_status");

            entity.HasIndex(e => new { e.EndorsementId, e.EndorsementStatus1 }, "CIDX__dbo__endorsement_status__Index1").IsClustered();

            entity.Property(e => e.EndorsementId).HasColumnName("Endorsement_Id");
            entity.Property(e => e.EndorsementStatus1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Endorsement_Status");
            entity.Property(e => e.EndorsementStatusDate)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Endorsement_Status_Date");
        });

        modelBuilder.Entity<FilterOperationType>(entity =>
        {
            entity.HasKey(e => e.OperationTypeName).HasName("PK__filter_o__1EBD0CFC42BE0F56");

            entity.ToTable("filter_operation_type");

            entity.Property(e => e.OperationTypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("operation_type_name");
        });

        modelBuilder.Entity<FilteringInsurerCriterion>(entity =>
        {
            entity.HasKey(e => e.FilteringInsurerCriteriaId).HasName("PK__filterin__8ED8DEA55EE3014C");

            entity.ToTable("filtering_insurer_criteria");

            entity.Property(e => e.FilteringInsurerCriteriaId).HasColumnName("filtering_insurer_criteria_id");
            entity.Property(e => e.FieldComparisonValue)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("field_comparison_value");
            entity.Property(e => e.FieldName)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("field_name");
            entity.Property(e => e.InsuranceCoId).HasColumnName("insurance_co_id");
            entity.Property(e => e.MgaId).HasColumnName("mga_id");
            entity.Property(e => e.OperationTypeName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("operation_type_name");

            entity.HasOne(d => d.InsuranceCo).WithMany(p => p.FilteringInsurerCriteria)
                .HasForeignKey(d => d.InsuranceCoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__filtering__insur__290D0E62");

            entity.HasOne(d => d.Mga).WithMany(p => p.FilteringInsurerCriteria)
                .HasForeignKey(d => d.MgaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__filtering__mga_i__2A01329B");

            entity.HasOne(d => d.OperationTypeNameNavigation).WithMany(p => p.FilteringInsurerCriteria)
                .HasForeignKey(d => d.OperationTypeName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__filtering__opera__2AF556D4");
        });

        modelBuilder.Entity<FilteringMgaIc>(entity =>
        {
            entity.HasKey(e => e.InsuranceCoId).HasName("PK__filterin__406008C5A8D997DF");

            entity.ToTable("filtering_mga_ic");

            entity.Property(e => e.InsuranceCoId).HasColumnName("insurance_co_id");
            entity.Property(e => e.InsuranceCoName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("insurance_co_name");

            entity.HasMany(d => d.Drivers).WithMany(p => p.InsuranceCos)
                .UsingEntity<Dictionary<string, object>>(
                    "InsuranceCompanyDriver",
                    r => r.HasOne<Driver>().WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__insurance__drive__11BF94B6"),
                    l => l.HasOne<FilteringMgaIc>().WithMany()
                        .HasForeignKey("InsuranceCoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__insurance__insur__10CB707D"),
                    j =>
                    {
                        j.HasKey("InsuranceCoId", "DriverId");
                        j.ToTable("insurance_company_drivers");
                        j.IndexerProperty<int>("InsuranceCoId").HasColumnName("insurance_co_id");
                        j.IndexerProperty<int>("DriverId").HasColumnName("driver_id");
                    });
        });

        modelBuilder.Entity<Garage>(entity =>
        {
            entity.HasKey(e => e.GarageId).HasName("pk_garage");

            entity.ToTable("garage");

            entity.Property(e => e.GarageId).HasColumnName("garage_id");
            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.GaragingAddressCity)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("garaging_address_city");
            entity.Property(e => e.GaragingAddressState)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("garaging_address_state");
            entity.Property(e => e.GaragingAddressStreet)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("garaging_address_street");
            entity.Property(e => e.GaragingAddressZip)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("garaging_address_zip");

            entity.HasOne(d => d.AgencyClient).WithMany(p => p.Garages)
                .HasForeignKey(d => d.AgencyClientId)
                .HasConstraintName("fk_garage__agency_client");
        });

        modelBuilder.Entity<Insurance>(entity =>
        {
            entity.HasKey(e => e.InsuranceId).HasName("pk_insurance");

            entity.ToTable("insurance");

            entity.Property(e => e.InsuranceId).HasColumnName("insurance_id");
            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.InsuranceCoName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("insurance_co_name");
            entity.Property(e => e.InsuranceEffectiveDate).HasColumnName("insurance_effective_date");
            entity.Property(e => e.InsuranceExpirationDate).HasColumnName("insurance_expiration_date");
            entity.Property(e => e.InsurancePolicyNo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("insurance_policy_no");

            entity.HasOne(d => d.AgencyClient).WithMany(p => p.Insurances)
                .HasForeignKey(d => d.AgencyClientId)
                .HasConstraintName("fk_insurance__agency_client");
        });

        modelBuilder.Entity<InsuranceCompanyState>(entity =>
        {
            entity.HasKey(e => new { e.InsuranceCoId, e.StateCode });

            entity.ToTable("insurance_company_states");

            entity.Property(e => e.InsuranceCoId).HasColumnName("insurance_co_id");
            entity.Property(e => e.StateCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("state_code");

            entity.HasOne(d => d.InsuranceCo).WithMany(p => p.InsuranceCompanyStates)
                .HasForeignKey(d => d.InsuranceCoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__insurance__insur__0CFADF99");
        });

        modelBuilder.Entity<InsuredType>(entity =>
        {
            entity.HasKey(e => e.InsuredTypeId).HasName("pk_insured_type");

            entity.ToTable("insured_type");

            entity.Property(e => e.InsuredTypeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("insured_type_id");
            entity.Property(e => e.InsuredTypeDescription)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("insured_type_description");
        });

        modelBuilder.Entity<Insurer>(entity =>
        {
            entity.HasKey(e => e.InsurerId).HasName("pk_insurer");

            entity.ToTable("insurer");

            entity.Property(e => e.InsurerId).HasColumnName("insurer_id");
            entity.Property(e => e.InsurerAddress1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Insurer_Address1");
            entity.Property(e => e.InsurerAddress2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Insurer_Address2");
            entity.Property(e => e.InsurerCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Insurer_city");
            entity.Property(e => e.InsurerCoName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("insurer_co_name");
            entity.Property(e => e.InsurerEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Insurer_Email");
            entity.Property(e => e.InsurerEmailDomain)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("insurer_email_domain");
            entity.Property(e => e.InsurerPhoneNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Insurer_Phone_Number");
            entity.Property(e => e.InsurerState)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Insurer_State");
            entity.Property(e => e.InsurerType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("insurer_type");
            entity.Property(e => e.InsurerZipcode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Insurer_Zipcode");
            entity.Property(e => e.NaicNumber).HasColumnName("NAIC_Number");
            entity.Property(e => e.UnderwritersViewAll)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("underwriters_view_all");

            entity.HasMany(d => d.InsuranceCos).WithMany(p => p.Mgas)
                .UsingEntity<Dictionary<string, object>>(
                    "MgaMember",
                    r => r.HasOne<FilteringMgaIc>().WithMany()
                        .HasForeignKey("InsuranceCoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__mga_membe__insur__027D5126"),
                    l => l.HasOne<Insurer>().WithMany()
                        .HasForeignKey("MgaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__mga_membe__mga_i__01892CED"),
                    j =>
                    {
                        j.HasKey("MgaId", "InsuranceCoId").HasName("PK_mga_members_mga_and_insurer");
                        j.ToTable("mga_members");
                        j.IndexerProperty<int>("MgaId").HasColumnName("mga_id");
                        j.IndexerProperty<int>("InsuranceCoId").HasColumnName("insurance_co_id");
                    });
        });

        modelBuilder.Entity<InsurerAgent>(entity =>
        {
            entity.HasKey(e => e.InsurerAgentId).HasName("pk_insurer_agents");

            entity.ToTable("insurer_agents");

            entity.HasIndex(e => e.UserId, "IDX__insurer_agents__Index1").IsUnique();

            entity.Property(e => e.InsurerAgentId).HasColumnName("insurer_agent_id");
            entity.Property(e => e.InsurerAgentEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("insurer_agent_email");
            entity.Property(e => e.InsurerAgentFirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("insurer_agent_first_name");
            entity.Property(e => e.InsurerAgentLastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("insurer_agent_last_name");
            entity.Property(e => e.InsurerAgentPhone)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("insurer_agent_phone");
            entity.Property(e => e.InsurerAgentStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("insurer_agent_status");
            entity.Property(e => e.InsurerAgentTitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("insurer_agent_title");
            entity.Property(e => e.InsurerId).HasColumnName("insurer_id");
            entity.Property(e => e.ManagerInsurerAgentId).HasColumnName("manager_insurer_agent_id");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Insurer).WithMany(p => p.InsurerAgents)
                .HasForeignKey(d => d.InsurerId)
                .HasConstraintName("fk_insurer_agents__insurer");
        });

        modelBuilder.Entity<InsurerDocument>(entity =>
        {
            entity.HasKey(e => e.InsurerDocumentId).HasName("pk_insurer_documents");

            entity.ToTable("insurer_documents");

            entity.Property(e => e.InsurerDocumentId).HasColumnName("insurer_document_id");
            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.InsurerDocumentDate).HasColumnName("insurer_document_date");
            entity.Property(e => e.InsurerId).HasColumnName("insurer_id");
            entity.Property(e => e.QuoteId).HasColumnName("quote_id");

            entity.HasOne(d => d.AgencyClient).WithMany(p => p.InsurerDocuments)
                .HasForeignKey(d => d.AgencyClientId)
                .HasConstraintName("fk_insurer_documents__agency_client");

            entity.HasOne(d => d.Application).WithMany(p => p.InsurerDocuments)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("fk_insurer_documents__application");

            entity.HasOne(d => d.Insurer).WithMany(p => p.InsurerDocuments)
                .HasForeignKey(d => d.InsurerId)
                .HasConstraintName("fk_insurer_documents__insurer");

            entity.HasOne(d => d.Quote).WithMany(p => p.InsurerDocuments)
                .HasForeignKey(d => d.QuoteId)
                .HasConstraintName("fk_insurer_documents__quote");
        });

        modelBuilder.Entity<InsurerStateLicense>(entity =>
        {
            entity.HasKey(e => new { e.InsurerId, e.StateId }).HasName("pk_insurer_state_license");

            entity.ToTable("insurer_state_license");

            entity.Property(e => e.InsurerId).HasColumnName("insurer_id");
            entity.Property(e => e.StateId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("state_id");
            entity.Property(e => e.InsurerLicenseNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("insurer_license_number");
            entity.Property(e => e.LicenseNumberEffectiveDate).HasColumnName("license_number_effective_date");
            entity.Property(e => e.LicenseNumberExpirationDate).HasColumnName("license_number_expiration_date");

            entity.HasOne(d => d.Insurer).WithMany(p => p.InsurerStateLicenses)
                .HasForeignKey(d => d.InsurerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_insurer_state_license__insurer");
        });

        modelBuilder.Entity<LossClaim>(entity =>
        {
            entity.HasKey(e => e.ClaimId).HasName("pk_loss_claim");

            entity.ToTable("loss_claim");

            entity.Property(e => e.ClaimId).HasColumnName("claim_id");
            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.DateOfLoss).HasColumnName("date_of_loss");
            entity.Property(e => e.InsuranceCoName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("insurance_co_name");
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("policy_number");
            entity.Property(e => e.PolicyYear).HasColumnName("policy_year");
            entity.Property(e => e.TypeOfLoss)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("type_of_loss");

            entity.HasOne(d => d.AgencyClient).WithMany(p => p.LossClaims)
                .HasForeignKey(d => d.AgencyClientId)
                .HasConstraintName("fk_loss_claim__agency_client");
        });

        modelBuilder.Entity<Market>(entity =>
        {
            entity.HasKey(e => new { e.AgencyId, e.InsurerId }).HasName("pk_market");

            entity.ToTable("market");

            entity.Property(e => e.AgencyId).HasColumnName("agency_id");
            entity.Property(e => e.InsurerId).HasColumnName("insurer_id");
            entity.Property(e => e.AgencyInsurerCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("agency_insurer_code");
            entity.Property(e => e.InsurerAgentId).HasColumnName("insurer_agent_id");
            entity.Property(e => e.InsurerAssignedAgencyId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("insurer_assigned_agency_id");
            entity.Property(e => e.RelationshipActive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("relationship_active");

            entity.HasOne(d => d.Agency).WithMany(p => p.Markets)
                .HasForeignKey(d => d.AgencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_market__agency_agency");

            entity.HasOne(d => d.InsurerAgent).WithMany(p => p.Markets)
                .HasForeignKey(d => d.InsurerAgentId)
                .HasConstraintName("fk_market__insurer_agents");

            entity.HasOne(d => d.Insurer).WithMany(p => p.Markets)
                .HasForeignKey(d => d.InsurerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_market__agency_insurer");
        });

        modelBuilder.Entity<MgaOperatingState>(entity =>
        {
            entity.HasKey(e => new { e.InsuranceCoId, e.InsuranceCoStateId });

            entity.ToTable("mga_operating_states");

            entity.Property(e => e.InsuranceCoId).HasColumnName("insurance_co_id");
            entity.Property(e => e.InsuranceCoStateId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("insurance_co_state_id");

            entity.HasOne(d => d.InsuranceCo).WithMany(p => p.MgaOperatingStates)
                .HasForeignKey(d => d.InsuranceCoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mga_opera__insur__0559BDD1");
        });

        modelBuilder.Entity<NewInsurer>(entity =>
        {
            entity.HasKey(e => e.NewLeadId).HasName("pk_new_insurer");

            entity.ToTable("new_insurer");

            entity.Property(e => e.NewLeadId).HasColumnName("new_lead_id");
            entity.Property(e => e.AgencyCode)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("agency_code");
            entity.Property(e => e.AgencyId).HasColumnName("agency_id");
            entity.Property(e => e.InsurerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("insurer_name");
            entity.Property(e => e.InsurerPhone)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("insurer_phone");
            entity.Property(e => e.InsurerServicingDeptEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("insurer_servicing_dept_email");
            entity.Property(e => e.InsurerUnderwriterEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("insurer_underwriter_email");

            entity.HasOne(d => d.Agency).WithMany(p => p.NewInsurers)
                .HasForeignKey(d => d.AgencyId)
                .HasConstraintName("fk_new_insurer__agency");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("pk_note");

            entity.ToTable("note");

            entity.Property(e => e.NoteId).HasColumnName("note_id");
            entity.Property(e => e.AgencyAgentFirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("agency_agent_first_name");
            entity.Property(e => e.AgencyAgentId).HasColumnName("agency_agent_id");
            entity.Property(e => e.AgencyAgentLastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("agency_agent_last_name");
            entity.Property(e => e.AgencyId).HasColumnName("agency_id");
            entity.Property(e => e.InsurerAgentFirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("insurer_agent_first_name");
            entity.Property(e => e.InsurerAgentId).HasColumnName("insurer_agent_id");
            entity.Property(e => e.InsurerAgentLastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("insurer_agent_last_name");
            entity.Property(e => e.InsurerId).HasColumnName("insurer_id");
            entity.Property(e => e.Note1)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("note");
            entity.Property(e => e.NoteDatetime)
                .HasColumnType("datetime")
                .HasColumnName("note_datetime");
            entity.Property(e => e.QuoteId).HasColumnName("quote_id");

            entity.HasOne(d => d.AgencyAgent).WithMany(p => p.Notes)
                .HasForeignKey(d => d.AgencyAgentId)
                .HasConstraintName("fk_note__agency_agents");

            entity.HasOne(d => d.Agency).WithMany(p => p.Notes)
                .HasForeignKey(d => d.AgencyId)
                .HasConstraintName("fk_note__agency");

            entity.HasOne(d => d.InsurerAgent).WithMany(p => p.Notes)
                .HasForeignKey(d => d.InsurerAgentId)
                .HasConstraintName("fk_note__insurer_agents");

            entity.HasOne(d => d.Insurer).WithMany(p => p.Notes)
                .HasForeignKey(d => d.InsurerId)
                .HasConstraintName("fk_note__insurer");

            entity.HasOne(d => d.Quote).WithMany(p => p.Notes)
                .HasForeignKey(d => d.QuoteId)
                .HasConstraintName("fk_note__quote");
        });

        modelBuilder.Entity<PaymentPlan>(entity =>
        {
            entity.HasKey(e => e.PaymentPlanId).HasName("pk_payment_plan");

            entity.ToTable("payment_plan");

            entity.Property(e => e.PaymentPlanId).HasColumnName("Payment_Plan_Id");
            entity.Property(e => e.MonthlyCharge)
                .HasColumnType("money")
                .HasColumnName("Monthly_Charge");
            entity.Property(e => e.PaymentPlanDescription)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Payment_Plan_Description");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => new { e.PolicyNumber, e.QuoteId, e.InsurerId }).HasName("pk_policy");

            entity.ToTable("policy");

            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Policy_Number");
            entity.Property(e => e.QuoteId).HasColumnName("Quote_Id");
            entity.Property(e => e.InsurerId).HasColumnName("Insurer_Id");
            entity.Property(e => e.AgencyClientId).HasColumnName("Agency_Client_Id");
            entity.Property(e => e.AgencyId).HasColumnName("Agency_Id");
            entity.Property(e => e.PolicyEndDate).HasColumnName("Policy_End_Date");
            entity.Property(e => e.PolicyStartDate).HasColumnName("Policy_Start_Date");

            entity.HasOne(d => d.AgencyClient).WithMany(p => p.Policies)
                .HasForeignKey(d => d.AgencyClientId)
                .HasConstraintName("fk_policy__agency_client");

            entity.HasOne(d => d.Agency).WithMany(p => p.Policies)
                .HasForeignKey(d => d.AgencyId)
                .HasConstraintName("fk_policy__agency");

            entity.HasOne(d => d.Insurer).WithMany(p => p.Policies)
                .HasForeignKey(d => d.InsurerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_policy__insurer");

            entity.HasOne(d => d.Quote).WithMany(p => p.Policies)
                .HasForeignKey(d => d.QuoteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_policy__quote");
        });

        modelBuilder.Entity<PolicyEndorsement>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("policy_endorsement");

            entity.HasIndex(e => new { e.EndorsementId, e.EndorsementStatus }, "CIDX__dbo__policy_endorsement__Index1").IsClustered();

            entity.HasIndex(e => new { e.PolicyNumber, e.QuoteId }, "IDX__dbo__policy_endorsement__Index1");

            entity.Property(e => e.AgencyClientId).HasColumnName("Agency_Client_Id");
            entity.Property(e => e.AgencyId).HasColumnName("Agency_Id");
            entity.Property(e => e.EndorsementId).HasColumnName("Endorsement_Id");
            entity.Property(e => e.EndorsementRequestDate).HasColumnName("Endorsement_Request_Date");
            entity.Property(e => e.EndorsementStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Endorsement_Status");
            entity.Property(e => e.EndorsementStatusDate)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Endorsement_Status_Date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.InsurerId).HasColumnName("Insurer_Id");
            entity.Property(e => e.OperationType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Operation_Type");
            entity.Property(e => e.PolicyChangeEffectiveDate).HasColumnName("Policy_Change_Effective_Date");
            entity.Property(e => e.PolicyEndDate).HasColumnName("Policy_End_Date");
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Policy_Number");
            entity.Property(e => e.PolicyStartDate).HasColumnName("Policy_Start_Date");
            entity.Property(e => e.QuoteId).HasColumnName("Quote_Id");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("pk_promotion");

            entity.ToTable("promotion");

            entity.Property(e => e.PromotionId).HasColumnName("Promotion_Id");
            entity.Property(e => e.PromotionAmount)
                .HasColumnType("money")
                .HasColumnName("Promotion_Amount");
            entity.Property(e => e.PromotionDescription)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Promotion_Description");
            entity.Property(e => e.PromotionDiscount).HasColumnName("Promotion_Discount");
            entity.Property(e => e.PromotionEndDate).HasColumnName("Promotion_End_Date");
            entity.Property(e => e.PromotionStartDate).HasColumnName("Promotion_Start_Date");
            entity.Property(e => e.PromotionType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Promotion_Type");
        });

        modelBuilder.Entity<Quote>(entity =>
        {
            entity.HasKey(e => e.QuoteId).HasName("pk_quote");

            entity.ToTable("quote");

            entity.Property(e => e.QuoteId).HasColumnName("quote_id");
            entity.Property(e => e.AgencyId).HasColumnName("agency_id");
            entity.Property(e => e.AgencyQuoteStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("agency_quote_status");
            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.InsurerAgentId).HasColumnName("insurer_agent_id");
            entity.Property(e => e.InsurerId).HasColumnName("insurer_id");
            entity.Property(e => e.ProvidedCoverageType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("provided_coverage_type");
            entity.Property(e => e.QuoteBound)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("quote_bound");
            entity.Property(e => e.QuoteBoundDate).HasColumnName("quote_bound_date");
            entity.Property(e => e.QuoteExpirationDate)
                .HasColumnType("datetime")
                .HasColumnName("quote_expiration_date");
            entity.Property(e => e.QuoteReceivedDate)
                .HasColumnType("datetime")
                .HasColumnName("quote_received_date");
            entity.Property(e => e.QuoteStartDate)
                .HasColumnType("datetime")
                .HasColumnName("quote_start_date");
            entity.Property(e => e.QuoteUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("quote_update_date");
            entity.Property(e => e.RejectionReason)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("rejection_reason");

            entity.HasOne(d => d.Agency).WithMany(p => p.Quotes)
                .HasForeignKey(d => d.AgencyId)
                .HasConstraintName("fk_quote__agency");

            entity.HasOne(d => d.Application).WithMany(p => p.Quotes)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("fk_quote__application");

            entity.HasOne(d => d.InsurerAgent).WithMany(p => p.Quotes)
                .HasForeignKey(d => d.InsurerAgentId)
                .HasConstraintName("fk_quote__insurer_agents");

            entity.HasOne(d => d.Insurer).WithMany(p => p.Quotes)
                .HasForeignKey(d => d.InsurerId)
                .HasConstraintName("fk_quote__insurer");
        });

        modelBuilder.Entity<Safety>(entity =>
        {
            entity.HasKey(e => e.SafetyId).HasName("pk_safety");

            entity.ToTable("safety");

            entity.Property(e => e.SafetyId)
                .ValueGeneratedOnAdd()
                .HasColumnName("safety_id");
            entity.Property(e => e.SafetyDescription)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("safety_description");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("pk_vehicle");

            entity.ToTable("vehicle");

            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.VehicleGvw)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("vehicle_gvw");
            entity.Property(e => e.VehicleMake)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("vehicle_make");
            entity.Property(e => e.VehicleModel)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("vehicle_model");
            entity.Property(e => e.VehicleTypeId).HasColumnName("vehicle_type_id");
            entity.Property(e => e.VehicleValue)
                .HasColumnType("money")
                .HasColumnName("vehicle_value");
            entity.Property(e => e.VehicleVinNumber)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("vehicle_vin_number");
            entity.Property(e => e.VehicleYear).HasColumnName("vehicle_year");

            entity.HasOne(d => d.AgencyClient).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.AgencyClientId)
                .HasConstraintName("fk_vehicle__agency_client");

            entity.HasOne(d => d.VehicleType).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.VehicleTypeId)
                .HasConstraintName("fk_vehicle__vehicle_type");

            entity.HasMany(d => d.Safeties).WithMany(p => p.Vehicles)
                .UsingEntity<Dictionary<string, object>>(
                    "VehicleSafety",
                    r => r.HasOne<Safety>().WithMany()
                        .HasForeignKey("SafetyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_vehicle_safety__safety"),
                    l => l.HasOne<Vehicle>().WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_vehicle_safety__vehicle"),
                    j =>
                    {
                        j.HasKey("VehicleId", "SafetyId").HasName("pk_vehicle_safety");
                        j.ToTable("vehicle_safety");
                        j.IndexerProperty<int>("VehicleId").HasColumnName("vehicle_id");
                        j.IndexerProperty<byte>("SafetyId").HasColumnName("safety_id");
                    });
        });

        modelBuilder.Entity<VehicleEndorsement>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("vehicle_endorsement");

            entity.HasIndex(e => new { e.EndorsementId, e.EndorsementStatus }, "CIDX__dbo__vehicle_endorsement__Index1").IsClustered();

            entity.Property(e => e.AgencyClientId).HasColumnName("agency_client_id");
            entity.Property(e => e.EndorsementId).HasColumnName("Endorsement_Id");
            entity.Property(e => e.EndorsementRequestDate).HasColumnName("Endorsement_Request_Date");
            entity.Property(e => e.EndorsementStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Endorsement_Status");
            entity.Property(e => e.EndorsementStatusDate)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Endorsement_Status_Date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.OperationType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Operation_Type");
            entity.Property(e => e.PolicyChangeEffectiveDate).HasColumnName("Policy_Change_Effective_Date");
            entity.Property(e => e.VehicleGvw)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("vehicle_gvw");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
            entity.Property(e => e.VehicleMake)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("vehicle_make");
            entity.Property(e => e.VehicleModel)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("vehicle_model");
            entity.Property(e => e.VehicleTypeId).HasColumnName("vehicle_type_id");
            entity.Property(e => e.VehicleValue)
                .HasColumnType("money")
                .HasColumnName("vehicle_value");
            entity.Property(e => e.VehicleVinNumber)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("vehicle_vin_number");
            entity.Property(e => e.VehicleYear).HasColumnName("vehicle_year");
        });

        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.HasKey(e => e.VehicleTypeId).HasName("pk_vehicle_type");

            entity.ToTable("vehicle_type");

            entity.Property(e => e.VehicleTypeId).HasColumnName("vehicle_type_id");
            entity.Property(e => e.VehicleTypeDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("vehicle_type_description");
        });
        modelBuilder.HasSequence("Endorsement_Id")
            .HasMin(1L)
            .IsCyclic();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
