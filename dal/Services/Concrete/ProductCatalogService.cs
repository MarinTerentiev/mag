using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Infrastructure;
using dal.Models.Data;
using dal.Services.Abstract;

namespace dal.Services.Concrete
{
	public class ProductCatalogService : IProductCatalogService
	{
		private readonly IAutofacOfWork _autofacOfWork;

		public ProductCatalogService(IAutofacOfWork autofacOfWork)
		{
			this._autofacOfWork = autofacOfWork;
		}

		public ProductCatalog Get(int id)
		{
			return _autofacOfWork.ProductCatalogRepository.Get(id);
		}

		public IEnumerable<ProductCatalog> Search(SearchProduct searchProduct)
		{
			return _autofacOfWork.ProductCatalogRepository.Search(searchProduct);
		}

		public int GetCountProduct(SearchProduct searchProduct)
		{
			return _autofacOfWork.ProductCatalogRepository.GetCountProduct(searchProduct);
		}
	}
}
