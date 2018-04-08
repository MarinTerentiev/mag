using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Models.Entities;

namespace dal.Repositories.Abstract
{
	public interface IProductCardRepository : IEntitiesRepository<ProductCard>
	{
		IEnumerable<ProductCard> GetByOrderId(int orderId);
		IEnumerable<ProductCard> GetByDealerId(int orderId, int dealerId);
	}
}
