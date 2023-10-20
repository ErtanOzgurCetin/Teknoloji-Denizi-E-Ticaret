using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknolojidenizi.DAL.Entities
{
	public class ProductPicture
	{
        public int ID { get; set; }

        [Column(TypeName = "Varchar(50)") , StringLength(50), Display(Name ="Resim Adı"),Required(ErrorMessage ="Ürün Resim Adı Boş Bırakılamaz")]
        public string Name { get; set; }

		[Column(TypeName = "Varchar(150)"), StringLength(150), Display(Name = "Resim Dosyası")]

		public string Picture { get; set; }

		[Display(Name = "Görüntülenme Sırası")]
        public int DisplayIndex { get; set; }

		[Display(Name = "Ürün Adı")]

		public int ProductID { get; set; }

		public Product Product { get; set; }

    }
}
