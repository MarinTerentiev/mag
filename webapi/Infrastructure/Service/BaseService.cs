using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace webapi.Infrastructure.Service
{
	public class BaseService
	{
		public static IEnumerable<T> GetActive<T>(SqlConnection conn)
		{
			var table = typeof(T).Name;
			var ret = conn.Query<T>($"SELECT * FROM [{table}] WHERE Status=@status", new { status = Common.Enum.Status.Active.ToString() });
			return ret;
		}

		public static T GetById<T>(SqlConnection conn, int id)
		{
			var table = typeof(T).Name;
			var ret = conn.Query<T>($"SELECT * FROM [{table}] WHERE ID=@id AND Status=@status", new { id, status = Common.Enum.Status.Active.ToString() }).FirstOrDefault();
			return ret;
		}

		public static IEnumerable<T> GetDeleted<T>(SqlConnection conn)
		{
			var table = typeof(T).Name;
			var ret = conn.Query<T>($"SELECT * FROM [{table}] WHERE Status=@status", new { status = Common.Enum.Status.Deleted.ToString() });
			return ret;
		}

		public static string GetTest(SqlConnection conn)
		{
			string str = conn.Query<string>($"SELECT test FROM [test] WHERE id=1").FirstOrDefault();
			return str;
		}
	}
}