using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models.Data
{
	public class ResetPassword
	{
		public string UserName { get; set; }
		public string Key { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }
	}
}
