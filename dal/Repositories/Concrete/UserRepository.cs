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
	public class UserRepository : IUserRepository
	{
		readonly IConnectionFactory _connectionFactory;

		public UserRepository(IConnectionFactory connectionFactory)
		{
			this._connectionFactory = connectionFactory;
		}

		public User Get(int id)
		{
			return _connectionFactory.GetConnection.Query<User>(@"
					SELECT * 
					  FROM [User] 
					 WHERE [Id] = @id",
				new { id }).FirstOrDefault();
		}

		public IEnumerable<User> GetAll()
		{
			return _connectionFactory.GetConnection.Query<User>(@"
					SELECT * 
					  FROM [User] 
					 WHERE [Status] != 'Deleted'");
		}

		public int Insert(User entity)
		{
			return _connectionFactory.GetConnection.Query<int>(@"
					INSERT INTO [User](
						[UserName], 
						[Password],
						[Name], 
						[Email], 
						[Roles],
						[Status])
					VALUES(
						@UserName,
						@Password,
						@Name,
						@Email,
						@Roles,
						@Status);
					SELECT SCOPE_IDENTITY();",
				entity).FirstOrDefault();
		}

		public void Delete(int id)
		{
			_connectionFactory.GetConnection.Query(@"
					UPDATE [User]
					   SET [Status] = 'Deleted'
					 WHERE [Id] = @Id",
				new { id });
		}

		public void Update(User entity)
		{
			_connectionFactory.GetConnection.Query(@"
					UPDATE [User]
					   SET [UserName] = @UserName,
						   [Name] = @Name,
						   [Email] = @Email,
						   [Roles] = @Roles
					 WHERE [Id] = @Id",
				entity);
		}

		public void UpdatePassword(User entity)
		{
			_connectionFactory.GetConnection.Query(@"
					UPDATE [User]
					   SET [Password] = @Password
					 WHERE [Id] = @Id",
				entity);
		}

		public User GetByIdActive(int id)
		{
			var user = _connectionFactory.GetConnection.Query<User>(@"
					SELECT * 
					  FROM [User] 
					 WHERE [Id] = @id
					   AND [Status] = 'Active'",
				new { id }).FirstOrDefault();
			return user;
		}

		public User GetByUserNameActive(string userName)
		{		
			var user = _connectionFactory.GetConnection.Query<User>(@"
					SELECT * 
					  FROM [User] 
					 WHERE [UserName] = @userName
					   AND [Status] = 'Active'", 
				new { userName }).FirstOrDefault();
			return user;
		}

		public User GetByUserNameAndPassActive(string userName, string pass)
		{
			var user = _connectionFactory.GetConnection.Query<User>(@"
					SELECT * 
					  FROM [User] 
					 WHERE [UserName] = @userName
					   AND [Password] = @pass
					   AND [Status] = 'Active'",
				new { userName, pass }).FirstOrDefault();
			return user;
		}

		public User GetByEmailActive(string email)
		{
			var user = _connectionFactory.GetConnection.Query<User>(@"
					SELECT * 
					  FROM [User] 
					 WHERE [Email] = @email
					   AND [Status] = 'Active'",
				new { email }).FirstOrDefault();
			return user;
		}

		public bool ExistUserName(string userName)
		{
			var user = _connectionFactory.GetConnection.Query<User>(@"
					SELECT * 
					  FROM [User] 
					 WHERE [UserName] = @userName",
				new { userName }).FirstOrDefault();

			return user != null;
		}

		public bool ExistEmail(string email)
		{
			var user = _connectionFactory.GetConnection.Query<User>(@"
					SELECT * 
					  FROM [User] 
					 WHERE [Email] = @email",
				new { email }).FirstOrDefault();

			return user != null;
		}
	}
}
