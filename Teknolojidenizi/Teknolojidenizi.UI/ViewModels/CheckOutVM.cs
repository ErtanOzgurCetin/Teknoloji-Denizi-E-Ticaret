using Teknolojidenizi.DAL.Entities;
using Teknolojidenizi.UI.Models;

namespace Teknolojidenizi.UI.ViewModels
{
	public class CheckOutVM
	{
		public Order Order { get; set; }
		public IEnumerable<Cart> Carts { get; set; }
	}
}
