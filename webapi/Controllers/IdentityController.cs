using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using dal.Services.Abstract;
using webapi.Infrastructure.Auth;
using webapi.Models.Data;
using Enum = webapi.Common.Enum;

namespace webapi.Controllers
{
	public class IdentityController : BaseController
	{
		private UserManager<User, int> _userManager;
		private IAuthenticationManager Authentication => Request.GetOwinContext().Authentication;
		readonly IDealerServices _dealerServices;

		public IdentityController() { }
		public IdentityController(UserManager<User, int> userManager, IDealerServices dealerServices)
		{
			_userManager = userManager;
			_dealerServices = dealerServices;
		}


		[HttpPost]
		[AllowAnonymous]
		[Route("api/Identity/Login")]
		public async Task<IHttpActionResult> Login(Auth auth)
		{
			try
			{
				var appUser = await _userManager.FindAsync(auth.UserName, auth.Password);
				if (appUser != null && appUser.Status == Enum.Status.Active)
				{
					if (appUser.IsInRole(Enum.Role.Dealer))
					{
						var dealer = _dealerServices.GetByUserId(appUser.Id);
						if (!dealer.Enable)
						{
							return Content(HttpStatusCode.Forbidden, "This dealer is disabled");
						}
					}

					var isRememberMe = true;
					Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
					Authentication.SignIn(new AuthenticationProperties { IsPersistent = isRememberMe },
						await appUser.GenerateUserIdentityAsync(_userManager, DefaultAuthenticationTypes.ApplicationCookie, appUser));


					return Ok(new
					{
						data = new
						{
							user = appUser,
							token = Guid.NewGuid().ToString()
						}
					});
				}
				return Ok(new
				{
					data = new { error = "error" }
				});
			}
			catch (Exception ex)
			{
				return BadRequest(ex.ToString());
			}
		}

		[HttpPost]
		[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		public IHttpActionResult Logout()
		{
			var authenticationTypes = new string[] {
				DefaultAuthenticationTypes.ApplicationCookie,
				DefaultAuthenticationTypes.ExternalCookie,
				CookieAuthenticationDefaults.AuthenticationType
			};
			Authentication.SignOut(authenticationTypes);

			//Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			//Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
			return Ok(new {data = "OK"});
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && _userManager != null)
			{
				_userManager.Dispose();
				_userManager = null;
			}

			base.Dispose(disposing);
		}
	}
}
