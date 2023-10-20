using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknolojidenizi.DAL.Entities
{
	public	enum EPaymentOption
	{
		[Display(Name = "Kredi Kartı")]
		KrediKarti=1,
		[Display(Name = "Havale")]
		Havale,
		[Display(Name = "Kapıda Nakit / Kredi Kartı Ödeme ")]
		KapidaOdeme
	}
}
