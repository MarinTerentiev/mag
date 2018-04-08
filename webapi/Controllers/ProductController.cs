using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dal.Models.Entities;
using dal.Services.Abstract;

namespace webapi.Controllers
{
	public class ProductController : BaseController
	{
		readonly IProductServices _productServices;

		public ProductController(IProductServices productServices)
		{
			_productServices = productServices;
		}

		[HttpGet]
		[Authorize(Roles = "Dealer")]
		[Route("api/product/get")]
		public IHttpActionResult Get()
		{
			var products = _productServices.GetByUserId(UserId);

			return Ok(new
			{
				data = new
				{
					products
				}
			});
		}

		[HttpGet]
		[Authorize(Roles = "Dealer")]
		[Route("api/product/getByCompanyId/{companyId}")]
		public IHttpActionResult GetByCompanyId(int companyId)
		{
			var products = _productServices.GetByCompanyId(companyId);

			return Ok(new
			{
				data = new
				{
					products
				}
			});
		}

		[HttpGet]
		[Authorize(Roles = "Dealer")]
		[Route("api/product/get/{id}")]
		public IHttpActionResult Get(int id)
		{
			if (!_productServices.IsOwner(id, UserId))
			{
				return Content(HttpStatusCode.Forbidden, "This is not your product");
			}

			var product = _productServices.Get(id);

			return Ok(new
			{
				data = new
				{
					product
				}
			});
		}

		[HttpPost]
		[Authorize(Roles = "Dealer")]
		[Route("api/product/post")]
		public IHttpActionResult Post(Product product)
		{
			if (!_productServices.IsOwner(product.Id, UserId) && product.Id != -1)
			{
				return Content(HttpStatusCode.Forbidden, "This is not your product");
			}

			if (product.Id == -1)
			{
				product.Id = _productServices.Insert(product);
			}
			else
			{
				_productServices.Update(product);
			}

			return Ok(new
			{
				data = new
				{
					product
				}
			});
		}

		[HttpDelete]
		[Authorize(Roles = "Dealer")]
		[Route("api/product/delete/{id}")]
		public IHttpActionResult Delete(int id)
		{
			if (!_productServices.IsOwner(id, UserId))
			{
				return Content(HttpStatusCode.Forbidden, "This is not your product");
			}

			_productServices.Delete(id);

			return Ok(new
			{
				data = "OK"
			});
		}

		[HttpGet]
		[Authorize(Roles = "Dealer")]
		[Route("api/product/getCategory/{companyId}")]
		public IHttpActionResult GetCategory(int companyId)
		{
			var category = _productServices.GetCategoryByCompanyId(companyId);

			return Ok(new
			{
				data = new
				{
					category
				}
			});
		}

		[HttpPost]
		[Authorize(Roles = "Dealer")]
		[Route("api/product/saveCategory")]
		public IHttpActionResult SaveCategory(Category category)
		{
			if (category.Id == -1)
			{
				category.Id = _productServices.InsertCategory(category);
			}

			return Ok(new
			{
				data = new
				{
					category
				}
			});
		}

		[HttpDelete]
		[Authorize(Roles = "Dealer")]
		[Route("api/product/deleteCategory/{id}")]
		public IHttpActionResult DeleteCategory(int id)
		{
			if (!_productServices.IsOwnerCategory(id, UserId))
			{
				return Content(HttpStatusCode.Forbidden, "This is not your category");
			}

			_productServices.DeleteCategory(id);

			return Ok(new
			{
				data = "OK"
			});
		}

		[HttpGet]
		[Authorize(Roles = "Customer")]
		[Route("api/product/getCategoryForCatalog")]
		public IHttpActionResult GetCategoryForCatalog()
		{
			var categories = _productServices.GetCategoryAll();

			return Ok(new
			{
				data = new
				{
					categories
				}
			});
		}
	}
}
