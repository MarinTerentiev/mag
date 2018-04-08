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
	public class CategoryRepository : ICategoryRepository
	{
		readonly IConnectionFactory _connectionFactory;

		public CategoryRepository(IConnectionFactory connectionFactory)
		{
			this._connectionFactory = connectionFactory;
		}

		public Category Get(int id)
		{
			return _connectionFactory.GetConnection.Query<Category>(@"
					SELECT *
					  FROM [Category]
					 WHERE [Id] = @id",
				new { id }).FirstOrDefault();
		}

		public IEnumerable<Category> GetAll()
		{
			return _connectionFactory.GetConnection.Query<Category>(@"
					SELECT *
					  FROM [Category]
					 WHERE [Status] != @status",
				new { status = "Deleted" });
		}

		public IEnumerable<Category> GetByCompanyId(int companyId)
		{
			return _connectionFactory.GetConnection.Query<Category>(@"
					SELECT *
					  FROM [Category]
					 WHERE [CompanyId] = @companyId
					   AND [Status] != @status",
				new { companyId, status = "Deleted" });
		}

		public int Insert(Category entity)
		{
			return _connectionFactory.GetConnection.Query<int>(@"
					INSERT INTO [Category](
						[CompanyId],
						[Name])
					VALUES(
						@CompanyId,
						@Name);
					SELECT SCOPE_IDENTITY();",
				entity).FirstOrDefault();
	}

		public void Delete(int id)
		{
			_connectionFactory.GetConnection.Query(@"
					UPDATE [Category]
					   SET [Status] = 'Deleted'
					 WHERE [Id] = @Id",
				new { id });
		}

		public void Update(Category entity)
		{
			_connectionFactory.GetConnection.Query(@"
					UPDATE [Category]
					   SET [CompanyId] = @CompanyId,
						   [Name] = @Name
					 WHERE [Id] = @Id",
				entity);
		}

		public bool IsOwner(int id, int userId)
		{
			var category = _connectionFactory.GetConnection.Query<Company>(@"
					SELECT *
					  FROM [User]			AS US
					 INNER JOIN [Dealer]	AS DL ON DL.UserId = US.ID		AND DL.[Status] != 'Deleted'
					 INNER JOIN [Company]	AS CM ON CM.DealerId = DL.Id	AND CM.[Status] != 'Deleted'
					 INNER JOIN [Category]	AS CT ON CT.CompanyId = CM.Id	AND CT.[Status] != 'Deleted'
					 WHERE CT.Id = @id
					   AND US.Id = @userId
					   AND US.[Status] != 'Deleted'",
				new { id, userId }).FirstOrDefault();

			return category != null;
		}
	}
}
