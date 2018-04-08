using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace webapi.Infrastructure.Auth
{
	public class ApplicationUserManager : UserManager<User, int>
	{
		public ApplicationUserManager(IUserStore<User, int> store)
			: base(store)
		{ }

		public override Task<User> FindAsync(string userName, string password)
		{
			Task<User> taskInvoke = Task<User>.Factory.StartNew(() =>
			{
				var appUser = Store.FindByNameAsync(userName).Result;
				if (appUser == null)
				{
					return null;
				}

				var hashPass = PasswordHash.GetPass(password);
				if (appUser.Password == hashPass && appUser.Status == Common.Enum.Status.Active)
				{
					return appUser;
				}
				return null;
			});
			return taskInvoke;
		}

		public override Task<User> FindByNameAsync(string userName)
		{
			Task<User> taskInvoke = Task<User>.Factory.StartNew(() =>
			{
				var appUser = Store.FindByNameAsync(userName).Result;
				if (appUser == null)
				{
					return null;
				}
				return appUser;
			});
			return taskInvoke;
		}

		public override Task<IdentityResult> CreateAsync(User user)
		{
			Task.Factory.StartNew(() =>
			{
				Store.CreateAsync(user);
				return IdentityResult.Success;
			});
			List<string> errors = new List<string> { "Create Error" };
			return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
		}
	}
}