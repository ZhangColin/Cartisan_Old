using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Cartisan.Core.Domain;

namespace Cartisan.EntityFramework.TestHelpers {
    public class FakeDbSet<TEntity>: IDbSet<TEntity> where TEntity: class, IEntity<int> {
        private readonly ObservableCollection<TEntity> _collection;
        private readonly IQueryable _query;

        public FakeDbSet() {
            this._collection = new ObservableCollection<TEntity>();
            this._query = this._collection.AsQueryable();
        }

        public TEntity Add(TEntity entity) {
            this._collection.Add(entity);
            return entity;
        }

        public TEntity Attach(TEntity entity) {
            this._collection.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity: class, TEntity {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public TEntity Create() {
            return Activator.CreateInstance<TEntity>();
        }

        public TEntity Find(params object[] keyValues) {
            return _collection.FirstOrDefault(entity => entity.Id == int.Parse(keyValues[0].ToString()));
        }

        public ObservableCollection<TEntity> Local {
            get {
                return this._collection;
            }
        }

        public TEntity Remove(TEntity entity) {
            this._collection.Remove(entity);
            return entity;
        }

        public IEnumerator<TEntity> GetEnumerator() {
            return this._collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public Expression Expression {
            get {
                return this._query.Expression;
            }
        }

        public Type ElementType {
            get {
                return this._query.ElementType;
            }
        }

        public IQueryProvider Provider {
            get {
                return this._query.Provider;
            }
        }
    }
}