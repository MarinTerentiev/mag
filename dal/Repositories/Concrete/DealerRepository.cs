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
	public class DealerRepository : IDealerRepository
	{
		readonly IConnectionFactory _connectionFactory;

		public DealerRepository(IConnectionFactory connectionFactory)
		{
			this._connectionFactory = connectionFactory;
		}

		public Dealer Get(int id)
		{
			return _connectionFactory.GetConnection.Query<Dealer>(@"
					SELECT *
					  FROM [Dealer]
					 WHERE [Id] = @id",
				new { id }).FirstOrDefault();
		}

		public Dealer GetByUserId(int userId)
		{
			return _connectionFactory.GetConnection.Query<Dealer>(@"
					SELECT * 
					  FROM [Dealer] 
					 WHERE [UserId] = @userId
					   AND [Status] != 'Deleted'",
				new { userId }).FirstOrDefault();

		}

		public IEnumerable<Dealer> GetAll()
		{
			return _connectionFactory.GetConnection.Query<Dealer>(@"
					SELECT * 
					  FROM [Dealer]
					 WHERE [Status] != 'Deleted'");
		}

		public int Insert(Dealer entity)
		{
			return _connectionFactory.GetConnection.Query<int>(@"
					INSERT INTO [Dealer](
						[UserId], 
						[Name], 
						[Enable], 
						[Amount])
					VALUES(
						@UserId,
						@Name,
						@Enable,
						@Amount);
					SELECT SCOPE_IDENTITY();",
				entity).FirstOrDefault();
		}

		public void Delete(int id)
		{
			_connectionFactory.GetConnection.Query(@"
					UPDATE [Dealer]
					   SET [Status] = 'Deleted'
					 WHERE [Id] = @Id",
				new { id });
		}

		public void Update(Dealer entity)
		{
			_connectionFactory.GetConnection.Query(@"
					UPDATE [Dealer]
					   SET [UserId] = @UserId,
						   [Name] = @Name,
						   [Enable] = @Enable,
						   [Amount] = @Amount
					 WHERE [Id] = @Id",
				entity);
		}

		public bool ExistName(string name)
		{
			var dealer = _connectionFactory.GetConnection.Query<Dealer>(@"
					SELECT * 
					  FROM [Dealer] 
					 WHERE [Name] = @name",
				new { name }).FirstOrDefault();

			return dealer != null;
		}
	}
}
