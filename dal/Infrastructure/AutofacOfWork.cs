using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Repositories.Abstract;

namespace dal.Infrastructure
{
	public class AutofacOfWork : IAutofacOfWork
	{
		public ITestRepositories TestRepositories { get; }
		public IUserRepository UserRepository { get; }
		public IUserSettingsRepository UserSettingsRepository { get; }
		public IDealerRepository DealerRepository { get; }
		public ICompanyRepository CompanyRepository { get; }
		public ICategoryRepository CategoryRepository { get; }
		public IProductRepository ProductRepository { get; }
		public IProductCatalogRepository ProductCatalogRepository { get; }
		public IProductCardRepository ProductCardRepository { get; }
		public IOrderRepository OrderRepository { get; }
		

		public AutofacOfWork(
			ITestRepositories testRepositories,
			IUserRepository userRepository,
			IUserSettingsRepository userSettingsRepository,
			IDealerRepository dealerRepository,
			ICompanyRepository companyRepository,
			ICategoryRepository categoryRepository,
			IProductRepository productRepository,
			IProductCatalogRepository productCatalogRepository,
			IProductCardRepository productCardRepository,
			IOrderRepository orderRepository
			)
		{
			TestRepositories = testRepositories;
			UserRepository = userRepository;
			UserSettingsRepository = userSettingsRepository;
			DealerRepository = dealerRepository;
			CompanyRepository = companyRepository;
			CategoryRepository = categoryRepository;
			ProductRepository = productRepository;
			ProductCatalogRepository = productCatalogRepository;
			ProductCardRepository = productCardRepository;
			OrderRepository = orderRepository;
		}


		#region IDisposable 
		private bool _disposedValue = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				_disposedValue = true;
			}
		}

		void IDisposable.Dispose()
		{
			Dispose(true);
		}
		#endregion
	}
}
