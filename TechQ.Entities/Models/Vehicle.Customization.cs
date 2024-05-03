using AutoMapper;

namespace TechQ.Entities.Models;

public partial class Vehicle
{
	public static implicit operator Vehicle(VehicleEndorsement endorsement) => EndorsementToVehicleMapper.Map<Vehicle>(endorsement);

	public static explicit operator VehicleEndorsement(Vehicle vehicle) => VehicleToEndorsementMapper.Map<VehicleEndorsement>(vehicle);

	public Vehicle UpdateFrom(VehicleEndorsement endorsement)
	{
		EndorsementToVehicleMapper.Map(endorsement, this);
		return this;
	}

	private static readonly Lazy<IMapper> s_vehicleToEndorsementMapper = new(() => new MapperConfiguration(config => config.CreateMap<Vehicle, VehicleEndorsement>()).CreateMapper());
	private static readonly Lazy<IMapper> s_endorsementToVehicleMapper = new(() => new MapperConfiguration(config => config.CreateMap<VehicleEndorsement, Vehicle>()).CreateMapper());

	private static IMapper VehicleToEndorsementMapper => s_vehicleToEndorsementMapper.Value;
	private static IMapper EndorsementToVehicleMapper => s_endorsementToVehicleMapper.Value;
}