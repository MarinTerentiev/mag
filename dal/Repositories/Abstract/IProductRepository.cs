using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Models.Entities;

namespace dal.Repositories.Abstract
{
	public interface IProductRepository : IEntitiesRepository<Product>
	{
		IEnumerable<Product> GetByUserId(int userId);
		IEnumerable<Product> GetByCompanyId(int companyId);
		bool IsOwner(int id, int userId);
	}
}
