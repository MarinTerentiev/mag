using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Models.Entities;

namespace dal.Services.Abstract
{
	public interface IOrderService
	{
		int Save(Order order);
		IEnumerable<Order> GetAll();
		IEnumerable<Order> GetByDealer(int dealerId);
		Order GetById(int id);
		Order GebByIdAndDealerId(int id, int dealerId);
	}
}
