using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Infrastructure;
using dal.Models.Entities;
using dal.Services.Abstract;

namespace dal.Services.Concrete
{
	public class OrderService : IOrderService
	{
		private readonly IAutofacOfWork _autofacOfWork;

		public OrderService(IAutofacOfWork autofacOfWork)
		{
			this._autofacOfWork = autofacOfWork;
		}

		public int Save(Order order)
		{
			order.Id = _autofacOfWork.OrderRepository.Insert(order);

			foreach (var productCard in order.ProductCards)
			{
				productCard.OrderId = order.Id;
				productCard.Id = _autofacOfWork.ProductCardRepository.Insert(productCard);
			}

			return order.Id;
		}

		public IEnumerable<Order> GetAll()
		{
			return _autofacOfWork.OrderRepository.GetAll();
		}

		public IEnumerable<Order> GetByDealer(int dealerId)
		{
			return _autofacOfWork.OrderRepository.GetByDealer(dealerId);
		}

		public Order GetById(int id)
		{
			var order = _autofacOfWork.OrderRepository.Get(id);
			if (order != null)
			{
				order.ProductCards = _autofacOfWork.ProductCardRepository.GetByOrderId(id);
			}
			return order;
		}

		public Order GebByIdAndDealerId(int id, int dealerId)
		{
			var order = _autofacOfWork.OrderRepository.Get(id, dealerId);
			if (order != null)
			{
				order.Amount = 0;
				order.ProductCards = _autofacOfWork.ProductCardRepository.GetByDealerId(id, dealerId);
			}
			return order;
		}
	}
}
