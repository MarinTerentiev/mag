using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dal.Models.Entities;
using dal.Services.Abstract;
using webapi.Infrastructure.Auth;
using webapi.Models.Data;

namespace webapi.Controllers
{
	public class UserController : BaseController
	{
		readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("api/user/signUp")]
		public IHttpActionResult SignUp(SignUp signUp)
		{
			if (_userService.ExistUserName(signUp.UserName))
			{
				return Content(HttpStatusCode.BadRequest, "Login is exist.");
			}

			if (_userService.ExistEmail(signUp.Email))
			{
				return Content(HttpStatusCode.BadRequest, "Email is exist.");
			}

			if (signUp.Password != signUp.PasswordConfirm)
			{
				return Content(HttpStatusCode.BadRequest, "Password is not match");
			}

			var user = new Infrastructure.Auth.User();
			user.UserName = signUp.UserName;
			user.Email = signUp.Email;
			user.Name = signUp.Name;
			user.ListRoles = new List<Common.Enum.Role> { Common.Enum.Role.Customer };
			user.Status = Common.Enum.Status.Active;
			user.Password = PasswordHash.GetPass(signUp.Password);
			user.Id = _userService.Create(user);

			var settings = new UserSettings();
			settings.UserId = user.Id;
			settings.Status = Common.Enum.Status.Active.ToString();
			settings.Id = _userService.CreateUserSettings(settings);

			return Ok(new { data = "OK" });
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("api/user/existData")]
		public IHttpActionResult ExistData(UserExists dealerExists)
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


			return Ok(new
			{
				data = new
				{
					exist,
					message
				}
			});
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("api/user/resetPassword")]
		public IHttpActionResult ResetPassword(ResetPassword resetPassword)
		{
			if (resetPassword.Key != "123")
			{
				return Content(HttpStatusCode.Forbidden, "Code is wrong");
			}

			if (resetPassword.Password != resetPassword.PasswordConfirm)
			{
				return Content(HttpStatusCode.Forbidden, "Password is not matched");
			}

			var user = _userService.GetByUserNameActive(resetPassword.UserName);
			user.Password = PasswordHash.GetPass(resetPassword.Password);
			_userService.UpdatePassword(user);

			return Ok(new
			{
				data = "Ok"
			});
		}
	}
}
