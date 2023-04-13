using Microsoft.AspNetCore.Mvc;
using ShopGI.Models;
using ShopGI.Models.ViewModels;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using Microsoft.AspNetCore.Identity;

namespace ShopGI.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContext _appDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public IProductRep productRep { get; set; }
        private IWebHostEnvironment environment { get; set; }

        public ProductController(AppDbContext appDbContext, IProductRep productRep, IWebHostEnvironment environment, UserManager<IdentityUser> userManager)
        {
            this._appDbContext = appDbContext;
            this.productRep = productRep;
            this.environment = environment;
            this._userManager = userManager;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            return View(productRep.GetProduct(id));
        }
        public async Task <IActionResult> SendEmail(int id)
        {
            //gửi tài khoản về email
            Product product = productRep.GetProduct(id);
            string account;
            account = product.accountname.ToString().Trim();
            string password;
            password = product.password.ToString().Trim();

            //lấy email để gửi
            var emailUser = "";
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                emailUser = await _userManager.GetEmailAsync(user);
            }

            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("ShopGI", "lethanhbinh12t5nh2020@gmail.com"));
            message.To.Add(new MailboxAddress("User", emailUser));
            message.Subject = "Tài khoản của bạn";

            BodyBuilder body= new BodyBuilder();
            body.TextBody = "Tài Khoản: " + account + "; Mật Khẩu: " + password;
            message.Body = body.ToMessageBody();
            

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("lethanhbinh12t5nh2020@gmail.com", "pfurakpwollyuzgw");
                client.Send(message);

                client.Disconnect(true);
            }
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
            product.accountname = v.accountname;
            product.password = v.password;


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
            UpdateViewModel updateViewModel = new UpdateViewModel();
            Product product = productRep.GetProduct(id);

            updateViewModel.AR = product.AR;
            updateViewModel.Server = product.Server;
            updateViewModel.Champ = product.Champ;
            updateViewModel.Desccription = product.Desccription;
            updateViewModel.Category = product.Category;
            updateViewModel.Price = product.Price;
            updateViewModel.Photo = null;
            updateViewModel.ID = id;
            updateViewModel.PhotoPath = product.PhotoPath;
            updateViewModel.accountname = product.accountname;
            updateViewModel.password = product.password;
            
            
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
            product.accountname = updateViewModel.accountname;
            product.password = updateViewModel.password;
            

            updateViewModel.Photo.CopyTo(new FileStream(filepath, FileMode.Create));

            productRep.Update(product);

            return View("List",new ProductLVModel { Product = productRep.GetAllProduct() , CurrentCategory = null});
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
            Product product = productRep.GetProduct(id);
            productRep.Delete(id);

            return RedirectToAction("List", productRep.GetAllProduct());
        }

    }
}
