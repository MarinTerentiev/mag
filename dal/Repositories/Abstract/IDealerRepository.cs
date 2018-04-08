using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Models.Entities;

namespace dal.Repositories.Abstract
{
	public interface IDealerRepository : IEntitiesRepository<Dealer>
	{
		Dealer GetByUserId(int userId);

		bool ExistName(string name);
	}
}
