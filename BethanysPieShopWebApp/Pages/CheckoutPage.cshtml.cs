using BethanysPieShopWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;

namespace BethanysPieShopWebApp.Pages
{
    public class CheckoutPageModel : PageModel
    {
        [BindProperty]
        public Order Order { get; set; }
        public IOrderRepository _orderRepository { get; set; }
        public IShoppingCart _shoppingCart { get; set; }

        public CheckoutPageModel(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        public void OnGet()
        {
        }
        
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some pies first.");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.Createorder(Order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return Page();
        }
    }
}
