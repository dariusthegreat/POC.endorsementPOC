using ImpromptuInterface;
using TechQ.Core.Extensions;
using TechQ.Entities;
using TechQ.Entities.Models;

namespace TechQ.DocumentManagement;

public class InsuranceDbDummyDataGenerator : IInsuranceDbContextProvider
{
	public long   ClientId             { get; set; } = 1L;
	public long   AgencyId			   { get; set; } = 2L;
	public long   AgentId			   { get; set; } = 1L;
	public long?  VehicleId			   { get; set; }
	public int    CoverageRequestCount { get; set; } = 20;
	public int    QuotesCount          { get; set; } = 20;
	public int    InsurersCount        { get; set; } = 4;
	public int    DriverCount          { get; set; } = 60;
	public int    GaragesCount         { get; set; } = 20;
	public int    VehiclesCount        { get; set; } = 80;
	
	public IInsuranceDbContext GetDbContext()
	{
		return new
			   {
				   AgencyClients    = new[] 
				   { 
					   new AgencyClient 
					   { 
						   AgencyClientId = ClientId, 
						   AgencyId   = 1,
						   DbaName = "Jerde Group",
						   AgencyAgentId = 1,
						   CompanyEmail = "kurtis.kiehn@example.net",
						   CompanyPhone = "05916894972",
						   CompanyPhoneType = "H",
						   CompanyTaxId = "1436",
						   DmvFilingState = "NY",
						   InformationValid = "",
						   MailingAddressStreet = "027 Fredy Cliffs Suite 878",
						   MailingAddressCity = "Floridatown",
						   MailingAddressState = "LA",
						   MailingAddressZip = "58665",
						   PhysicalAddressStreet = "0661 Verna Parks Apt. 315",
						   PhysicalAddressCity = "Kristinland",
						   PhysicalAddressState = "IL",
						   PhysicalAddressZip = "73742",
						   LegalCompanyName = "Brekke, Zemlak and Renner",
						   OwnerDlNumber= "1108",
						   OwnerDoB = new DateOnly(1978, 09, 11),
						   OwnerFirstName = "Murl",
						   OwnerLastName = "Hoppe",
						   OwnerExcluded = "1",
						   YearsOfPriorInsurance = 8,
						   IccCargoBmc34Number = "1587",
						   Mcs90Number = "1323",
						   Mc91Number = "1880",
						   TexasFormT = "1989",
						   UsDotNumber = "1897",
						   TypeOfOperation = "For Hire"
					   }
				   },
				   Agencies         = Enumerable.Range(1, 20).Select(x => GetRandomAgency(x, "some agency name")).ToArray(),
				   AgencyAgents		= Enumerable.Range(1, 20).Select(x => GetRandomAgent(x, x)).ToArray(),
				   Applications     = new[] { new Application { ApplicationId   = 1, AgencyClientId    = ClientId } },
				   CoverageRequests = Enumerable.Range(1, CoverageRequestCount).Select(x => new CoverageRequest { AgencyClientId = ClientId, CoverageId = x }).ToArray(),
				   Quotes = Enumerable.Range(1, QuotesCount).Select(x => new Quote { QuoteId = x, ApplicationId   = 1, InsurerId       = x, QuoteBound      = "Y" }).ToArray(),
				   Insurers = Enumerable.Range(1, InsurersCount).Select(x => new Insurer { InsurerId = x, InsurerCoName  = $"Test Insurer #{x}", InsurerEmail = $"x@insurer.com", InsurerPhoneNumber = GetRandomPhoneNumber(x), NaicNumber = s_random.Next(1234, 2000) }).ToArray(),
				   Drivers  = Enumerable.Range(1, DriverCount).Select(x => new Driver
				   {
					   DriverId = x,
					   AgencyClientId = ClientId,
					   DriverFirstName = $"Driver#{x}Firstname",
					   DriverLastName = $"Driver#{x}Lastname",
					   DriverDob = GetRandomDob(25, 90).ToDateOnly(),
					   DriverHireDate = GetRandomDob(1, 10).ToDateOnly(),
					   DriverExperienceStartDate = DateTime.Today.AddYears(- s_random.Next(1, 10)).ToDateOnly(),
					   DriverLicenseNumber = $"DL{x.ToString().PadLeft(8, '0')}",
					   DriverLicenseClass = "A",
					   DriverType = "A",
					   DriverLicenseState = "AL"
				   }).ToArray(),

				   Garages  = Enumerable.Range(1, GaragesCount).Select(x => new Garage { GarageId    = x, AgencyClientId = ClientId }).ToArray(),
				   Vehicles = Enumerable.Range(1, VehiclesCount).Select(x => new Vehicle { VehicleId = x, AgencyClientId = ClientId }).ToArray(),
				   Policies	= Enumerable.Range(1, 20).Select(x => new Policy 
				   { 
					   PolicyNumber = $"{x}", 
					   AgencyClientId=x, 
					   AgencyId=x, 
					   InsurerId=x, 
					   PolicyStartDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-4 * 30)), 
					   PolicyEndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(8 * 30))
				   }).ToArray(),
			   }.ActLike<IInsuranceDbContext>();
	}

	private static AgencyAgent GetRandomAgent(int agentId, int agencyId) => new AgencyAgent
	{
		AgencyAgentId = agentId,
		AgencyAgentType = "A",
		AgencyAgentFirstName = $"AgentName{agentId}",
		AgencyAgentLastName = $"AgentLastname{agentId}",
		AgencyAgentEmail = $"{agentId}@agency.com",
		AgencyAgentPhone = $"{GetRandomAreaCode()}-555-{agentId.ToString().PadLeft(3, '0')}",
		AgencyAgentNpn = agentId,
		AgencyId = agencyId,
		UserId = $"agent-user-{agentId}",
		AgencyAgentStatus = s_random.Next() % 2 == 0 ? "1" : null
	};

	private static Agency GetRandomAgency(int agencyId, string name) => new Agency
	{
		AgencyId = agencyId,
		AgencyName = $"{name} {agencyId}",
		AgencyMailingAddress1 = $"Test Address #{agencyId}",
		AgencyMailingAddress2 = $"Suite #{agencyId}",
		AgencyMailingCity = $"Test City #{agencyId}",
		AgencyMailingState = GetRandomState(),
		AgencyMailingZipcode = GetRandomZip(),
		PrimaryContactPhone = GetRandomPhoneNumber(agencyId),
		PrimaryContactEmail = $"{agencyId}@agency.com",
		AgencyNpn = agencyId,
		AgencyStatus = s_random.Next() % 2 == 0 ? "1" : null
	};

	private static string GetRandomPhoneNumber(int id) => $"{GetRandomAreaCode()}-555-{id.ToString().PadLeft(4, '0')}";

	private static string GetRandomZip()
	{
		var n1 = (int) Math.Pow(10, 6);
		var n2 = (int)Math.Pow(10, 7);
		var n = s_random.Next(n1, n2).ToString();
		return n[0..5];
	}

	private static string GetRandomAreaCode()
	{
		var areaCodes = "818,805,213,919,813,812,817,815,814,810,811,818,805,213,919,813,812,817,815,814,810,811,818,805,213,747,310"
			.Split(',')
			.Distinct()
			.ToArray();

		return areaCodes[s_random.Next(areaCodes.Length)];
	}

	private static string GetRandomState()
	{
		var states = "AL,AK,AZ,AR,CA,CO,CT,DE,FL,GA,HI,ID,IL,IN,IA,KS,KY,LA,ME,MD,MA,MI,MN,MS,MO,MT,NE,NV,NH,NJ,NM,NY,NC,ND,OH,OK,OR,PA,RI,SC,SD,TN,TX,UT,VT,VA,WA,WV,WI,WY".Split(',');
		return states[s_random.Next(states.Length)];
	}

	private static readonly Random s_random = new((int)DateTime.Now.Ticks);

	private static DateTime GetRandomDob(int minAge=25, int maxAge=90)
	{
		var ageInDays = s_random.Next((int) 365.25 * minAge, (int) 365.25 * maxAge);
		return DateTime.Today.AddDays(-ageInDays);
	}
}
