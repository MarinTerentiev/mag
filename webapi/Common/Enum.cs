using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Common
{
	public class Enum
	{
		public enum Status
		{
			Active,
			Deleted,
			Disabled
		}

		public enum Role
		{
			Admin,
			Dealer,
			Customer
		}
	}
}