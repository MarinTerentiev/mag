using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dal.Models.Data;
using dal.Services.Abstract;

namespace webapi.Controllers
{
	public class ProductCatalogController : BaseController
	{
		readonly IProductCatalogService _productCatalogService;

		public ProductCatalogController(IProductCatalogService productCatalogService)
		{
			_productCatalogService = productCatalogService;
		}

		[HttpPost]
		[Authorize(Roles = "Customer")]
		[Route("api/productCatalog/search")]
		public IHttpActionResult Search(SearchProduct searchProduct)
		{
			var products = _productCatalogService.Search(searchProduct);

			return Ok(new
			{
				data = new
				{
					products
				}
			});
		}

		[HttpPost]
		[Authorize(Roles = "Customer")]
		[Route("api/productCatalog/getCountProduct")]
		public IHttpActionResult GetCountProduct(SearchProduct searchProduct)
		{
			var count = _productCatalogService.GetCountProduct(searchProduct);

			return Ok(new
			{
				data = new
				{
					count
				}
			});
		}

		[HttpGet]
		[Authorize(Roles = "Customer")]
		[Route("api/productCatalog/get/{id}")]
		public IHttpActionResult Get(int id)
		{
			var product = _productCatalogService.Get(id);

			return Ok(new
			{
				data = new
				{
					product
				}
			});
		}
	}
}
