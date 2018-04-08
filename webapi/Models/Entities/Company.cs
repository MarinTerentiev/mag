using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models.Entities
{
	public class Company
	{
		public int Id { get; set; }
		public int DealerId { get; set; }
		public string Name { get; set; }

		public static explicit operator Company(dal.Models.Entities.Company company)
		{
			return new Company
			{
				Id = company.Id,
				Name = company.Name,
				DealerId = company.DealerId
			};
		}
	}
}