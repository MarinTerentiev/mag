using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Models.Entities
{
	public class Dealer : BaseClass
	{
		public int UserId { get; set; }
		public string Name { get; set; }
		public bool Enable { get; set; }
		public double Amount { get; set; }
	}
}
