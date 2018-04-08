﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Models.Entities
{
	public class User : BaseClass
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Roles { get; set; }
	}
}
