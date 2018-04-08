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
	public class OrderRepository : IOrderRepository
	{
		readonly IConnectionFactory _connectionFactory;

		public OrderRepository(IConnectionFactory connectionFactory)
		{
			this._connectionFactory = connectionFactory;
		}

		public Order Get(int id)
		{
			return _connectionFactory.GetConnection.Query<Order>(@"
					SELECT O.*,
						   U.UserName AS 'UserName'
					  FROM [Order]		AS O
					 INNER JOIN [User]	AS U ON U.[ID] = O.[UserId]
					 WHERE O.[Id] = @id",
				new { id }).FirstOrDefault();
		}

		public Order Get(int id, int dealerId)
		{
			return _connectionFactory.GetConnection.Query<Order>(@"
					IF EXISTS (SELECT * FROM ProductCard WHERE [DealerId] = @dealerId AND [OrderId] = @id) 
					BEGIN
						SELECT O.*,
							   U.UserName AS 'UserName'
						  FROM [Order]		AS O
						 INNER JOIN [User]	AS U ON U.[ID] = O.[UserId]
						 WHERE O.[Id] = @id
					END",
				new { id, dealerId }).FirstOrDefault();
		}

		public IEnumerable<Order> GetAll()
		{
			return _connectionFactory.GetConnection.Query<Order>(@"
					SELECT O.*,
						   U.UserName AS 'UserName'
					  FROM [Order]		AS O
					 INNER JOIN [User]	AS U ON U.[ID] = O.[UserId]
					 WHERE O.[Status] != 'Deleted'");
		}

		public IEnumerable<Order> GetByDealer(int dealerId)
		{
			return _connectionFactory.GetConnection.Query<Order>(@"
					SELECT O.*,
						   U.UserName AS 'UserName'
					  FROM [Order]		AS O
					 INNER JOIN [User]	AS U ON U.[ID] = O.[UserId]
					 WHERE O.[Status] != 'Deleted'
					   AND O.[Id] IN (SELECT OrderId
										FROM [ProductCard]
									   WHERE [DealerId] = @dealerId
										 AND [Status] != 'Deleted')",
				new { dealerId });
		}

		public int Insert(Order entity)
		{
			return _connectionFactory.GetConnection.Query<int>(@"
					INSERT INTO [Order](
						[UserId],
						[Amount],
						[Address],
						[Details])
					VALUES(
						@UserId,
						@Amount,
						@Address,
						@Details);
					SELECT SCOPE_IDENTITY();",
				entity).FirstOrDefault();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(Order entity)
		{
			throw new NotImplementedException();
		}
	}
}
