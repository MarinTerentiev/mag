using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Models.Entities
{
	public class UserSettings : BaseClass
	{
		public UserSettings()
		{
			Phone = string.Empty;
			Address = string.Empty;
			PhotoPath = string.Empty;
		}

		public int UserId { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string PhotoPath { get; set; }
	}
}
