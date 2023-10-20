using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace Teknolojidenizi.DAL.Entities
{
	public class Admin
	{
		public int ID { get; set; }

		[Column(TypeName ="Varchar(20)"), StringLength(20), Display(Name = "Kullancı Adı"), Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez")]

		public string UserName { get; set; }

		[Column(TypeName = "Varchar(33)"), StringLength(33), Display(Name = "Parola"), Required(ErrorMessage = "Parola Boş Geçilemez")]

		public string Password { get; set; }

		[Column(TypeName = "Varchar(50)"), StringLength(50), Display(Name = "Admin Adı"), Required(ErrorMessage = "Ad Soyad Boş Geçilemez")]

		public string NameSurname { get; set; }

		[Display(Name ="Son Giriş Tarihi")]
        public DateTime LastLoginDate { get; set; }

		[Display(Name = "Son Giriş IP")]

		public string LastLoginIP { get; set; }

    }

}
