using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Enum = webapi.Common.Enum;

namespace webapi.Infrastructure.Auth
{
	public class User : IUser<int>
	{
		public User()
		{
			Id = -1;
			UserName = string.Empty;
		}

		public int Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }

		public List<Enum.Role> ListRoles { get; set; }

		public Enum.Status Status { get; set; }


		public bool IsInRole(Enum.Role role)
		{
			return ListRoles.Contains(role);
		}


		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager, string authenticationType)
		{
			return await GenerateUserIdentityAsync(manager, authenticationType, new User { ListRoles = new List<Enum.Role>() });
		}
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager, string authenticationType, User appUser)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

			// Add custom user claims here
			foreach (var rol in appUser.ListRoles)
			{
				userIdentity.AddClaim(new Claim(ClaimTypes.Role, System.Enum.GetName(typeof(Enum.Role), rol)));
			}

			return userIdentity;
		}



		public static explicit operator User(dal.Models.Entities.User user)
		{
			return new User
			{
				Id = user.Id,
				UserName = user.UserName,
				ListRoles = GetListRoles(user.Roles),
				Email = user.Email,
				Name = user.Name,
				Password = user.Password,
				Status = (Enum.Status)System.Enum.Parse(typeof(Enum.Status), user.Status)
			};
		}

		public static implicit operator dal.Models.Entities.User(User user)
		{
			return new dal.Models.Entities.User
			{
				UserName = user.UserName,
				Id = user.Id,
				Roles = SetStringRoles(user.ListRoles),
				Password = user.Password,
				Email = user.Email,
				Name = user.Name,
				Status = user.Status.ToString()
			};
		}


		private static List<Enum.Role> GetListRoles(string roles)
		{
			var arrRoles = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			var listRoles = arrRoles.Select(role => (Enum.Role)System.Enum.Parse(typeof(Enum.Role), role)).ToList();
			return listRoles;
		}

		private static string SetStringRoles(List<Enum.Role> listRoles)
		{
			var roles = string.Join(",", listRoles.ConvertAll<string>(x => x.ToString()).ToArray());
			return roles;
		}
	}
}