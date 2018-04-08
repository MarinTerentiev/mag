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
	public class CompanyController : BaseController
	{
		readonly ICompanyServices _companyServices;
		readonly IDealerServices _dealerServices;

		public CompanyController(ICompanyServices companyServices, IDealerServices dealerServices)
		{
			_companyServices = companyServices;
			_dealerServices = dealerServices;
		}

		[HttpGet]
		[Authorize(Roles = "Dealer")]
		[Route("api/company/get")]
		public IHttpActionResult Get()
		{
			var dealer = _dealerServices.GetByUserId(UserId);
			var companies = _companyServices.GetByDealerId(dealer.Id);

			return Ok(new
			{
				data = new
				{
					companies
				}
			});
		}

		[HttpGet]
		[Authorize(Roles = "Dealer")]
		[Route("api/company/get/{id}")]
		public IHttpActionResult Get(int id)
		{
			if (!_companyServices.IsOwner(id, UserId))
			{
				return Content(HttpStatusCode.Forbidden, "This is not your company");
			}

			var company = _companyServices.Get(id);

			return Ok(new
			{
				data = new
				{
					company
				}
			});
		}

		[HttpPost]
		[Authorize(Roles = "Dealer")]
		[Route("api/company/post")]
		public IHttpActionResult Post(Company company)
		{
			if (!_companyServices.IsOwner(company.Id, UserId) && company.Id != -1)
			{
				return Content(HttpStatusCode.Forbidden, "This is not your company");
			}

			if (company.Id == -1)
			{
				var dealer = _dealerServices.GetByUserId(UserId);
				company.DealerId = dealer.Id;
				company.Id = _companyServices.Insert(company);
			}
			else
			{
				_companyServices.Update(company);
			}

			return Ok(new
			{
				data = new
				{
					company
				}
			});
		}

		[HttpDelete]
		[Authorize(Roles = "Dealer")]
		[Route("api/company/delete/{id}")]
		public IHttpActionResult Delete(int id)
		{
			if (!_companyServices.IsOwner(id, UserId))
			{
				return Content(HttpStatusCode.Forbidden, "This is not your company");
			}

			_companyServices.Delete(id);

			return Ok(new
			{
				data = "OK"
			});
		}

		[HttpGet]
		[Authorize(Roles = "Customer")]
		[Route("api/company/getForCatalog")]
		public IHttpActionResult GetForCalalog()
		{
			var companies = _companyServices.GetAll().Where(x => x.Enable).Select(x => (Models.Entities.Company) x);

			return Ok(new
			{
				data = new
				{
					companies
				}
			});
		}
	}
}
