using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace webapi.Common
{
	public class Tool
	{
		public static string GetProfilePath()
		{
			return "~/Uploads/Profile/";
		}

		public static string GetConnectionString()
		{
			var conn = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
			return conn;
		}
	}
}