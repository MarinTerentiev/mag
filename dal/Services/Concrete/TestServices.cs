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
	public class TestServices : ITestServices
	{

		private readonly IAutofacOfWork _autofacOfWork;

		public TestServices(IAutofacOfWork autofacOfWork)
		{
			this._autofacOfWork = autofacOfWork;
		}


		public Test1 Get(int id)
		{
			var ret = _autofacOfWork.TestRepositories.Get(id);
			return ret;
		}
	}
}
