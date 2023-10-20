using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknolojidenizi.BL;
using Teknolojidenizi.DAL.Entities;

namespace Teknolojidenizi.UI.Areas.admin.Controllers
{
    [Area("admin"),Authorize]
    public class SlideController : Controller
    {
        IRepository<Slide> repoSlide;
            public SlideController(IRepository<Slide> _repoSlide)
        {
            repoSlide = _repoSlide;
        }
        public IActionResult Index()
        {
            return View(repoSlide.GetAll().OrderByDescending(x => x.ID));
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost , ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(Slide model) 
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Any())
                {
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","img","slide")))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide"));
                    }
                    string dosyaAdi = Request.Form.Files["Picture"].FileName;
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","img","slide",dosyaAdi),FileMode.Create))
                    {
                        await Request.Form.Files["Picture"].CopyToAsync(stream);
                    }
                    model.Picture = "/img/slide/" + dosyaAdi;
                }
                repoSlide.Add(model);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("New");        
        }

        public IActionResult Edit(int id)
        {
            Slide slide = repoSlide.GetBy(x => x.ID == id);
            if (slide !=null) return View(slide);
            else return RedirectToAction("Index");
            
        }
        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Slide model)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Any())
                {
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide")))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide"));
                    }
                    string dosyaAdi = Request.Form.Files["Picture"].FileName;
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide", dosyaAdi), FileMode.Create))
                    {
                       await Request.Form.Files["Picture"].CopyToAsync(stream);
                    }
                    model.Picture = "/img/slide/" + dosyaAdi;
                }
                repoSlide.Update(model);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Edit");

        }
        public IActionResult Delete(int id)
        {
            Slide slide = repoSlide.GetBy(x=> x.ID == id);
            if (slide != null)
            {
                repoSlide.Delete(slide);

            }
             return RedirectToAction("Index");
        }
    }
}
