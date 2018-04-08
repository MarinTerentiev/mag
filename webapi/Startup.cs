using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using dal.Infrastructure;
using dal.Repositories.Abstract;
using dal.Repositories.Concrete;
using dal.Services.Abstract;
using dal.Services.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Owin;
using webapi.Common;
using webapi.Infrastructure.Auth;
using webapi.Infrastructure.Auth.Providers;

[assembly: OwinStartup(typeof(webapi.Startup))]

namespace webapi
{
	public class Startup
	{
		public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
		public static string PublicClientId { get; private set; }

		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);

			var apiConfig = ConfigureWebApi();
			ConfigureDependencyInjection(app, apiConfig);
			app.UseWebApi(apiConfig);
		}

		public void ConfigureAuth(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
			});

			PublicClientId = "self";
			OAuthOptions = new OAuthAuthorizationServerOptions
			{
				TokenEndpointPath = new PathString("/Token"),
				Provider = new ApplicationOAuthProvider(PublicClientId),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(8),
				AllowInsecureHttp = false
			};

			app.UseOAuthBearerTokens(OAuthOptions);
		}

		private HttpConfiguration ConfigureWebApi()
		{
			var config = new HttpConfiguration();
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
			return config;
		}


		private void ConfigureDependencyInjection(IAppBuilder app, HttpConfiguration apiConfig)
		{
			var builder = new ContainerBuilder();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			builder.RegisterApiControllers(executingAssembly);

			RegisterComponents(builder, app);

			var container = builder.Build();

			app.UseAutofacMiddleware(container);

			var apiResolver = new AutofacWebApiDependencyResolver(container);
			apiConfig.DependencyResolver = apiResolver;
			app.UseAutofacWebApi(apiConfig);
		}

		private void RegisterComponents(ContainerBuilder builder, IAppBuilder app)
		{
			var conn = Tool.GetConnectionString();
			// register data infrastructure
			builder.RegisterType<ConnectionFactory>().As<IConnectionFactory>().WithParameter("conn", conn);
			builder.RegisterType<AutofacOfWork>().As<IAutofacOfWork>();


			// register repositories
			builder.RegisterType<TestRepositories>().As<ITestRepositories>();
			builder.RegisterType<UserRepository>().As<IUserRepository>();
			builder.RegisterType<UserSettingsRepository>().As<IUserSettingsRepository>();
			builder.RegisterType<DealerRepository>().As<IDealerRepository>();
			builder.RegisterType<CompanyRepository>().As<ICompanyRepository>();
			builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
			builder.RegisterType<ProductRepository>().As<IProductRepository>();
			builder.RegisterType<ProductCatalogRepository>().As<IProductCatalogRepository>();
			builder.RegisterType<ProductCardRepository>().As<IProductCardRepository>();
			builder.RegisterType<OrderRepository>().As<IOrderRepository>();


			// register services
			builder.RegisterType<TestServices>().As<ITestServices>();
			builder.RegisterType<UserService>().As<IUserService>();
			builder.RegisterType<DealerServices>().As<IDealerServices>();
			builder.RegisterType<CompanyServices>().As<ICompanyServices>();
			builder.RegisterType<ProductServices>().As<IProductServices>();
			builder.RegisterType<ProductCatalogService>().As<IProductCatalogService>();
			builder.RegisterType<OrderService>().As<IOrderService>();


			// register owin
			builder.RegisterType<ApplicationDbContext>().As<IDbContext>().WithParameter("conn", conn);
			builder.RegisterType<ApplicationUserStore>().As<IUserStore<User, int>>().InstancePerRequest();
			builder.Register<IAuthenticationManager>((c, p) => c.Resolve<IOwinContext>().Authentication).InstancePerRequest();
			var dataProtectionProvider = app.GetDataProtectionProvider();
			builder.Register<UserManager<User, int>>((c, p) => BuildUserManager(c, p, dataProtectionProvider));
		}
		
		private ApplicationUserManager BuildUserManager(IComponentContext context, IEnumerable<Parameter> parameters, IDataProtectionProvider dataProtectionProvider)
		{
			var manager = new ApplicationUserManager(context.Resolve<IUserStore<User, int>>());
			// Configure validation logic for usernames
			manager.UserValidator = new UserValidator<User, int>(manager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};
			// Configure validation logic for passwords
			manager.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 4,
				//RequireNonLetterOrDigit = true,
				//RequireDigit = true,
				//RequireLowercase = true,
				//RequireUppercase = true,
			};

			if (dataProtectionProvider != null)
			{
				manager.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
			}
			return manager;
		}
	}
}
