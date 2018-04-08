using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Infrastructure;
using dal.Models.Entities;
using dal.Services.Abstract;

namespace dal.Services.Concrete
{
	public class UserService : IUserService
	{
		private readonly IAutofacOfWork _autofacOfWork;

		public UserService(IAutofacOfWork autofacOfWork)
		{
			this._autofacOfWork = autofacOfWork;
		}

		public int Create(User user)
		{
			return _autofacOfWork.UserRepository.Insert(user);
		}

		public void Update(User user)
		{
			_autofacOfWork.UserRepository.Update(user);
		}

		public void UpdatePassword(User entity)
		{
			_autofacOfWork.UserRepository.UpdatePassword(entity);
		}

		public void Delete(User user)
		{
			_autofacOfWork.UserRepository.Delete(user.Id);
			_autofacOfWork.UserSettingsRepository.Delete(user.Id);
		}

		public User GetByIdActive(int id)
		{
			return _autofacOfWork.UserRepository.GetByIdActive(id);
		}

		public User GetByUserNameActive(string userName)
		{
			return _autofacOfWork.UserRepository.GetByUserNameActive(userName);
		}

		public User GetByUserNameAndPassActive(string userName, string pass)
		{
			return _autofacOfWork.UserRepository.GetByUserNameAndPassActive(userName, pass);
		}

		public User GetByEmailActive(string email)
		{
			return _autofacOfWork.UserRepository.GetByEmailActive(email);
		}

		public bool ExistUserName(string userName)
		{
			return _autofacOfWork.UserRepository.ExistUserName(userName);
		}

		public bool ExistEmail(string email)
		{
			return _autofacOfWork.UserRepository.ExistEmail(email);
		}

		public IEnumerable<User> GetAll()
		{
			return _autofacOfWork.UserRepository.GetAll();
		}

		public void Delete(int id)
		{
			_autofacOfWork.UserSettingsRepository.Delete(id);
			_autofacOfWork.UserRepository.Delete(id);
		}


		public UserSettings GetUserSettings(int id)
		{
			return _autofacOfWork.UserSettingsRepository.Get(id);
		}

		public IEnumerable<UserSettings> GetAllUserSettings()
		{
			return _autofacOfWork.UserSettingsRepository.GetAll();
		}

		public int CreateUserSettings(UserSettings entity)
		{
			return _autofacOfWork.UserSettingsRepository.Insert(entity);
		}

		public void UpdateUserSettings(UserSettings entity)
		{
			_autofacOfWork.UserSettingsRepository.Update(entity);
		}
	}
}
