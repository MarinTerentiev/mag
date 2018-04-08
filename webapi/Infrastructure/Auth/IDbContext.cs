using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webapi.Infrastructure.Auth
{
	public interface IDbContext
	{
		int Create(dal.Models.Entities.User user);

		void Update(dal.Models.Entities.User user);

		void Delete(User user);

		dal.Models.Entities.User GetByIdActive(int id);

		dal.Models.Entities.User GetByUserNameActive(string userName);

		dal.Models.Entities.User GetByUserNameAndPassActive(string userName, string pass);
	}
}
