using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teknolojidenizi.BL;
using Teknolojidenizi.DAL.Entities;

namespace Teknolojidenizi.UI.ViewComponents
{
	public class FooterViewComponent : ViewComponent
	{
		IRepository<Category> repoCategory;

		public FooterViewComponent(IRepository<Category> _repoCategory)
		{
			repoCategory = _repoCategory;
		}

		public IViewComponentResult Invoke()
		{
			return View(repoCategory.GetAll().Include(x => x.SubCategories).OrderBy(x => x.DisplayIndex));

		}
	}
}
