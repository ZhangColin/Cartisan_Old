using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using Cartisan.EntityFramework;
using MySql.Data.Entity;

namespace Cartisan.Identity.Repository {
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class IdentityContext: ContextBase {
        public IdentityContext(string connectionString) : base(connectionString) {
            Configuration.ProxyCreationEnabled = false;
#if (DEBUG)
            //            Database.SetInitializer(new IdentityInitializer());
#endif
        }

        //        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            //            modelBuilder.Configurations.Add(new UserAccountMap());
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister) {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}