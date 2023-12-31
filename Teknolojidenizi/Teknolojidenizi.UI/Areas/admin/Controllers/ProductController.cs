﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknolojidenizi.BL;
using Teknolojidenizi.DAL.Entities;
using Teknolojidenizi.UI.Areas.admin.ViewModels;

namespace Teknolojidenizi.UI.Areas.admin.Controllers
{
    [Area("admin"),Authorize]
    public class ProductController : Controller
    {
        IRepository<Product> repoProduct;

		IRepository<Brand> repoBrand;
		IRepository<Category> repoCategory;


		public ProductController(IRepository<Product> _repoProduct, IRepository<Brand> _repoBrand, IRepository<Category> _repoCategory)
        {
            repoProduct = _repoProduct;
            repoBrand = _repoBrand;
            repoCategory = _repoCategory;
        }
        public IActionResult Index()
        {
            return View(repoProduct.GetAll().OrderByDescending(x => x.ID));
        }

        public IActionResult New()
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                Brands = repoBrand.GetAll().OrderBy(x => x.Name),
                Categories = repoCategory.GetAll(x => x.ParentID!=null).OrderBy(x => x.Name)
            };
            return View(productVM);
        }

        [HttpPost , ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(ProductVM model) 
        {
            if (ModelState.IsValid)
            {
                
                repoProduct.Add(model.Product);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("New");        
        }

        public IActionResult Edit(int id)
        {

            Product product = repoProduct.GetBy(x => x.ID == id);
            ProductVM productVM = new ProductVM() 
            {
                Product = product,
                Brands = repoBrand.GetAll().OrderBy(x => x.Name),
                Categories = repoCategory.GetAll(x => x.ParentID != null).OrderBy (x => x.Name)
            };
            if (product !=null) return View(productVM);
            else return RedirectToAction("Index");
            
        }
        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(ProductVM model)
        {
            if (ModelState.IsValid)
            {
                
                repoProduct.Update(model.Product);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Edit");

        }
        public IActionResult Delete(int id)
        {
            Product product = repoProduct.GetBy(x=> x.ID == id);
            if (product != null)
            {
                repoProduct.Delete(product);

            }
             return RedirectToAction("Index");
        }
    }
}
