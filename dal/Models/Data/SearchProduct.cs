using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Models.Data
{
	public class SearchProduct
	{
		public int DealerId { get; set; }
		public int CompanyId { get; set; }
		public int CategoryId { get; set; }
		public int PageNumber { get; set; }
		public int RowsPage { get; set; }
	}
}
