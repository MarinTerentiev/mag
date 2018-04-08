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
	public class ProductServices : IProductServices
	{
		private readonly IAutofacOfWork _autofacOfWork;

		public ProductServices(IAutofacOfWork autofacOfWork)
		{
			this._autofacOfWork = autofacOfWork;
		}

		public Product Get(int id)
		{
			return _autofacOfWork.ProductRepository.Get(id);
		}

		public IEnumerable<Product> GetByCompanyId(int companyId)
		{
			return _autofacOfWork.ProductRepository.GetByCompanyId(companyId);
		}

		public IEnumerable<Product> GetAll()
		{
			return _autofacOfWork.ProductRepository.GetAll();
		}

		public IEnumerable<Product> GetByUserId(int userId)
		{
			return _autofacOfWork.ProductRepository.GetByUserId(userId);
		}

		public int Insert(Product entity)
		{
			return _autofacOfWork.ProductRepository.Insert(entity);
		}

		public void Delete(int id)
		{
			_autofacOfWork.ProductRepository.Delete(id);
		}

		public void Update(Product entity)
		{
			_autofacOfWork.ProductRepository.Update(entity);
		}

		public bool IsOwner(int id, int userId)
		{
			return _autofacOfWork.ProductRepository.IsOwner(id, userId);
		}


		public Category GetCategory(int id)
		{
			return _autofacOfWork.CategoryRepository.Get(id);
		}

		public IEnumerable<Category> GetCategoryByCompanyId(int companyId)
		{
			return _autofacOfWork.CategoryRepository.GetByCompanyId(companyId);
		}

		public int InsertCategory(Category entity)
		{
			return _autofacOfWork.CategoryRepository.Insert(entity);
		}

		public void DeleteCategory(int id)
		{
			_autofacOfWork.CategoryRepository.Delete(id);
		}

		public bool IsOwnerCategory(int id, int userId)
		{
			return _autofacOfWork.CategoryRepository.IsOwner(id, userId);
		}

		public IEnumerable<Category> GetCategoryAll()
		{
			return _autofacOfWork.CategoryRepository.GetAll();
		}
	}
}
