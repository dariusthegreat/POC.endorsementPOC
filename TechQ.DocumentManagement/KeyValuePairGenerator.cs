using System.Diagnostics.Contracts;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using TechQ.Entities;
using TechQ.Entities.Models;


// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace TechQ.DocumentManagement;

public partial class KeyValuePairGenerator : IKeyValuePairGenerator
{
	private readonly IInsuranceDbContext               _dbContext;
	private readonly Lazy<AgencyClient>                _lazyLoadedClient;
	private readonly Lazy<AgencyAgent>                 _lazyLoadedAgencyAgent;
	private readonly Lazy<Agency>                      _lazyLoadedAgency;
	private readonly Lazy<Insurer>                     _lazyLoadedInsurer;
	private readonly Lazy<Policy>                      _lazyLoadedPolicy;
	private readonly Lazy<Vehicle>                     _lazyLoadedVehicle;
	private readonly Lazy<long>                        _lazyLoadedMaxApplicationId;
	private readonly Lazy<CoverageRequest>             _lazyLoadedCoverageRequest;
	private readonly Lazy<Market>                      _lazyLoadedInsurerMarket;
	private readonly Lazy<Garage>                      _lazyLoadedGarage;
	private readonly Lazy<IDictionary<string, string>> _lazyLoadedKeyValuePairs;
	

	public long		ClientId	{ get; }
	public long		AgencyId	{ get; }
	public long		AgentId		{ get; }
	public long?	VehicleId	{ get; }

	public  AgencyClient                Client					=> _lazyLoadedClient.Value;
	public  AgencyAgent                 Agent					=> _lazyLoadedAgencyAgent.Value;
	public  Agency                      Agency					=> _lazyLoadedAgency.Value;
	private Insurer						InsurerRecord			=> _lazyLoadedInsurer.Value;
	private Policy						PolicyRecord			=> _lazyLoadedPolicy.Value;
	private Vehicle						VehicleRecord			=> _lazyLoadedVehicle.Value;
	private long						MaxApplicationId		=> _lazyLoadedMaxApplicationId.Value;
	private CoverageRequest				CoverageRequestRecord	=> _lazyLoadedCoverageRequest.Value;
	private Market						InsurerMarket			=> _lazyLoadedInsurerMarket.Value;
	private Garage						FirstGarageRecord		=> _lazyLoadedGarage.Value;
	public  IDictionary<string, string> KeyValuePairs			=> _lazyLoadedKeyValuePairs.Value;

	public KeyValuePairGenerator(IInsuranceDbContext dbContext, long clientId, long agencyId, long agentId, long? vehicleId=null)
	{
		_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		
		ClientId	= clientId;
		AgencyId	= agencyId;
		AgentId		= agentId;
		VehicleId	= vehicleId;

		_lazyLoadedClient			= new(GetAgencyClient);
		_lazyLoadedAgencyAgent		= new(_dbContext.AgencyAgents.Single(x => x.AgencyAgentId		== AgentId				 ));
		_lazyLoadedAgency			= new(_dbContext.Agencies.Single(x => x.AgencyId				== Client.AgencyId		 ));
		_lazyLoadedPolicy			= new(_dbContext.Policies.First(x => x.AgencyClientId			== clientId				 ));
		_lazyLoadedInsurer			= new(_dbContext.Insurers.Single(x => x.InsurerId				== PolicyRecord.InsurerId));
		_lazyLoadedMaxApplicationId = new(GetMaxApplicationId);
		_lazyLoadedCoverageRequest	= new(_dbContext.CoverageRequests.First(x => x.ApplicationId	== MaxApplicationId		 ));
		_lazyLoadedInsurerMarket	= new(_dbContext.Markets.Where(x => x.InsurerId == PolicyRecord.InsurerId).SingleOrDefault(x => x.AgencyId == Agency.AgencyId));
		_lazyLoadedVehicle			= new(GetVehicleRecord);
		_lazyLoadedGarage			= new(_dbContext.Garages.FirstOrDefault(x => x.AgencyClientId == ClientId));
		_lazyLoadedKeyValuePairs	= new(GetPairs);
	}

	private AgencyClient GetAgencyClient()
	{
		var client = _dbContext.AgencyClients.Single(x => x.AgencyClientId == ClientId);

		if (_dbContext is InsuranceDbContext insuranxeDbContext)
			insuranxeDbContext.Entry(client).Collection(x => x.Commodities).Load();

		return client;
	}

	public IDictionary<string, string> GetPairs() => typeof(KeyValuePairGenerator)
								.GetProperties(BindingFlags.Public | BindingFlags.Instance)
								.SelectMany(property => property.GetCustomAttributes().Select(attribute => new { property, attribute }))
								.Select(x => new { x.property, x.attribute, propertyValue = x.property.GetValue(this) })
								.SelectMany(row => row.attribute switch
							{
								PdfFieldAttribute pdfFieldAttribute => [new KeyValuePair<string, string>(pdfFieldAttribute.FieldName, (string)row.propertyValue)],
								PdfFieldCollectionAttribute => ((IEnumerable<KeyValuePair<string, string>>)row.propertyValue),
								_ => []
							})
							.OrderBy(x => x.Key, new KeyNameComparer())
							.ToDictionary(x => x.Key, x => x.Value);


	public static string[] GetFieldNames(IInsuranceDbContextProvider insuranceDbContextProvider = null)
	{
		insuranceDbContextProvider ??= new InsuranceDbDummyDataGenerator();

		var dbContext = insuranceDbContextProvider.GetDbContext();
		var instance = new KeyValuePairGenerator(dbContext, insuranceDbContextProvider.ClientId, insuranceDbContextProvider.AgencyId, insuranceDbContextProvider.AgentId, insuranceDbContextProvider.VehicleId);

		return instance.FieldNames;
	}

	
	public string[] FieldNames =>
		(
			from pair in typeof(KeyValuePairGenerator)
								.GetProperties(BindingFlags.Public | BindingFlags.Instance)
								.SelectMany(property => property.GetCustomAttributes().Select(attribute => new { property, attribute }))

			from name in pair.attribute switch
			{
				PdfFieldAttribute pdfField  => [pdfField.FieldName],
				PdfFieldCollectionAttribute => (((IEnumerable<KeyValuePair<string, string>>)pair.property.GetValue(this))!).Select(x => x.Key),
				_                           => []
			}

			orderby name
			select name
		).ToArray();


	#region field logic


#pragma warning disable CA1822 // method/property can be made static: we know, but we need these to be instance properties, so they get picked up when the properties are gathered and computed

	[PdfField("PolicyType")]               public string PolicyType => "Commercial";
	
	// ReSharper disable StringLiteralTypo
	// ReSharper disable InconsistentNaming
	[PdfField("DateMMDDYYYY")]             public string DateMMDDYYYY => $"{DateTime.Today:MM-dd-yyyy}";
	
	[PdfField("AL_ADDLINSD")]              public string AL_ADDLINSD => ""; // todo: no implementation provided
	[PdfField("AL_SUBRWVD")]               public string AL_SUBRWVD => "";  // todo: no implementation provided
	[PdfField("YN")]                       public string YN => "N";
	// ReSharper restore InconsistentNaming
	// ReSharper restore StringLiteralTypo
	[PdfField("InsuredName")]              public string InsuredName => Client.DbaName;
	[PdfField("InsuredAddress1")]          public string InsuredAddress1 => Client.MailingAddressStreet;
	[PdfField("InsuredAddress2")]          public string InsuredAddress2 => string.Empty;
	[PdfField("InsuredCity")]              public string InsuredCity => Client.MailingAddressCity;
	[PdfField("InsuredState")]             public string InsuredState => Client.MailingAddressState;
	[PdfField("InsuredZip")]               public string InsuredZip => Client.MailingAddressZip;
	[PdfField("YearsInsurance")]           public string YearsInsurance => $"{Client.YearsOfPriorInsurance ?? 0}";
	[PdfField("Years Prior Ins")]		   public string YearsPriorInsurance => YearsInsurance; // todo: duplicate
	[PdfField("OperationType")]            public string OperationType => Client.TypeOfOperation;
	[PdfField("DOTNum")]                   public string DotNumber => Client.UsDotNumber;
	[PdfField("US DOT")]				   public string DotNumberDuplicated => DotNumber; // todo: duplicated
	[PdfField("MCS90")]                    public string Mcs90Number => Client.Mcs90Number;
	[PdfField("MC91")]                     public string Mcs91Number => Client.Mc91Number;
	[PdfField("ClientTaxId")]              public string ClientTaxId => Client.CompanyTaxId;
	[PdfField("PhysicalAddress")]          public string PhysicalAddress => Client.PhysicalAddressStreet;
	[PdfField("PhysicalCity")]             public string PhysicalCity => Client.PhysicalAddressCity;
	[PdfField("PhysicalState")]            public string PhysicalState => Client.PhysicalAddressState;
	[PdfField("PhysicalZip")]              public string PhysicalZip => Client.PhysicalAddressZip;
	[PdfField("ClientEmail")]              public string ClientEmail => Client.CompanyEmail;
	[PdfField("Email Address")]			   public string EmailAddress => ClientEmail; // todo: duplicate
	[PdfField("ClientName")]               public string ClientName => $"{Client.OwnerFirstName} {Client.OwnerLastName}";
	[PdfField("ClientPhone")]              public string ClientPhone => Client.CompanyPhone;
	[PdfField("InsuredName_LGN")]          public string InsuredNameLgn => $"{Client.LegalCompanyName}";
	[PdfField("ClientPhoneBusiness")]      public string ClientPhoneBusiness	=> Client.CompanyPhoneType == "B" ? "X" : "";
	[PdfField("ClientPhoneCell")]          public string ClientPhoneCell		=> Client.CompanyPhoneType == "C" ? "X" : "";
	[PdfField("ClientPhoneHome")]          public string ClientPhoneHome		=> Client.CompanyPhoneType == "H" ? "X" : "";
	[PdfField("InsuredPhone")]             public string InsuredPhone			=> Client.CompanyPhone;
	[PdfField("Telephone")]				public string Telephone				=> InsuredPhone; // todo: duplicate
	[PdfField("OwnerDOB")]					public string OwnerDob				=> Client.OwnerDoB?.ToShortDateString();
	

	[PdfField("AgencyFax")]                public string AgencyFax => Agency.AgencyFax;
	[PdfField("AgencyEmail")]              public string AgencyEmail => Agency.PrimaryContactEmail;
	[PdfField("AgencyAddress1")]           public string AgencyAddress1 => Agency.AgencyMailingAddress1;
	[PdfField("AgencyAddress2")]           public string AgencyAddress2 => Agency.AgencyMailingAddress2;
	[PdfField("AgencyName")]               public string AgencyName => Agency.AgencyName;
	[PdfField("AgentName")]                public string AgentName => $"{Agency.PrimaryContactFirstName} {Agency.PrimaryContactLastName}";
	[PdfField("AgencyAddress")]            public string AgencyAddress => Agency.AgencyMailingAddress1;
	[PdfField("AgencyAddressFull")]        public string AgencyAddressFull => $"{AgencyAddress1} {AgencyAddress2} {Agency.AgencyMailingCity} {Agency.AgencyMailingState}";
	[PdfField("AgencyCity")]               public string AgencyCity => Agency.AgencyMailingCity;
	[PdfField("AgencyState")]              public string AgencyState => Agency.AgencyMailingState;
	[PdfField("AgencyZip")]                public string AgencyZip => Agency.AgencyMailingZipcode;
	[PdfField("AgencyPhone")]              public string AgencyPhone => Agency.PrimaryContactPhone;
	[PdfField("AgencyNPN")]                public string AgencyNpn => Agency.AgencyNpn?.ToString() ?? "";
	[PdfField("Email")]                    public string AgencyPrimaryContactEmailAddress => Agency.PrimaryContactEmail;
	
	[PdfField("CompanyName")]              public string CompanyName => InsurerRecord.InsurerCoName;
	[PdfField("CompanyNumber")]            public string CompanyNumber => InsurerRecord.InsurerPhoneNumber;
	[PdfField("InsurerName")]              public string InsurerName => InsurerRecord.InsurerCoName;
	// ReSharper disable once InconsistentNaming
	[PdfField("NAICNumber")]               public string NAICNumber => InsurerRecord.NaicNumber.ToString();
	
	[PdfField("PolicyNumber")]             public string PolicyNumber => PolicyRecord.PolicyNumber;
	[PdfField("PolicyEffectiveDate")]      public string PolicyEffectiveDate => PolicyRecord.PolicyStartDate?.ToString("MM-dd-yyyy") ?? "policy record not found";
	[PdfField("PolicyExpirationDate")]     public string PolicyExpirationDate => PolicyRecord.PolicyEndDate?.ToString("MM-dd-yyyy")  ?? "policy record not found";
	[PdfField("AL_PolicyNumber")]          public string AlPolicyNumber => PolicyNumber;
	[PdfField("AL_PolicyEffDate")]         public string AlPolicyEffectiveDate => PolicyEffectiveDate;
	[PdfField("AL_PolicyExpDate")]         public string AlPolicyExpirationDate => PolicyExpirationDate;
	
	
	[PdfField("InsurerAssignedAgencyId")]  public string InsurerAssignedAgencyId => InsurerMarket?.InsurerAssignedAgencyId;
	
	[PdfField("HiredAuto")]                public string HiredAutoCheckboxValue	=> CoverageRequestRecord.HiredAuto		is "Y" or "1" ? "X" : "";
	[PdfField("NonownedAuto")]             public string NonOwnedAutoCheckboxValue => CoverageRequestRecord.NonownedAuto	is "Y" or "1" ? "X" : "";
	[PdfField("AutoLiabilityLimit")]       public string AutoLiabilityLimit		=> CoverageRequestRecord.AutoLiabilityLimit;
	[PdfField("AutoLiabilityDeductible")]  public string AutoLiabilityDeductible	=> CoverageRequestRecord.AutoLiabilityDeductible;
	[PdfField("UninsuredMotorist")]		public string UninsuredMotorist			=> CoverageRequestRecord.UninsuredMotorist;
	[PdfField("CargoLimit")]               public string CargoLimit				=> CoverageRequestRecord.CargoLimit;
	[PdfField("CargoDeductible")]          public string CargoDeductible			=> CoverageRequestRecord.CargoDeductible;
	[PdfField("MedicalPayment")]           public string MedicalPayment			=> CoverageRequestRecord.MedicalPayment;
	[PdfField("PhysicalDamage")]			public string PhysicalDamage			=> CoverageRequestRecord.PhysicalDamageCoverage;
	[PdfField("PhysicalDamageDeductible")] public string PhysicalDamageDeductible	=> CoverageRequestRecord.PhysicalDamageDeductible;

	[PdfField("TrailerInterchange")]		public string TrailerInterchange		=> CoverageRequestRecord.TrailerInterchangeCoverage;
	[PdfField("ReeferBreakdownYes")]		public string ReeferBreakdownYes		=> (ReeferCoverageAmount is		> 0)	? "X" : "";
	[PdfField("ReeferBreakdownNo")]		public string ReeferBreakdownNo			=> (ReeferCoverageAmount is not > 0)	? "X" : "";

	private decimal? ReeferCoverageAmount => ParseDollarAmount(CoverageRequestRecord.ReeferCoverage);
	

	//#warning make sure to finalize the following fields:
	[PdfField("PhysicalDamageTrailers")]   public string PhysicalDamageTrailers => "???";                                            // todo: obtain the logic to populate this field
	[PdfField("UMPropertyDamage")]         public string UmPropertyDamage => "???";                                                  // todo: obtain the logic to populate this field
	[PdfField("UMBodilyInjury")]           public string UmBodilyInjury => "???";                                                    // todo: obtain the logic to populate this field 
	
	

	[PdfField("GarageStreet")]             public string GarageStreet => FirstGarageRecord.GaragingAddressStreet;
	[PdfField("GarageCity")]               public string GarageCity => FirstGarageRecord.GaragingAddressCity;
	[PdfField("GarageState")]              public string GarageState => FirstGarageRecord.GaragingAddressState;
	[PdfField("GarageZip")]                public string GarageZip => FirstGarageRecord.GaragingAddressZip;

	[PdfField("Garaging Address")]         public string GarageStreetDuplicated => GarageStreet;	// todo: duplicated
	[PdfField("Garaging City")]            public string GarageCityDuplicated => GarageCity;        // todo: duplicated
	[PdfField("Garaging State")]           public string GarageStateDuplicated => GarageState;      // todo: duplicated
	[PdfField("Garaging Zip")]             public string GarageZipDuplicated => GarageZip;          // todo: duplicated





	[PdfField("VehicleYear")]              public string VehicleYear => VehicleRecord.VehicleYear.ToString();
	[PdfField("VehicleMakeModel")]         public string VehicleMakeModel => $"{VehicleRecord.VehicleMake} {VehicleRecord.VehicleModel}";
	[PdfField("VIN")]                      public string VehicleVin => VehicleRecord.VehicleVinNumber;

	[PdfField("ForHireYes")]				public string ForHireChecked		=> Client.CarrierOperation == "For Hire"		? "X" : "";
	[PdfField("NonTruckingYes")]			public string NonTruckingChecked	=> Client.CarrierOperation == "Not For Hire"	? "X" : "";
	[PdfField("PrivateYes")]				public string PrivateChecked		=> Client.CarrierOperation == "Private"			? "X" : "";

	[PdfField("90% App - Radius - less than 100")]		public string NinetyPercentRadiusLessThan100  => Client.RadiusOfOperation == "100"									? "100%" : "";
	[PdfField("90% App - Radius - 100 - 200")]			public string NinetyPercentRadius100To200 => Client.RadiusOfOperation == "100200"									? "100%" : "";
	[PdfField("90% App - Radius - 200 - 500")]			public string NinetyPercentRadius200To500 => Client.RadiusOfOperation == "200500"									? "100%" : "";
	[PdfField("90% App - Radius - 500+")]				public string NinetyPercentRadius500Plus => Client.RadiusOfOperation == "500Plus"									? "100%" : "";
	[PdfField("90% App - Radius - 12 Western States")] public string NinetyPercentRadius12WesternStates => Radius12WesternStatesRegex.IsMatch(Client.RadiusOfOperation)	? "100%" : "";
	[PdfField("90% App - Radius - unlimited")]			public string NinetyPercentRadiusUnlimited => Client.RadiusOfOperation == "Unlimited48St"							? "100%" : "";


	private static readonly Lazy<Regex> s_radius12WesternStatesRegex = new(() => new Regex(@"^\s*(12\s*West(ern)?\s*St(ates)?)\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase));
	private static Regex Radius12WesternStatesRegex => s_radius12WesternStatesRegex.Value;


	private static decimal? ParseDollarAmount(string text)
	{
		if(text== null) return null;

		var match = DollarAmountRegex.Match(text);

		return match.Success
			? Convert.ToDecimal(match.Groups["amount"].Value) * (match.Groups["thousand"].Success ? 1000 : 1)
			: null;
	}

	private static readonly Lazy<Regex> s_dollarAmountRegex = new(() => new Regex(@"^\s*\$(?<amount>[0-9]+)(?<thousand>K)?\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase));
	private static Regex DollarAmountRegex => s_dollarAmountRegex.Value;

	


	[PdfFieldCollection] public IDictionary<string, string> VehicleFields
	{
		get
		{
			return _dbContext.Vehicles
				.Where(x => x.AgencyClientId == ClientId)
				.SelectMany(GetKeyValuePairs)
				.ToDictionary(x => x.Key, x => x.Value);

			static IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs(Vehicle item, int index)
			{
				yield return new($"Veh{index + 1}", $"{index + 1}");
				yield return new($"VehYear{index + 1}", $"{item.VehicleYear}");
				yield return new($"VehMake{index + 1}", item.VehicleMake);
				yield return new($"VehModel{index + 1}", item.VehicleModel);
				yield return new($"VehBodyType{index + 1}", item.VehicleType?.ToString());
				yield return new($"VehGVW{index + 1}", item.VehicleGvw);
				yield return new($"VIN{index + 1}", item.VehicleVinNumber);
				yield return new($"VehValue{index + 1}", $"{item.VehicleValue??0}"); // todo: what's the default when value is null?
			}
		}
	}

	[PdfFieldCollection] public IDictionary<string, string> GarageFields
	{
		get
		{
			return _dbContext.Garages
				.Where(x => x.AgencyClientId == ClientId)
				.SelectMany(GetKeyValuePairs)
				.ToDictionary(x => x.Key, x => x.Value);

			static IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs(Garage item, int index)
			{
				yield return new($"GarageStreet{index + 1}", item.GaragingAddressStreet);
				yield return new($"GarageCity{index + 1}", item.GaragingAddressCity);
				yield return new($"GarageState{index + 1}", item.GaragingAddressState);
				yield return new($"GarageZip{index + 1}", item.GaragingAddressZip);
			}
		}
	}

	
	[PdfField("AgentSignature")]
	public string AgentSignature
	{
		get
		{
			//	"agency_agents.Agency_Agent_Signature or agency.Primary_Contact_Signature"
			//var agencyAgentSignature = Agent.signature;
			// ReSharper disable once ArrangeAccessorOwnerBody
			return "";
		}
	}

	[PdfFieldCollection] public IDictionary<string, string> YesNoFields => Enumerable.Range(1, 100).ToDictionary(x => $"YN{x}", _ => "N");

	// todo: do we still need these?
	[PdfFieldCollection] public IDictionary<string, string> CoverageRequestFields
	{
		get
		{
			return _dbContext.CoverageRequests
				.Where(x => x.AgencyClientId == ClientId)
				.SelectMany(GetKeyValuePairs)
				.ToDictionary(x => x.Key, x => x.Value);

			static IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs(CoverageRequest item, int index)
			{
				var suffix = CreateDoubleLetterSuffix(index);
				yield return new($"Vehicle_HiredAutosIndicator_{suffix}", item.HiredAuto);
				yield return new($"Vehicle_NonOwnedAutosIndicator_{suffix}", item.NonownedAuto);
				yield return new($"Vehicle_CombinedSingleLimit_EachAccidentAmount_{suffix}", $"{Parse(item.AutoLiabilityLimit):N0}");
			}
		}
	}

	[Pure]
	private static string CreateDoubleLetterSuffix(int index)
	{
		if (index<0 || index >= 26 * 26) throw new ArgumentOutOfRangeException(nameof(index), index, "expected range: [0, 26*26-1]");
		
		return index < 26
			? $"{(char)('A' + index % 26)}"
			: $"{(char)('A' + (index / 26) - 1)}{(char)('A' + index % 26)}";
	}

	private static readonly Regex s_priceRegex = MyRegex();

	[Pure]
	private static decimal Parse(string text)
	{
		try
		{
			var match = s_priceRegex.Match(text);
			if (!match.Success) throw new ArgumentException($"invalid price amount: {text}");
			var amount = Convert.ToDecimal(match.Groups["amount"].Value);
			var multiples = !match.Groups["multiples"].Success
				? 1
				: match.Groups["multiples"].Value switch
				{
					"K" => 1000,
					"M" => 1000 * 1000,
					_ => throw new NotSupportedException($"invalid multiple suffix: {match.Groups["multiple"]}")
				};

			return amount * multiples;
		}
		catch
		{
			return 0;
		}
	}

	[PdfFieldCollection] public IDictionary<string, string> InsurersFields
	{
		get
		{
			var insurerRecords = _dbContext.Quotes
				.Where(x => x.ApplicationId == MaxApplicationId)
				.Where(x => x.QuoteBound is "Y" or "1")
				.Select(x => x.InsurerId)
				.Distinct()
				.OrderBy(x => x)
				.Join(_dbContext.Insurers, id => id, x => x.InsurerId, (_, insurer) => insurer)
				.ToArray();

			return insurerRecords.SelectMany(GetKeyValuePairs).ToDictionary(x => x.Key, x => x.Value);

			static IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs(Insurer item, int index)
			{
				var suffix = CreateDoubleLetterSuffix(index);
				yield return new($"Insurer{suffix}", item.InsurerCoName);
				yield return new($"NAICNumber{suffix}", item.NaicNumber?.ToString());
			}
		}
	}

	[PdfFieldCollection] public IDictionary<string, string> DriverFields
	{
		get
		{
			return _dbContext.Drivers
				.Where(x => x.AgencyClientId == ClientId)
				.SelectMany(GetKeyValuePairs)
				.ToDictionary(x => x.Key, x => x.Value);

			static IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs(Driver item, int index)
			{
				yield return new($"Driver{index + 1}", $"{index + 1}");
				yield return new($"DriverName{index + 1}", $"{item.DriverFirstName} {item.DriverMiddleName} {item.DriverLastName}");
				yield return new($"DriverFirstName{index + 1}", item.DriverLastName);
				yield return new($"DriverLastName{index + 1}", item.DriverLastName);
				yield return new($"DriverDLNumber{index + 1}", item.DriverLicenseNumber);
				yield return new($"DriverDOB{index + 1}", $"{item.DriverDob}");
				yield return new($"DriverStateLicense{index + 1}", item.DriverLicenseState);
				yield return new($"HireDate{index + 1}", index==0 ? "Owner" : "Driver");
				yield return new($"DLClass{index + 1}", item.DriverLicenseClass);
				yield return new($"DriverYearExp{index + 1}", $"{item.GetDriverYearsOfExperience()}");
			}
		}
	}

	[PdfFieldCollection] public IDictionary<string, string> ApplicationFields
	{
		get
		{
			return _dbContext.Applications
				.Where(x => x.AgencyClientId == ClientId)
				.SelectMany(GetKeyValuePairs)
				.ToDictionary(x => x.Key, x => x.Value);

			static IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs(Application item, int index)
			{
				var suffix = CreateDoubleLetterSuffix(index);
				yield return new($"Policy_AutomobileLiability_EffectiveDate_{suffix}", item.ApplicationStartDate.ToString("MM-dd-yyyy"));
				yield return new($"Policy_AutomobileLiability_ExpirationDate_{suffix}", item.ApplicationStartDate.ToString("MM-dd-yyyy"));
			}
		}
	}

	[PdfFieldCollection] public Dictionary<string,string> InsuranceFields
	{
		get
		{
			return new(_dbContext.Insurances
				.Where(x => x.AgencyClientId == ClientId)
				.SelectMany(GetKeyValuePairs));

			static IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs(Insurance item, int index)
			{
				yield return new($"PriorIns{index + 1}", item.InsuranceCoName);
				yield return new($"PriorInsEffective{index + 1}", item.InsuranceEffectiveDate?.ToShortDateString());
				yield return new($"PriorInsExpiration{index + 1}", item.InsuranceExpirationDate?.ToShortDateString());
				yield return new($"PriorInsPolicyNo{index + 1}", item.InsurancePolicyNo);
			}
		}
	}


	[PdfFieldCollection] public Dictionary<string, string> CommoditiesFields => ((InsuranceDbContext)_dbContext).AgencyClients
																					.Where(x => x.AgencyClientId==ClientId)
																					.Include(x => x.Commodities)
																					.Single()
																					.Commodities
																					.Select((commodity, index) => new { commodity, index })
																					.ToDictionary(x => $"Commodities{x.index + 1}", x => x.commodity.CommodityDescription);

	
	[GeneratedRegex(@"^\$?(?<amount>[0-9]+)(<multiples>K|M)?", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
	private static partial Regex MyRegex();

	
	private Vehicle GetVehicleRecord() => (
												from vehicle in _dbContext.Vehicles
												join coverageRequest in _dbContext.CoverageRequests on vehicle.VehicleId equals coverageRequest.VehicleId
												where coverageRequest.NonownedAuto == "N"
												where VehicleId == null || vehicle.VehicleId == VehicleId
												select vehicle
											).FirstOrDefault();


	private long GetMaxApplicationId() => _dbContext.Applications.Where(x => x.AgencyClientId == ClientId).Max(x => x.ApplicationId);



	#endregion

#pragma warning restore CA1822
}