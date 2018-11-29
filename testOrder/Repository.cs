using System;
using System.Collections.Generic;
using System.Linq;

namespace testOrder
{
	public class Repository<TEntity> where TEntity : class
	{
		public Repository(IQueryable<TEntity> Db)
		{
			this.Db = Db;
		}
		IQueryable<TEntity> Db;


		public IQueryable<TEntity> GetModel()
		{
			return Db.AsQueryable();
		}

		public IQueryable<TEntity> GetModel(Action<IOrderable<TEntity>> orderBy)
		{
			var linq = new Orderable<TEntity>(GetModel());
			orderBy(linq);
			return linq.Queryable;
		}


	}


}
