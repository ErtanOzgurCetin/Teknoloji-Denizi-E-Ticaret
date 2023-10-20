using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknolojidenizi.BL;
using Teknolojidenizi.DAL.Entities;

namespace Teknolojidenizi.UI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class ProductPictureController : Controller
    {
        IRepository<ProductPicture> repoProductPicture;
        public ProductPictureController(IRepository<ProductPicture> _repoProductPicture)
        {
            repoProductPicture = _repoProductPicture;
        }
        public IActionResult Index(int productid)
        {
            ViewBag.ProductID = productid;
            return View(repoProductPicture.GetAll(x => x.ProductID == productid).OrderByDescending(x => x.ID));
        }

        public IActionResult New(int productid)
        {
            ViewBag.ProductID = productid;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(ProductPicture model)
        {
            if (ModelState.IsValid) // gelen model doğrulanmışsa
            {
                if (Request.Form.Files.Any())
                {
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "ProductPicture")))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "ProductPicture"));
                    }
                    string dosyaAdi = Request.Form.Files["Picture"].FileName;
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "ProductPicture", dosyaAdi), FileMode.Create))
                    {
                        await Request.Form.Files["Picture"].CopyToAsync(stream);
                    }
                    model.Picture = "/img/ProductPicture/" + dosyaAdi;
                }
                repoProductPicture.Add(model);
                return RedirectToAction("Index", new { productid = model.ProductID });
            }
            else return RedirectToAction("New");
        }

        public IActionResult Edit(int id)
        {
            ProductPicture ProductPicture = repoProductPicture.GetBy(x => x.ID == id);
            if (ProductPicture != null) return View(ProductPicture);
            else return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        #region BİLGİLENDİRME
        // Edit sayfasında, Price propertysi içerisinde para birimlerinin küsüratlarını ayırırken virgül kullandığımızda jquery.validate.min.js dosyasındaki bir koddan ötürü hata alıyoruz. Bunun için o dosyada
        // (?:,\d{3})+)?(?:\.\d+)?$/.test(a) kodu aratılacak
        // buradaki kodlar (?:.\d{3})+)?(?:\,\d+)?$/.test(a)
        // olarak değiştirilecek
        #endregion
        public async Task<IActionResult> Edit(ProductPicture model)
        {
            if (ModelState.IsValid) // gelen model doğrulanmışsa
            {
                if (Request.Form.Files.Any())
                {
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "ProductPicture")))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "ProductPicture"));
                    }
                    string dosyaAdi = Request.Form.Files["Picture"].FileName;
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "ProductPicture", dosyaAdi), FileMode.Create))
                    {
                        await Request.Form.Files["Picture"].CopyToAsync(stream);
                    }
                    model.Picture = "/img/ProductPicture/" + dosyaAdi;
                }
                repoProductPicture.Update(model);
                return RedirectToAction("Index", new { productid = model.ProductID });
            }
            else return RedirectToAction("Edit");
        }


        public IActionResult Delete(int id)
        {
            ProductPicture ProductPicture = repoProductPicture.GetBy(x => x.ID == id);
            if (ProductPicture != null) repoProductPicture.Delete(ProductPicture);
            return RedirectToAction("Index", new { productid = ProductPicture.ID });
        }
    }
}

