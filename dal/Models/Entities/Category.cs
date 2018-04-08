using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Models.Entities
{
	public class Category : BaseClass
	{
		public int CompanyId { get; set; }
		public string Name { get; set; }
	}
}
