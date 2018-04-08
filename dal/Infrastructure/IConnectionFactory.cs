using System.Data;

namespace dal.Infrastructure
{
	public interface IConnectionFactory
	{
		IDbConnection GetConnection { get; }
	}
}