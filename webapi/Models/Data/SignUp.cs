﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models.Data
{
	public class SignUp
	{
		public string UserName { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }
	}
}