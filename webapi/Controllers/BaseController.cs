using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using dal.Repositories.Abstract;
using dal.Services.Abstract;
using Microsoft.AspNet.Identity;
using webapi.Infrastructure.Service;

namespace webapi.Controllers
{
	public class BaseController : ApiController
	{
		protected readonly SqlConnection Db;
		readonly ITestServices _testServices;

		private int _userId = -1;
		public int UserId
		{
			get
			{
				if (_userId == -1)
				{
					var userIdStr = HttpContext.Current.User.Identity.GetUserId();
					_userId = string.IsNullOrWhiteSpace(userIdStr) ? -1 : int.Parse(userIdStr);
				}
				return _userId;
			}
		}

		public BaseController()
		{
			Db = new SqlConnection(ConfigurationManager.ConnectionStrings[@"Db"].ConnectionString);
		}

		public BaseController(ITestServices testServices)
		{
			Db = new SqlConnection(ConfigurationManager.ConnectionStrings[@"Db"].ConnectionString);
			this._testServices = testServices;
		}

		[HttpGet]
		public IHttpActionResult TestService()
		{
			var cc = BaseService.GetTest(Db);
			return Ok("db service: " + cc);
		}

		[HttpGet]
		public IHttpActionResult TestInject()
		{
			var cc = _testServices.Get(1);
			return Ok("db inject: " + cc.Test);
		}
	}
}
