using AutoMapper;
using Devy.Security.Api.Application.Model.Employees;
using Devy.Security.Api.Application.Model.Locations;
using Devy.Security.Api.Application.Query;

namespace Devy.Security.Api;

public class AutoMapperConfig
{
    /// <summary>
    /// Initializes this instance.
    /// </summary>
    /// <returns></returns>
    public static MapperConfiguration Initialize()
    {
        var mapperConfig = new MapperConfiguration(config =>
        {
            config.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            config.RecognizePrefixes("Date");
            config.RecognizeDestinationPostfixes("Date");

            ConfigureEmployee(config);
            ConfigureLocations(config);
        });
        return mapperConfig;
    }

    #region Employee

    private static void ConfigureEmployee(IProfileExpression configuration)
    {
        configuration.CreateMap<Domain.Entities.EmployeeContact, EmployeeContact>();
        configuration.CreateMap<CreateEmployeeContactsCommand, Domain.Entities.EmployeeContact>()
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => string.Join("|", src.Numbers)));
    }
    private static void ConfigureLocations(IProfileExpression configuration)
    {
        configuration.CreateMap<Domain.Entities.Location, Location>();
    }
    #endregion Employee
}
