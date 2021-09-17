using AutoMapper;
using Bp.Api.Service.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BP.Api.Extensions
{
    public static class ConfigureMappingProfileExtension
    {
        public static IServiceCollection ConfigureMapping(this IServiceCollection services)
        {
            var types = typeof(AutoMapperCustomProfile).Assembly.GetTypes();

            // IsSubclassOf will make sure that we using class deriving Profile
            // Activator.CreateInstance will instantiate the profile
            // OfType<Profile>() will cast the types
            var automapperProfiles = types
                                        .Where(x => x.IsSubclassOf(typeof(Profile)))
                                        .Select(Activator.CreateInstance)
                                        .OfType<AutoMapperCustomProfile>()
                                        .ToList();

            var config = new MapperConfiguration(i =>
            {
                
                    i.AddProfiles(automapperProfiles);
                
            });

            IMapper mapper = config.CreateMapper();

            //var mappingConfig = new MapperConfiguration(i => i.AddProfile(new AutoMapperMappingProfile()));
            //IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }

    }

}
