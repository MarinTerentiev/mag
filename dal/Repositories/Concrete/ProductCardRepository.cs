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
	public class ProductCardRepository : IProductCardRepository
	{
		readonly IConnectionFactory _connectionFactory;

		public ProductCardRepository(IConnectionFactory connectionFactory)
		{
			this._connectionFactory = connectionFactory;
		}

		public ProductCard Get(int id)
		{
			return _connectionFactory.GetConnection.Query<ProductCard>(@"
					SELECT *
					  FROM [ProductCard]
					 WHERE [Id] = @id",
				new { id }).FirstOrDefault();
		}

		public IEnumerable<ProductCard> GetAll()
		{
			return _connectionFactory.GetConnection.Query<ProductCard>(@"
					SELECT *
					  FROM [ProductCard]
					 WHERE [Status] != 'Deleted'");
		}

		public IEnumerable<ProductCard> GetByOrderId(int orderId)
		{
			return _connectionFactory.GetConnection.Query<ProductCard>(@"
					SELECT C.*,
						   P.[Name]	AS 'Name',
						   M.[Name]	AS 'CompanyName'
					  FROM [ProductCard]	AS C
					 INNER JOIN [Product]	AS P ON P.[Id] = C.[ProductId]
					 INNER JOIN [Category]	AS T ON T.[Id] = P.CategoryId
					 INNER JOIN [Company]	AS M ON M.[Id] = T.CompanyId
					 WHERE C.[Status] != 'Deleted'
					   AND [OrderId] = @orderId",
				new { orderId });
		}

		public IEnumerable<ProductCard> GetByDealerId(int orderId, int dealerId)
		{
			return _connectionFactory.GetConnection.Query<ProductCard>(@"
					SELECT C.*,
						   P.[Name]	AS 'Name',
						   M.[Name]	AS 'CompanyName'
					  FROM [ProductCard]	AS C
					 INNER JOIN [Product]	AS P ON P.[Id] = C.[ProductId]
					 INNER JOIN [Category]	AS T ON T.[Id] = P.CategoryId
					 INNER JOIN [Company]	AS M ON M.[Id] = T.CompanyId
					 WHERE C.[Status] != 'Deleted'
					   AND C.[DealerId] = @dealerId
					   AND C.[OrderId] = @orderId",
				new { dealerId, orderId });
		}

		public int Insert(ProductCard entity)
		{
			return _connectionFactory.GetConnection.Query<int>(@"
					INSERT INTO [ProductCard](
						[ProductId], 
						[OrderId],
						[DealerId],
						[Price],
						[Count])
					VALUES(
						@ProductId,
						@OrderId,
						@DealerId,
						@Price,
						@Count);
					SELECT SCOPE_IDENTITY();",
				entity).FirstOrDefault();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(ProductCard entity)
		{
			throw new NotImplementedException();
		}
	}
}
