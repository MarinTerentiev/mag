using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Models.Entities;

namespace dal.Services.Abstract
{
	public interface IUserService
	{
		int Create(User user);

		void Update(User user);

		void UpdatePassword(User entity);

		void Delete(User user);

		User GetByIdActive(int id);

		User GetByUserNameActive(string userName);

		User GetByUserNameAndPassActive(string userName, string pass);
		User GetByEmailActive(string email);

		bool ExistUserName(string userName);

		bool ExistEmail(string email);

		IEnumerable<User> GetAll();

		void Delete(int id);

		UserSettings GetUserSettings(int id);

		IEnumerable<UserSettings> GetAllUserSettings();

		int CreateUserSettings(UserSettings entity);

		void UpdateUserSettings(UserSettings entity);
	}
}
