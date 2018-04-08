using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Models.Entities;

namespace dal.Repositories.Abstract
{
	public interface ICompanyRepository : IEntitiesRepository<Company>
	{
		IEnumerable<Company> GetByDealerId(int dealerId);
		bool IsOwner(int id, int userId);
	}
}
