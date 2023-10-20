using Teknolojidenizi.DAL.Entities;

namespace Teknolojidenizi.UI.Areas.admin.ViewModels

{
	public class CategoryVM
	{
		public Category Category { get; set; } // Ekle-sil-güncelle işlemleri için
		public IEnumerable<Category> Categories { get; set; } // combobox içerisine doldurulmak için
	}
}
