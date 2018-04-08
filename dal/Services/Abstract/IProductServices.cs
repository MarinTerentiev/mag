using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Models.Entities;

namespace dal.Services.Abstract
{
	public interface IProductServices
	{
		Product Get(int id);
		IEnumerable<Product> GetAll();
		IEnumerable<Product> GetByUserId(int userId);
		IEnumerable<Product> GetByCompanyId(int companyId);
		int Insert(Product entity);
		void Delete(int id);
		void Update(Product entity);
		bool IsOwner(int id, int userId);

		Category GetCategory(int id);
		IEnumerable<Category> GetCategoryByCompanyId(int companyId);
		int InsertCategory(Category entity);
		void DeleteCategory(int id);
		bool IsOwnerCategory(int id, int userId);
		IEnumerable<Category> GetCategoryAll();
	}
}
