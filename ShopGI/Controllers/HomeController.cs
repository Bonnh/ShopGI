using Microsoft.AspNetCore.Mvc;
using ShopGI.Models;
using System.Diagnostics;

namespace ShopGI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductRep iproductRep;

        public HomeController(ILogger<HomeController> logger, IProductRep iproductRep)
        {
            this._logger = logger;
            this.iproductRep = iproductRep;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}