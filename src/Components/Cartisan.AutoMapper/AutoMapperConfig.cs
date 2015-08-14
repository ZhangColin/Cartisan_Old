using System.Collections.Generic;
using AutoMapper;
using Cartisan.DependencyInjection;
using Cartisan.Extensions;

namespace Cartisan.AutoMapper {
    public static class AutoMapperConfig {
        public static void Initialize() {
            IEnumerable<Profile> profiles = ServiceLocator.GetServices<Profile>();
            Mapper.Initialize(config => profiles.ForEach(profile => config.AddProfile(profile)));
            Mapper.AssertConfigurationIsValid();
        }
    }
}