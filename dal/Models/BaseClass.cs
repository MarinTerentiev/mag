using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Models
{
	public class BaseClass
	{
		public int Id { get; set; }
		public string Status { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
	}
}