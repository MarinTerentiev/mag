using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Models.Entities
{
	public class Product : BaseClass
	{
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool Enable { get; set; }
		public decimal Price { get; set; }
		public string CategoryName { get; set; }
		public string PhotoPath { get; set; }
	}
}
