using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknolojidenizi.DAL.Entities
{
	public class Category
	{
        public int ID { get; set; }

        [Column(TypeName = "Varchar(50)") , StringLength(50), Display(Name ="Kategori Adı")]
        public string Name { get; set; }

		[Display(Name = "Görüntülenme Sırası")]
        public int DisplayIndex { get; set; }
        [Display(Name="Alt Kategori")]
        public int? ParentID { get; set; }

        public Category ParentCategory { get; set; }

        public ICollection<Category> SubCategories { get; set;}
        public ICollection<Product> Products { get; set;}

    }
}
