using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodApp.Domain.Domain;
using FoodApp.Repository;
using FoodApp.Service.Interface;
using System.Security.Claims;
using FoodApp.Domain;
using Stripe;

namespace FoodApp.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IEmailService _emailService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService, IEmailService emailService)
        {
            _shoppingCartService = shoppingCartService;
            _emailService = emailService;
        }

        // GET: ShoppingCarts
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var dto = _shoppingCartService.getShoppingCartInfo(userId);

            return View(dto);
        }
   

        // GET: ShoppingCarts/Delete/5
        public async Task<IActionResult> DeleteProductFromShoppingCart(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _shoppingCartService.deleteFoodFromShoppingCart(userId, id);
            return RedirectToAction("Index");
        }

        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _shoppingCartService.order(userId);
            return RedirectToAction("Index", "ShoppingCarts");

        }
        public IActionResult SuccessPayment()
        {
            return View();
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            StripeConfiguration.ApiKey = "sk_test_51QYWTFGUik777iy6r6403CwuGckZ07IcHPzQ0cUOWmh3GG4Md6Aa0URvN0I6aw19MTceRfN2nsbngqLuPDX01iNi004Evv8bKs";
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._shoppingCartService.getShoppingCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "FoodDelivery Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                var emailMessage = new EmailMessage
                {
                    MailTo = stripeEmail, // на корисникот кој плати
                    Subject = "Order Confirmation",
                    Content = $"Thank you for your purchase. Your order with total {order.TotalPrice} USD was successful."
                };

                _emailService.SendEmailAsync(emailMessage);
                this.Order();
                return RedirectToAction("SuccessPayment");

            }
            else
            {
                return RedirectToAction("NotsuccessPayment");
            }
        }

     }
}
