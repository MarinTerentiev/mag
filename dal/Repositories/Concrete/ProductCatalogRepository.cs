using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Infrastructure;
using dal.Models.Data;
using dal.Repositories.Abstract;
using Dapper;

namespace dal.Repositories.Concrete
{
	public class ProductCatalogRepository : IProductCatalogRepository
	{
		private readonly IConnectionFactory _connectionFactory;

		public ProductCatalogRepository(IConnectionFactory connectionFactory)
		{
			this._connectionFactory = connectionFactory;
		}

		public ProductCatalog Get(int id)
		{
			return _connectionFactory.GetConnection.Query<ProductCatalog>(@"
					SELECT DEA.[Name]		AS 'DealerName', 
						   COM.[DealerId], 
						   COM.[Name]		AS 'CompanyName', 
						   CAT.[CompanyId], 
						   CAT.[Name]		AS 'CategoryName', 
						   PRO.*
					  FROM Dealer			AS DEA
					 INNER JOIN Company		AS COM ON COM.[DealerId] = DEA.Id		AND COM.[Status] = 'Active' AND COM.[Enable] = 1
					 INNER JOIN Category	AS CAT ON CAT.[CompanyId] = COM.Id		AND CAT.[Status] = 'Active'
					 INNER JOIN Product		AS PRO ON PRO.[CategoryId] = CAT.Id		AND PRO.[Status] = 'Active' AND PRO.[Enable] = 1
					 WHERE DEA.[Status] = 'Active'
					   AND DEA.[Enable] = 1
					   AND PRO.[Id] = @id",
				new { id }).FirstOrDefault();
		}

		public IEnumerable<ProductCatalog> Search(SearchProduct searchProduct)
		{
			return _connectionFactory.GetConnection.Query<ProductCatalog>(@"
					SELECT DEA.[Name]		AS 'DealerName', 
						   COM.[DealerId], 
						   COM.[Name]		AS 'CompanyName', 
						   CAT.[CompanyId], 
						   CAT.[Name]		AS 'CategoryName', 
						   PRO.*
					  FROM Dealer			AS DEA
					 INNER JOIN Company		AS COM ON COM.[DealerId] = DEA.Id		AND COM.[Status] = 'Active' AND COM.[Enable] = 1
					 INNER JOIN Category	AS CAT ON CAT.[CompanyId] = COM.Id		AND CAT.[Status] = 'Active'
					 INNER JOIN Product		AS PRO ON PRO.[CategoryId] = CAT.Id		AND PRO.[Status] = 'Active' AND PRO.[Enable] = 1
					 WHERE DEA.[Status] = 'Active'
					   AND DEA.[Enable] = 1
					   AND (@dealerId = -1 OR DEA.Id = @dealerId)
					   AND (@companyId = -1 OR COM.Id = @companyId)
					   AND (@categoryId = -1 OR CAT.Id = @categoryId)
					 ORDER BY PRO.Name DESC 
					OFFSET ((@pageNumber - 1) * @rowsPage) ROWS
					 FETCH NEXT @rowsPage ROWS ONLY;",
				searchProduct);
		}

		public int GetCountProduct(SearchProduct searchProduct)
		{
			return _connectionFactory.GetConnection.Query<int>(@"
					SELECT COUNT(*)
					  FROM Dealer			AS DEA
					 INNER JOIN Company		AS COM ON COM.[DealerId] = DEA.Id		AND COM.[Status] = 'Active' AND COM.[Enable] = 1
					 INNER JOIN Category	AS CAT ON CAT.[CompanyId] = COM.Id		AND CAT.[Status] = 'Active'
					 INNER JOIN Product		AS PRO ON PRO.[CategoryId] = CAT.Id		AND PRO.[Status] = 'Active' AND PRO.[Enable] = 1
					 WHERE DEA.[Status] = 'Active'
					   AND DEA.[Enable] = 1
					   AND (@dealerId = -1 OR DEA.Id = @dealerId)
					   AND (@companyId = -1 OR COM.Id = @companyId)
					   AND (@categoryId = -1 OR CAT.Id = @categoryId)",
				searchProduct).FirstOrDefault();
		} 
	}
}
