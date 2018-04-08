using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Models.Entities;

namespace dal.Repositories.Abstract
{
	public interface ICategoryRepository : IEntitiesRepository<Category>
	{
		IEnumerable<Category> GetByCompanyId(int companyId);

		bool IsOwner(int id, int userId);
	}
}
