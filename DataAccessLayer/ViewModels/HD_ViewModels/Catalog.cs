using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ViewModels.HD_ViewModels
{
	public class Catalog
	{
		public double Price { get; set; }
		public string MaterialCode { get; set; } = string.Empty;
		public string ProductDescription { get; set; } = string.Empty;
		public string Size { get; set; } = string.Empty ;
		public string Brand { get; set; } = string.Empty ;
	}
}
