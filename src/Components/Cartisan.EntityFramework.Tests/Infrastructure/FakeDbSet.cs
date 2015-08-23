using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Cartisan.EntityFramework.Tests.Infrastructure {
    public class FakeDbSet<TEntity>: IDbSet<TEntity> where TEntity: class {
        private ObservableCollection<TEntity> _collection;

        private IQueryable _query;

        public FakeDbSet() {
            _collection = new ObservableCollection<TEntity>();
            _query = _collection.AsQueryable();
        }

        public IEnumerator<TEntity> GetEnumerator() {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public Expression Expression {
            get {
                return _query.Expression;
            }
        }

        public Type ElementType {
            get {
                return _query.ElementType;
            }
        }

        public IQueryProvider Provider {
            get {
                return _query.Provider;
            }
        }

        public TEntity Find(params object[] keyValues) {
            throw new NotImplementedException();
        }

        public TEntity Add(TEntity entity) {
            _collection.Add(entity);
            return entity;
        }

        public TEntity Remove(TEntity entity) {
            _collection.Remove(entity);
            return entity;
        }

        public TEntity Attach(TEntity entity) {
            _collection.Add(entity);
            return entity;
        }

        public TEntity Create() {
            return Activator.CreateInstance<TEntity>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity: class, TEntity {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<TEntity> Local {
            get {
                return _collection;
            }
        }
    }
}