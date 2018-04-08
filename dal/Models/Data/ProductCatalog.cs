using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Models.Entities;

namespace dal.Models.Data
{
	public class ProductCatalog : Product
	{
		public string DealerName { get; set; }
		public int DealerId { get; set; }
		public string CompanyName { get; set; }
		public string CompanyId { get; set; }
	}
}
