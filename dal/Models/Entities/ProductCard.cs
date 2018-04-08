using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Models.Entities
{
	public class ProductCard : BaseClass
	{
		public int ProductId { get; set; }
		public int Count { get; set; }
		public double Price { get; set; }
		public int OrderId { get; set; }
		public int DealerId { get; set; }
		public string Name { get; set; }
		public string CompanyName { get; set; }
	}
}
