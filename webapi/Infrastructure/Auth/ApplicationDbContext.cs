using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using Dapper;

namespace webapi.Infrastructure.Auth
{
	public class ApplicationDbContext : IDbContext
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

		public ApplicationDbContext(string conn)
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


		public int Create(dal.Models.Entities.User user)
		{
			throw new NotImplementedException();
		}

		public void Update(dal.Models.Entities.User user)
		{
			throw new NotImplementedException();
		}

		public void Delete(User user)
		{
			throw new NotImplementedException();
		}

		public dal.Models.Entities.User GetByIdActive(int id)
		{
			throw new NotImplementedException();
		}

		public dal.Models.Entities.User GetByUserNameActive(string userName)
		{
			return GetConnection.Query<dal.Models.Entities.User>(@"
					SELECT *
					  FROM [User]
					 WHERE [UserName] = @userName",
				new { userName }).FirstOrDefault();
		}

		public dal.Models.Entities.User GetByUserNameAndPassActive(string userName, string pass)
		{
			throw new NotImplementedException();
		}
	}
}