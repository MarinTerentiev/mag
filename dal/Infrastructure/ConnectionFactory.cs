using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Infrastructure
{
	public class ConnectionFactory: IConnectionFactory
	{
		private bool _disposedValue = false;

		private readonly string _connectionString;
		public IDbConnection GetConnection
		{
			get
			{
				var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
				var conn = factory.CreateConnection();
				conn.ConnectionString = _connectionString;
				return conn;
			}
		}


		public ConnectionFactory(string conn)
		{
			_connectionString = conn;
		}


		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				_disposedValue = true;
			}
		}
		public void Dispose()
		{
			Dispose(true);
		}
	}
}
