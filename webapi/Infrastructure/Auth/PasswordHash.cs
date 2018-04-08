using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace webapi.Infrastructure.Auth
{
	public class PasswordHash
	{
		public static string GetPass(string pass)
		{
			byte[] data = Encoding.ASCII.GetBytes("c5a8e141-d7d2-4dfc-bcee-3a2ee2c46889" + pass);
			data = MD5.Create().ComputeHash(data);
			return Convert.ToBase64String(data);
		}
	}
}