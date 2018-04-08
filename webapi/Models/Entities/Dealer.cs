using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models.Entities
{
	public class Dealer
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public static explicit operator Dealer(dal.Models.Entities.Dealer dealer)
		{
			return new Dealer
			{
				Id = dealer.Id,
				Name = dealer.Name
			};
		}
	}
}