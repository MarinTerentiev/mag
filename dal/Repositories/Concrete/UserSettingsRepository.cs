using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Infrastructure;
using dal.Models.Entities;
using dal.Repositories.Abstract;
using Dapper;

namespace dal.Repositories.Concrete
{
	public class UserSettingsRepository : IUserSettingsRepository
	{
		readonly IConnectionFactory _connectionFactory;

		public UserSettingsRepository(IConnectionFactory connectionFactory)
		{
			this._connectionFactory = connectionFactory;
		}

		public UserSettings Get(int id)
		{
			return _connectionFactory.GetConnection.Query<UserSettings>(@"
					SELECT * 
					  FROM [UserSettings] 
					 WHERE [Id] = @id",
				new { id }).FirstOrDefault();
		}

		public IEnumerable<UserSettings> GetAll()
		{
			return _connectionFactory.GetConnection.Query<UserSettings>(@"
					SELECT * 
					  FROM [UserSettings] 
					 WHERE [Id] = @id
					   AND [Status] != 'Deleted'");
		}

		public int Insert(UserSettings entity)
		{
			return _connectionFactory.GetConnection.Query<int>(@"
					INSERT INTO [UserSettings](
						[UserId], 
						[Phone], 
						[Address], 
						[PhotoPath])
					VALUES(
						@UserId,
						@Phone,
						@Address,
						@PhotoPath);
					SELECT SCOPE_IDENTITY();",
				entity).FirstOrDefault();
		}

		public void Delete(int userId)
		{
			_connectionFactory.GetConnection.Query(@"
					UPDATE [UserSettings]
					   SET [Status] = 'Deleted'
					 WHERE [UserId] = @userId",
				new { userId });
		}

		public void Update(UserSettings entity)
		{
			_connectionFactory.GetConnection.Query(@"
					UPDATE [UserSettings]
					   SET [UserId] = @UserId,
						   [Phone] = @Phone,
						   [Address] = @Address,
						   [PhotoPath] = @PhotoPath
					 WHERE [Id] = @Id",
				entity);
		}
	}
}
