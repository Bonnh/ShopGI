using Microsoft.AspNetCore.Mvc;
using ShopGI.Models;
using ShopGI.Models.ViewModels;
using System;

namespace ShopGI.Controllers
{
    public class ProductController : Controller
    {
        public IProductRep productRep { get; set; }
        private IWebHostEnvironment environment { get; set; }
        
        public ProductController(IProductRep productRep,IWebHostEnvironment webHostEnvironment)
        {
            this.productRep = productRep;
            this.environment = webHostEnvironment;
        }
        

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddViewModel v)
        {
            string filename = null;
            string path = Path.Combine(environment.WebRootPath, "img");
            filename = v.PhotoPath.FileName;
            string filepath = Path.Combine(path, filename);

            Product product = new Product();
            product.ID = v.ID;
            product.AR = v.AR;
            product.Server = v.Server;
            product.Champ = v.Champ;
            product.Desccription = v.Desccription;
            product.Category = v.Category;
            product.Price = v.Price;
            product.PhotoPath = filename;


            productRep.Add(product);

            v.PhotoPath.CopyTo(new FileStream(filepath, FileMode.Create));

            ProductLVModel productListViewModel = new ProductLVModel()
            {
                Product = productRep.GetAllProduct(),
                CurrentCategory = null
            };

            return View("List", productListViewModel);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Product product = productRep.GetProduct(id);
            UpdateViewModel updateViewModel = new UpdateViewModel()
            {
                AR = product.AR,
                Server = product.Server,
                Champ = product.Champ,
                Desccription = product.Desccription,
                Category = product.Category,
                Price = product.Price,
                PhotoPath = product.PhotoPath,
                ID = id
            };
            
            return View(updateViewModel);
        }
        [HttpPost]
        public IActionResult Update(UpdateViewModel updateViewModel) 
        {
            string filename = null;
            string path = Path.Combine(environment.WebRootPath, "img");
            filename = updateViewModel.Photo.FileName;
            string filepath = Path.Combine(path, filename);

            Product product = new Product();
            product.PhotoPath = filename;
            product.ID = updateViewModel.ID;
            product.AR = updateViewModel.AR;
            product.Server = updateViewModel.Server;
            product.Champ = updateViewModel.Champ;
            product.Desccription = updateViewModel.Desccription;
            product.Category = updateViewModel.Category;
            product.Price = updateViewModel.Price;
            

            updateViewModel.Photo.CopyTo(new FileStream(filepath, FileMode.Create));

            productRep.Update(product);

            return View("List", updateViewModel);
        }
        public IActionResult List(string category)
        {
            ProductLVModel model = new ProductLVModel()
            {
                Product = productRep.GetAllProduct().Where(p => category == null || p.Category == category).ToList(),
                CurrentCategory = category
            };

            return View(model);
        }
        public IActionResult Delete(int id) 
        {
            return RedirectToAction("List", productRep.GetAllProduct());
        }

    }
}
