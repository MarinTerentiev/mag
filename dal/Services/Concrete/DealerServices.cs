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
	public class DealerServices : IDealerServices
	{
		private readonly IAutofacOfWork _autofacOfWork;

		public DealerServices(IAutofacOfWork autofacOfWork)
		{
			this._autofacOfWork = autofacOfWork;
		}


		public Dealer Get(int id)
		{
			return _autofacOfWork.DealerRepository.Get(id);
		}

		public Dealer GetByUserId(int userId)
		{
			return _autofacOfWork.DealerRepository.GetByUserId(userId);
		}

		public IEnumerable<Dealer> GetAll()
		{
			return _autofacOfWork.DealerRepository.GetAll();
		}

		public int Insert(Dealer dealer)
		{
			return _autofacOfWork.DealerRepository.Insert(dealer);
		}

		public void Update(Dealer dealer)
		{
			_autofacOfWork.DealerRepository.Update(dealer);
		}

		public void Delete(int id)
		{
			_autofacOfWork.DealerRepository.Delete(id);
		}

		public bool ExistName(string name)
		{
			return _autofacOfWork.DealerRepository.ExistName(name);
		}
	}
}
