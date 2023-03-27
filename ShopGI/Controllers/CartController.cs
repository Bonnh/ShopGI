using Microsoft.AspNetCore.Mvc;
using ShopGI.Models;
using ShopGI.Models.ViewModels;


namespace ShopGI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
