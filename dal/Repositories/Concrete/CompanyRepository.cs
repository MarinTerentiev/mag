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
	public class CompanyRepository : ICompanyRepository
	{
		readonly IConnectionFactory _connectionFactory;

		public CompanyRepository(IConnectionFactory connectionFactory)
		{
			this._connectionFactory = connectionFactory;
		}

		public Company Get(int id)
		{
			return _connectionFactory.GetConnection.Query<Company>(@"
					SELECT *
					  FROM [Company]
					 WHERE [Id] = @id",
				new { id }).FirstOrDefault();
		}

		public IEnumerable<Company> GetAll()
		{
			return _connectionFactory.GetConnection.Query<Company>(@"
					SELECT *
					  FROM [Company]
					 WHERE [Status] != 'Deleted'");
		}

		public IEnumerable<Company> GetByDealerId(int dealerId)
		{
			return _connectionFactory.GetConnection.Query<Company>(@"
					SELECT *
					  FROM [Company]
					 WHERE [DealerId] = @dealerId
					   AND [Status] != 'Deleted'",
				new { dealerId });
		}

		public int Insert(Company entity)
		{
			return _connectionFactory.GetConnection.Query<int>(@"
					INSERT INTO [Company](
						[DealerId],
						[Name],
						[Enable])
					VALUES(
						@DealerId,
						@Name,
						@Enable);
					SELECT SCOPE_IDENTITY();",
				entity).FirstOrDefault();
		}

		public void Delete(int id)
		{
			_connectionFactory.GetConnection.Query(@"
					UPDATE [Company]
					   SET [Status] = 'Deleted'
					 WHERE [Id] = @Id",
				new { id });
		}

		public void Update(Company entity)
		{
			_connectionFactory.GetConnection.Query(@"
					UPDATE [Company]
					   SET [DealerId] = @DealerId,
						   [Name] = @Name,
						   [Enable] = @Enable
					 WHERE [Id] = @Id",
				entity);
		}

		public bool IsOwner(int id, int userId)
		{
			var company = _connectionFactory.GetConnection.Query<Company>(@"
					SELECT *
					  FROM [User]			AS US
					 INNER JOIN [Dealer]	AS DL ON DL.UserId = US.ID
					 INNER JOIN [Company]	AS CM ON CM.DealerId = DL.Id
					 WHERE CM.Id = @id
					   AND US.Id = @userId",
				new {id, userId}).FirstOrDefault();

			return company != null;
		}
	}
}
