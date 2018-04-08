using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dal.Services.Abstract;

namespace webapi.Controllers
{
	public class CustomerController : BaseController
	{
		readonly IUserService _userService;

		public CustomerController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		[Route("api/customer/get")]
		public IHttpActionResult Get()
		{
			var users = _userService.GetAll().Where(x => x.Roles.Contains(Common.Enum.Role.Customer.ToString()));

			return Ok(new
			{
				data = new
				{
					users
				}
			});
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		[Route("api/customer/get/{id}")]
		public IHttpActionResult Get(int id)
		{
			var user = _userService.GetByIdActive(id);

			return Ok(new
			{
				data = new
				{
					user
				}
			});
		}

		[HttpDelete]
		[Authorize(Roles = "Admin")]
		[Route("api/customer/delete/{id}")]
		public IHttpActionResult Delete(int id)
		{
			_userService.Delete(id);

			return Ok(new
			{
				data = "OK"
			});
		}
	}
}
