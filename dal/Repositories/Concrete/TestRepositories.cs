using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Infrastructure;
using dal.Models.Entities;
using dal.Repositories.Abstract;
using Dapper;

namespace dal.Repositories.Concrete
{
	public class TestRepositories : ITestRepositories
	{
		readonly IConnectionFactory _connectionFactory;

		public TestRepositories(IConnectionFactory connectionFactory)
		{
			this._connectionFactory = connectionFactory;
		}


		public Test1 Get(int id)
		{
			return _connectionFactory.GetConnection.Query<Test1>(@"
					SELECT * 
					  FROM [test] 
					 WHERE [Id] = @id", 
				new { id }).FirstOrDefault();
		}

		public IEnumerable<Test1> GetAll()
		{
			throw new NotImplementedException();
		}

		public int Insert(Test1 entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(Test1 entity)
		{
			throw new NotImplementedException();
		}
	}
}
