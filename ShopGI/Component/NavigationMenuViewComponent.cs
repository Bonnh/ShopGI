using Microsoft.AspNetCore.Mvc;
using ShopGI.Models;

namespace ShopGI.Component
{
    [ViewComponent(Name = "NavigationMenuViewComponent")]
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRep _iproductRep;

        public NavigationMenuViewComponent(IProductRep repository)
        {
            _iproductRep = repository;
        }

        public IViewComponentResult Invoke(string Category)
        {
            // Lấy các Category
            return View("~/Views/Shared/Component/NavigationMenuViewComponent/Default.cshtml",
                _iproductRep.GetAllProduct()
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x)
            );

        }


    }
}
