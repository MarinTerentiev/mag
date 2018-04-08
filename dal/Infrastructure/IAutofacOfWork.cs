using System;
using dal.Repositories.Abstract;

namespace dal.Infrastructure
{
	public interface IAutofacOfWork : IDisposable
	{
		ITestRepositories TestRepositories { get; }
		IUserRepository UserRepository { get; }
		IUserSettingsRepository UserSettingsRepository { get; }
		IDealerRepository DealerRepository { get; }
		ICompanyRepository CompanyRepository { get; }
		ICategoryRepository CategoryRepository { get; }
		IProductRepository ProductRepository { get; }
		IProductCatalogRepository ProductCatalogRepository { get; }
		IProductCardRepository ProductCardRepository { get; }
		IOrderRepository OrderRepository { get; }
	}
}
