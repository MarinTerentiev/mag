using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Models.Entities
{
	public class Company : BaseClass
	{
		public int DealerId { get; set; }
		public string Name { get; set; }
		public bool Enable { get; set; }
	}
}
