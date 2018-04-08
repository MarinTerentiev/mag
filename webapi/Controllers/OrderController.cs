using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dal.Models.Entities;
using dal.Services.Abstract;

namespace webapi.Controllers
{
	public class OrderController : BaseController
	{
		readonly IOrderService _orderService;
		readonly IDealerServices _dealerServices;
		public OrderController(IOrderService orderService, IDealerServices dealerServices)
		{
			_orderService = orderService;
			_dealerServices = dealerServices;
		}

		[HttpPost]
		[Authorize(Roles = "Customer")]
		[Route("api/order/post")]
		public IHttpActionResult Post(Order order)
		{
			order.Amount = order.ProductCards.Sum(x => x.Count*x.Price);
			order.UserId = UserId;
			order.Id = _orderService.Save(order);

			return Ok(new
			{
				data = "OK"
			});
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		[Route("api/order/get")]
		public IHttpActionResult Get()
		{
			var orders = _orderService.GetAll();

			return Ok(new
			{
				data = new
				{
					orders
				}
			});
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		[Route("api/order/get/{id}")]
		public IHttpActionResult Get(int id)
		{
			var order = _orderService.GetById(id);

			return Ok(new
			{
				data = new
				{
					order
				}
			});
		}

		[HttpGet]
		[Authorize(Roles = "Dealer")]
		[Route("api/order/getForDealer")]
		public IHttpActionResult GetForDealer()
		{
			var dealer = _dealerServices.GetByUserId(UserId);
			var orders = _orderService.GetByDealer(dealer.Id);

			return Ok(new
			{
				data = new
				{
					orders
				}
			});
		}

		[HttpGet]
		[Authorize(Roles = "Dealer")]
		[Route("api/order/getForDealer/{id}")]
		public IHttpActionResult GetForDealer(int id)
		{
			var dealer = _dealerServices.GetByUserId(UserId);
			var order = _orderService.GebByIdAndDealerId(id, dealer.Id);

			return Ok(new
			{
				data = new
				{
					order
				}
			});
		}
	}
}
