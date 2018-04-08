using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models.Entities
{
	public class Product
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public string PhotoPath { get; set; }

		public static explicit operator Product(dal.Models.Entities.Product product)
		{
			return new Product
			{
				Id = product.Id,
				Name = product.Name,
				CategoryId = product.CategoryId
			};
		}
	}
}