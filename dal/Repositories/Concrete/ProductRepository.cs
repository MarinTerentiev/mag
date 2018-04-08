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
	public class ProductRepository : IProductRepository
	{
		readonly IConnectionFactory _connectionFactory;

		public ProductRepository(IConnectionFactory connectionFactory)
		{
			this._connectionFactory = connectionFactory;
		}

		public Product Get(int id)
		{
			return _connectionFactory.GetConnection.Query<Product>(@"
					SELECT *
					  FROM [Product]
					 WHERE [Id] = @id",
				new { id }).FirstOrDefault();
		}

		public IEnumerable<Product> GetAll()
		{
			return _connectionFactory.GetConnection.Query<Product>(@"
					SELECT *
					  FROM [Product]
					 WHERE [Status] != 'Deleted'");
		}

		public IEnumerable<Product> GetByUserId(int userId)
		{
			return _connectionFactory.GetConnection.Query<Product>(@"
					SELECT PR.*, 
						   CT.[Name] AS 'CategoryName'
					  FROM [User]			AS US
					 INNER JOIN [Dealer]	AS DL ON DL.[UserId] = US.[ID]		AND DL.[Status] != 'Deleted'
					 INNER JOIN [Company]	AS CM ON CM.[DealerId] = DL.[Id]	AND CM.[Status] != 'Deleted'
					 INNER JOIN [Category]	AS CT ON CT.[CompanyId] = CM.[Id]	AND CT.[Status] != 'Deleted'
					 INNER JOIN [Product]	AS PR ON PR.[CategoryId] = CT.[Id]	AND PR.[Status] != 'Deleted'
					 WHERE US.[Id] = @userId
					   AND US.[Status] != 'Deleted'", 
				new { userId });
		}

		public IEnumerable<Product> GetByCompanyId(int companyId)
		{
			return _connectionFactory.GetConnection.Query<Product>(@"
					SELECT PR.*, 
						   CT.[Name] AS 'CategoryName'
					  FROM [Company]		AS CM 
					 INNER JOIN [Category]	AS CT ON CT.[CompanyId] = CM.[Id]	AND CT.[Status] != 'Deleted'
					 INNER JOIN [Product]	AS PR ON PR.[CategoryId] = CT.[Id]	AND PR.[Status] != 'Deleted'
					 WHERE CM.[Id] = @companyId
					   AND CM.[Status] != 'Deleted'",
				new { companyId });
		}

		public int Insert(Product entity)
		{
			return _connectionFactory.GetConnection.Query<int>(@"
					INSERT INTO [Product](
						[CategoryId], 
						[Name],
						[Description],
						[Enable],
						[Price],
						[PhotoPath])
					VALUES(
						@CategoryId,
						@Name,
						@Description,
						@Enable,
						@Price,
						@PhotoPath);
					SELECT SCOPE_IDENTITY();",
				entity).FirstOrDefault();
		}

		public void Delete(int id)
		{
			_connectionFactory.GetConnection.Query(@"
					UPDATE [Product]
					   SET [Status] = 'Deleted'
					 WHERE [Id] = @Id",
				new { id });
		}

		public void Update(Product entity)
		{
			_connectionFactory.GetConnection.Query(@"
					UPDATE [Product]
					   SET [CategoryId] = @CategoryId,
						   [Name] = @Name,
						   [Description] = @Description,
						   [Enable] = @Enable,
						   [Price] = @Price,
						   [PhotoPath] = @PhotoPath
					 WHERE [Id] = @Id",
				entity);
		}

		public bool IsOwner(int id, int userId)
		{
			var company = _connectionFactory.GetConnection.Query<Company>(@"
					SELECT *
					  FROM [User]			AS US
					 INNER JOIN [Dealer]	AS DL ON DL.UserId = US.ID		AND DL.[Status] != 'Deleted'
					 INNER JOIN [Company]	AS CM ON CM.DealerId = DL.Id	AND CM.[Status] != 'Deleted'
					 INNER JOIN [Category]	AS CT ON CT.CompanyId = CM.Id	AND CT.[Status] != 'Deleted'
					 INNER JOIN [Product]	AS PR ON PR.CategoryId = CT.Id	AND PR.[Status] != 'Deleted'
					 WHERE PR.Id = @id
					   AND US.Id = @userId
					   AND US.[Status] != 'Deleted'",
				new { id, userId }).FirstOrDefault();

			return company != null;
		}
	}
}
