using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Teknolojidenizi.BL;
using Teknolojidenizi.DAL.Entities;
using Teknolojidenizi.UI.Models;
using Teknolojidenizi.UI.ViewModels;

namespace Teknolojidenizi.UI.Controllers
{
	public class CartController : Controller
	{
		IRepository<Product> repoProduct;
		IRepository<Order> repoOrder;
		IRepository<OrderDetail> repoOrderDetail;
		public CartController(IRepository<Product> _repoProduct, IRepository<Order> _repoOrder, IRepository<OrderDetail> _repoOrderDetail)
		{
			repoProduct = _repoProduct;
			repoOrder = _repoOrder;
			repoOrderDetail = _repoOrderDetail;
		}
		[Route("/sepet")]
		public IActionResult Index()
		{
			if (Request.Cookies["MyCart"] != null)
			{
				List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
				return View(carts);
			}
			else return Redirect("/");
		}

		[Route("/sepet/sepeteekle"), HttpPost]
		public string AddCart(int productid, int quantity)
		{
			Product product = repoProduct.GetAll(x => x.ID == productid).Include(x => x.ProductPictures).FirstOrDefault();
			bool varMi = false;
			if (product != null)
			{
				Cart cart = new Cart()
				{
					ProductId = productid,
					ProductName = product.Name,
					ProductPicture = product.ProductPictures.FirstOrDefault().Picture,
					ProductPrice = product.Price,
					Quantity = quantity
				};
				List<Cart> carts = new List<Cart>();
				if (Request.Cookies["MyCart"] != null) // daha önce sepete ürün eklenmiş ise
				{
					carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
					foreach (Cart c in carts)
					{
						if (c.ProductId == productid)
						{
							varMi = true;
							c.Quantity += quantity;
							break;
						}
					}
				}
				if (!varMi)
					carts.Add(cart);
				CookieOptions options = new();
				options.Expires = DateTime.Now.AddHours(3);
				Response.Cookies.Append("MyCart", JsonConvert.SerializeObject(carts), options);
				return product.Name;

			}
			else return "~ Ürün Bulunamadı";
		}

		[Route("/sepet/sepetsayisi")]
		public int CartCount()
		{
			int geri = 0;
			if (Request.Cookies["MyCart"] != null)
			{
				List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
				geri = carts.Sum(x => x.Quantity);

				// sipariş bazlı sepet sayısını tutmak için
				// geri = carts.Count();
			}
			return geri;
		}

		[Route("/sepet/alisverisitamamla")]
		public IActionResult CheckOut()
		{
			ViewBag.ShippingFee = 1000;
			if (Request.Cookies["MyCart"] != null)
			{
				List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
				CheckOutVM checkOutVM = new CheckOutVM
				{
					Order = new Order(),
					Carts = carts
				};

				return View(checkOutVM);
			}
			else
				return Redirect("/");
		}


		[Route("/sepet/alisverisitamamla"), HttpPost, ValidateAntiForgeryToken]
		public IActionResult CheckOut(CheckOutVM model)
		{

			model.Order.RecDate = DateTime.Now;
			string orderNumber = DateTime.Now.Microsecond.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Microsecond.ToString() + DateTime.Now.Microsecond.ToString();
			if (orderNumber.Length > 20) orderNumber = orderNumber.Substring(0, 20);
			model.Order.OrderNumber = orderNumber;

			model.Order.OrderStatus = EOrderStatus.Hazirlaniyor;
			repoOrder.Add(model.Order);
			List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
			foreach (Cart c in carts)
			{
				OrderDetail od = new OrderDetail
				{
					OrderID = model.Order.ID,
					ProductID = c.ProductId,
					ProductName = c.ProductName,
					ProductPicture = c.ProductPicture,
					ProductPrice = c.ProductPrice,
					Quantity = c.Quantity
				};
				repoOrderDetail.Add(od);
			}

			// Müşteriye Mail Gönder
			// Kişiye Mail Gönder

			return Redirect("/");
		}

		//[Route("/sepet/updatequantity"), HttpPost]
		//public JsonResult UpdateQuantity(int productId, int quantity)
		//{
		//	if (Request.Cookies["MyCart"] != null)
		//	{
		//		List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
		//		foreach (Cart cart in carts)
		//		{
		//			if (cart.ProductId == productId)
		//			{
		//				cart.Quantity = quantity;
		//				// Sepet verilerini güncelle ve yeni verileri cookie'ye kaydet
		//				CookieOptions options = new();
		//				options.Expires = DateTime.Now.AddHours(3);
		//				Response.Cookies.Append("MyCart", JsonConvert.SerializeObject(carts), options);
		//				// Sepet toplamını hesapla ve gönder
		//				decimal totalPrice = carts.Sum(c => c.ProductPrice * c.Quantity);
		//				return Json(new { totalPrice });
		//			}
		//		}
		//	}
		//	// Eğer hata olursa veya ürün bulunamazsa, hata durumunu gönder
		//	return Json(new { error = "Ürün bulunamadı veya hata oluştu" });
		//}
	}
}

