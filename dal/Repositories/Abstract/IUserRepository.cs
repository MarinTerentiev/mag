using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Models.Entities;

namespace dal.Repositories.Abstract
{
	public interface IUserRepository : IEntitiesRepository<User>
	{
		User GetByIdActive(int id);
		User GetByUserNameActive(string userName);
		User GetByUserNameAndPassActive(string userName, string pass);
		User GetByEmailActive(string email);

		bool ExistUserName(string userName);
		bool ExistEmail(string email);

		void UpdatePassword(User entity);
	}
}
