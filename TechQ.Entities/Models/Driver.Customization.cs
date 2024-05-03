using AutoMapper;

namespace TechQ.Entities.Models;

public partial class Driver
{
	public static implicit operator Driver(DriverEndorsement endorsement) => EndorsementToDriverMapper.Map<Driver>(endorsement);

	public static implicit operator DriverEndorsement(Driver driver) => DriverToEndorsementMapper.Map<DriverEndorsement>(driver);

	public Driver UpdateFrom(DriverEndorsement endorsement)
	{
		EndorsementToDriverMapper.Map(endorsement, this);
		return this;
	}
	
	private static readonly Lazy<IMapper> s_driverToEndorsementMapper = new(() => new MapperConfiguration(config => config.CreateMap<Driver, DriverEndorsement>()).CreateMapper());
	private static readonly Lazy<IMapper> s_endorsementToDriverMapper = new(() => new MapperConfiguration(config => config.CreateMap<DriverEndorsement, Driver>()).CreateMapper());

	private static IMapper DriverToEndorsementMapper => s_driverToEndorsementMapper.Value;
	private static IMapper EndorsementToDriverMapper => s_endorsementToDriverMapper.Value;
}
