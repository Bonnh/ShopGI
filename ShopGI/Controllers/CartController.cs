using Microsoft.AspNetCore.Mvc;
using ShopGI.Models;
using ShopGI.Models.ViewModels;
using ShopGI.SessionExtensions;


namespace ShopGI.Controllers
{
    public class CartController : Controller
    {
        private IProductRep iproductRep;
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
        public CartController(IProductRep productRep)
        {
            iproductRep = productRep;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            { 
                Cart = GetCart(), 
                ReturnUrl = returnUrl
            });
        }
        // thêm vào giỏ hàng
        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            Product product = iproductRep.GetAllProduct().FirstOrDefault(p => p.ID == id);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        // cút khỏi giỏ hàng
        public RedirectToActionResult RemoveFromCart(int id, string returnUrl)
        {
            Product product = iproductRep.GetAllProduct().FirstOrDefault(p => p.ID == id);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveItem(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        
        
        public RedirectToActionResult Clear(string returnUrl)
        {
            Cart cart = GetCart();
            cart.Clear();
            SaveCart(cart);
            return RedirectToAction("Index", new { returnUrl });
        }
        

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
