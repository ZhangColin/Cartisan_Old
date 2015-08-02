//using Cartisan.Domain;
//using Cartisan.Repository;
//
//namespace Cartisan.EntityFramework {
//    public class EntityUnitOfWork: IUnitOfWork {
//        private readonly IEntitiesContext _context;
//
//        public EntityUnitOfWork(IEntitiesContext context) {
//            this._context = context;
//        }
//
//        public void RegisterAmended(IAggregateRoot entity) {
//            _context.SetAsModified(entity);
//        }
//
//        public void RegisterNew(IAggregateRoot entity) {
//            _context.SetAsAdded(entity);
//        }
//
//        public void RegisterRemoved(IAggregateRoot entity) {
//            _context.SetAsDeleted(entity);
//        }
//
//        public void Commit() {
//            _context.SaveChanges();
//        }
//    }
//}