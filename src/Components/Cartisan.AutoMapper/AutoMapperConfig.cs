using System.Collections.Generic;
using AutoMapper;
using Cartisan.Extensions;
using Cartisan.IoC;

namespace Cartisan.AutoMapper {
    public static class AutoMapperConfig {
        public static void Initialize() {
            IEnumerable<Profile> profiles = ServiceLocator.GetServices<Profile>();
            Mapper.Initialize(config => profiles.ForEach(profile => config.AddProfile(profile)));
            Mapper.AssertConfigurationIsValid();
        }
    }
}