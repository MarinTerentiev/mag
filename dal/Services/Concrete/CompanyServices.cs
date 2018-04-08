using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Infrastructure;
using dal.Models.Entities;
using dal.Services.Abstract;

namespace dal.Services.Concrete
{
	public class CompanyServices : ICompanyServices
	{
		private readonly IAutofacOfWork _autofacOfWork;

		public CompanyServices(IAutofacOfWork autofacOfWork)
		{
			this._autofacOfWork = autofacOfWork;
		}

		public Company Get(int id)
		{
			return _autofacOfWork.CompanyRepository.Get(id);
		}

		public IEnumerable<Company> GetAll()
		{
			return _autofacOfWork.CompanyRepository.GetAll();
		}

		public IEnumerable<Company> GetByDealerId(int dealerId)
		{
			return _autofacOfWork.CompanyRepository.GetByDealerId(dealerId);
		}

		public int Insert(Company entity)
		{
			return _autofacOfWork.CompanyRepository.Insert(entity);
		}

		public void Delete(int id)
		{
			_autofacOfWork.CompanyRepository.Delete(id);
		}

		public void Update(Company entity)
		{
			_autofacOfWork.CompanyRepository.Update(entity);
		}

		public bool IsOwner(int id, int userId)
		{
			return _autofacOfWork.CompanyRepository.IsOwner(id, userId);
		}
	}
}
