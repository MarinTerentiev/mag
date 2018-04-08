using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Models.Entities
{
	public class Order : BaseClass
	{
		public Order()
		{
			ProductCards = new List<ProductCard>();
		}

		public double Amount { get; set; }
		public string Address { get; set; }
		public string Details { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }
		public IEnumerable<ProductCard> ProductCards { get; set; }
	}
}
