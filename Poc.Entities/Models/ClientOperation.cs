using System;
using System.Collections.Generic;

namespace Poc.Entities.Models;

public partial class ClientOperation
{
    public long AgencyClientId { get; set; }

    public string OwnAnotherCompany { get; set; }

    public string OwnAnotherCompanyExplanation { get; set; }

    public string AbortedPolicies { get; set; }

    public string LeasedVehicleOthers { get; set; }

    public string UndisclosedVehicles { get; set; }

    public string ComplyDotInspection { get; set; }

    public string ReportVehiclesDrivers { get; set; }

    public string TeamDrivers { get; set; }

    public string HaulHazardMaterial { get; set; }

    public string ProvideWorkerComp { get; set; }

    public string HaulOversizeOverweight { get; set; }

    public string HaulExtraTrailers { get; set; }

    public string HaveSafetyProgram { get; set; }

    public string DriverMvrObtained { get; set; }

    public string OperateMultiState { get; set; }

    public string Under21Drivers { get; set; }

    public string DriversDotCompliant { get; set; }

    public string OperateSameVehicle { get; set; }

    public string Minimum5YearsDriving { get; set; }

    public string DisciplinaryPlanDocumented { get; set; }

    public string DriverIncentiveProgram { get; set; }

    public string CargoInsuranceRequired { get; set; }

    public string OvernightCommoditiesStorage { get; set; }

    public string RefrigeratedUnits { get; set; }

    public string RefrigerationCoverageDesired { get; set; }

    public string HowsmydrivingProgram { get; set; }

    public string NoCellWhileDriving { get; set; }

    public virtual AgencyClient AgencyClient { get; set; }
}
