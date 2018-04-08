using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Repositories
{
	public interface IEntitiesRepository<TEntity> where TEntity : class
	{
		TEntity Get(int id);
		IEnumerable<TEntity> GetAll();
		int Insert(TEntity entity);
		void Delete(int id);
		void Update(TEntity entity);
	}
}
