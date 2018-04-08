using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Models.Entities;

namespace dal.Repositories.Abstract
{
	public interface IOrderRepository : IEntitiesRepository<Order>
	{
		IEnumerable<Order> GetByDealer(int dealerId);

		Order Get(int id, int dealerId);
	}
}
