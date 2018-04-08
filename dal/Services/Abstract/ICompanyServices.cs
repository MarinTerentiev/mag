using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Models.Entities;

namespace dal.Services.Abstract
{
	public interface ICompanyServices
	{
		Company Get(int id);
		IEnumerable<Company> GetAll();
		IEnumerable<Company> GetByDealerId(int dealerId);
		int Insert(Company entity);
		void Delete(int id);
		void Update(Company entity);
		bool IsOwner(int id, int userId);
	}
}
