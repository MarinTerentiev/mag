using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Infrastructure;
using dal.Models.Data;

namespace dal.Repositories.Abstract
{
	public interface IProductCatalogRepository
	{
		ProductCatalog Get(int id);
		IEnumerable<ProductCatalog> Search(SearchProduct searchProduct);
		int GetCountProduct(SearchProduct searchProduct);
	}
}
