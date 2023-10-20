using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teknolojidenizi.BL;
using Teknolojidenizi.DAL.Entities;
using Teknolojidenizi.UI.ViewModels;

namespace Teknolojidenizi.UI.Controllers
{
	public class HomeController : Controller
	{
		IRepository<Slide> repoSlide;
		IRepository<Product> repoProduct;

		public HomeController(IRepository<Slide> _repoSlide , IRepository<Product> _repoProduct)
        {
            repoSlide = _repoSlide;
			repoProduct = _repoProduct;

		}

        public IActionResult Index()
		{
			IndexVM indexVM = new IndexVM()
			{ 
				Slides = repoSlide.GetAll().OrderBy(x => x.DisplayIndex),
				LatestProducts = repoProduct.GetAll().Include(x=> x.ProductPictures).OrderBy(x => x.ID).Take(8),
				SalesProducts  = repoProduct.GetAll().Include(x => x.ProductPictures).OrderBy(x => Guid.NewGuid()).Take(8)

			};
			return View(indexVM);
		}
	}
}
