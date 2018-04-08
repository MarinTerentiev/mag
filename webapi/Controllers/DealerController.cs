using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dal.Models.Entities;
using dal.Services.Abstract;
using webapi.Models.Data;

namespace webapi.Controllers
{
	public class DealerController : BaseController
	{
		readonly IDealerServices _dealerServices;
		readonly IUserService _userService;

		public DealerController(IDealerServices dealerServices, IUserService userService)
		{
			_dealerServices = dealerServices;
			_userService = userService;
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		[Route("api/dealer/get")]
		public IHttpActionResult Get()
		{
			var dealers = _dealerServices.GetAll();

			return Ok(new
			{
				data = new
				{
					dealers
				}
			});
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		[Route("api/dealer/get/{id}")]
		public IHttpActionResult Get(int id)
		{
			var dealer = _dealerServices.Get(id);

			return Ok(new
			{
				data = new
				{
					dealer
				}
			});
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		[Route("api/dealer/post")]
		public IHttpActionResult Post(Dealer dealer)
		{
			if (dealer.Id == -1)
			{
				dealer.Id = _dealerServices.Insert(dealer);
			}
			else
			{
				_dealerServices.Update(dealer);
			}

			return Ok(new
			{
				data = new
				{
					dealer
				}
			});
		}

		[HttpDelete]
		[Authorize(Roles = "Admin")]
		[Route("api/dealer/delete/{id}")]
		public IHttpActionResult Delete(int id)
		{
			_dealerServices.Delete(id);

			return Ok(new
			{
				data = "OK"
			});
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("api/dealer/signUpDealer")]
		public IHttpActionResult SignUpDealer(SignUpDealer signUp)
		{
			if (_userService.ExistUserName(signUp.UserName))
			{
				return Content(HttpStatusCode.BadRequest, "Login is exist.");
			}

			if (_userService.ExistEmail(signUp.Email))
			{
				return Content(HttpStatusCode.BadRequest, "Email is exist.");
			}

			var existName = _dealerServices.ExistName(signUp.Name);
			if (existName)
			{
				return Content(HttpStatusCode.BadRequest, "Dealer name is exist.");
			}

			var user = new Infrastructure.Auth.User();
			user.UserName = signUp.UserName;
			user.Email = signUp.Email;
			user.Name = signUp.Name;
			user.ListRoles = new List<Common.Enum.Role> { Common.Enum.Role.Dealer };
			user.Status = Common.Enum.Status.Active;
			user.Password = string.Empty;
			user.Id = _userService.Create(user);

			var dealer = new Dealer();
			dealer.UserId = user.Id;
			dealer.Name = signUp.DealerName;
			dealer.Enable = true;
			dealer.Id = _dealerServices.Insert(dealer);

			return Ok(new { data = "OK" });
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("api/dealer/existData")]
		public IHttpActionResult ExistData(DealerExists dealerExists)
		{
			var exist = false;
			var message = string.Empty;

			var existUserName = _userService.ExistUserName(dealerExists.UserName);
			if (existUserName)
			{
				exist = true;
				message += "Login is exist.<br>";
			}

			var existEmail = _userService.ExistEmail(dealerExists.Email);
			if (existEmail)
			{
				exist = true;
				message += "Email is exist.<br>";
			}

			var existName = _dealerServices.ExistName(dealerExists.Name);
			if (existName)
			{
				exist = true;
				message += "Dealer name is exist.<br>";
			}

			return Ok(new
			{
				data = new
				{
					exist,
					message
				}
			});
		}

		[HttpGet]
		[Authorize(Roles = "Customer")]
		[Route("api/dealer/getForCatalog")]
		public IHttpActionResult GetForCalalog()
		{
			var dealers =  _dealerServices.GetAll().Where(x => x.Enable).Select(x => (Models.Entities.Dealer) x);

			return Ok(new
			{
				data = new
				{
					dealers
				}
			});
		}
	}
}
