using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknolojidenizi.DAL.Entities
{
	public enum EOrderStatus
	{
		[Display(Name = "Hazırlanıyor")]

		Hazirlaniyor,
		[Display(Name = "Kargoya Verildi")]

		KargoyaVerildi,
		[Display(Name = "Teslim Edildi")]

		TeslimEdildi,

		[Display(Name = "İptal Edildi")]

		IptalEdildi
	}
}
