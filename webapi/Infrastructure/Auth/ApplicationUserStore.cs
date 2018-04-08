using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using webapi.Infrastructure.Auth;
using webapi.Infrastructure.Service;
using Enum = webapi.Common.Enum;

namespace webapi.Infrastructure.Auth
{
	public class ApplicationUserStore : IUserStore<User, int>, IUserPasswordStore<User, int> //IUserSecurityStampStore
	{
		readonly IDbContext _dbContext;

		public ApplicationUserStore(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void Dispose()
		{
			//throw new NotImplementedException();
		}

		public Task CreateAsync(User user)
		{
			return Task.Factory.StartNew(() => _dbContext.Create(user));
		}
		public Task UpdateAsync(User user)
		{
			return Task.Factory.StartNew(() => _dbContext.Update(user));
		}
		public Task DeleteAsync(User user)
		{
			user.Status = Enum.Status.Deleted;
			return Task.Factory.StartNew(() => _dbContext.Delete(user));
		}

		public Task<User> FindByIdAsync(int userId)
		{
			var user = _dbContext.GetByIdActive(userId);
			var appUser = Task<User>.Factory.StartNew(() => (User)user);
			return appUser;
		}
		public Task<User> FindByNameAsync(string userName)
		{
			var user = _dbContext.GetByUserNameActive(userName);
			var appUser = Task<User>.Factory.StartNew(() => (User)user);
			return appUser;
		}
		public Task<User> FindByNameAsync(string userName, string password)
		{
			var user = _dbContext.GetByUserNameAndPassActive(userName, password);
			var appUser = Task<User>.Factory.StartNew(() => (User)user);
			return appUser;
		}

		public Task SetPasswordHashAsync(User user, string passwordHash)
		{
			user.Password = passwordHash;
			return Task.FromResult(0);
		}
		public Task<string> GetPasswordHashAsync(User user)
		{
			var pass = Task.FromResult<string>(user.Password);
			return pass;
		}
		public Task<bool> HasPasswordAsync(User user)
		{
			return Task.FromResult<bool>(!String.IsNullOrEmpty(user.Password));
		}
	}
}
