using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using dal.Infrastructure;
using dal.Repositories.Abstract;
using dal.Repositories.Concrete;
using webapi.Common;

namespace webapi
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{

		}
	}
}
