using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Cartisan.EntityFramework {
    public abstract class ContextBase: DbContext {
        static ContextBase() {
//            Database.SetInitializer<T>(null);
        }

        protected ContextBase() { }
        protected ContextBase(DbCompiledModel model)
            : base(model) { }
        protected ContextBase(string connectionString)
            : base(connectionString) { }
        protected ContextBase(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection) { }
        protected ContextBase(string connectionString, DbCompiledModel model)
            : base(connectionString, model) { }
        protected ContextBase(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection) { }
        protected ContextBase(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
//            IEnumerable<Type> typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
//                .Where(type => !string.IsNullOrEmpty(type.Namespace))
//                .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
//                    type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
//
//            foreach(var type in typesToRegister) {
//                dynamic configurationInstance = Activator.CreateInstance(type);
//                modelBuilder.Configurations.Add(configurationInstance);
//            }

            base.OnModelCreating(modelBuilder);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity: class {
            return base.Set<TEntity>();
        }

        public void SetAsAdded<TEntity>(TEntity entity) where TEntity: class {
            DbEntityEntry dbEntityEntry = this.GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = EntityState.Added;
        }

        public void SetAsModified<TEntity>(TEntity entity) where TEntity: class {
            DbEntityEntry dbEntityEntry = this.GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity: class {
            DbEntityEntry dbEntityEntry = this.GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        private DbEntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity)
            where TEntity: class {
            DbEntityEntry<TEntity> dbEntityEntry = Entry<TEntity>(entity);
            if (dbEntityEntry.State == EntityState.Detached) {
                this.Set<TEntity>().Attach(entity);
            }

            return dbEntityEntry;
        }
    }
}