using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Models.Entities;

namespace dal.Services.Abstract
{
	public interface IDealerServices
	{
		Dealer Get(int id);

		Dealer GetByUserId(int userId);

		IEnumerable<Dealer> GetAll();

		int Insert(Dealer dealer);

		void Update(Dealer dealer);

		void Delete(int id);

		bool ExistName(string name);
	}
}
