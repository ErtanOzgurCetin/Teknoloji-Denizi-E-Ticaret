﻿using Teknolojidenizi.DAL.Entities;

namespace Teknolojidenizi.UI.ViewModels
{
	public class IndexVM
	{
		public IEnumerable<Slide> Slides { get; set; }
		public IEnumerable<Product> LatestProducts { get; set; }
		public IEnumerable<Product> SalesProducts { get; set; }


	}
}
