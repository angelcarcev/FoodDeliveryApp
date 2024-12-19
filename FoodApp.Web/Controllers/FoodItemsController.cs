
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodApp.Domain.Domain;
using FoodApp.Repository;
using FoodApp.Domain.DTO;
using FoodApp.Service.Interface;
using System.Security.Claims;

namespace FoodApp.Web.Controllers
{
    public class FoodItemsController : Controller
    {
        private readonly IFoodItemService _foodItemService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IRestaurantService _restaurantService;
        private readonly ApplicationDbContext _context;



        public FoodItemsController(IFoodItemService foodItemService, IShoppingCartService shoppingCartService, IRestaurantService restaurantService, ApplicationDbContext context)
        {
            _foodItemService = foodItemService;
            _shoppingCartService = shoppingCartService;
            _restaurantService = restaurantService;
            _context = context;
        }


        public IActionResult Index()
        {
            return View(_foodItemService.GetAllFoodItems());
        }


        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = _foodItemService.GetDetailsForFoodItem(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            return View(foodItem);
        }

        public IActionResult Create()
        {
            ViewBag.Restaurants = _context.Restaurants
               .Select(r => new SelectListItem
               {
                   Value = r.Id.ToString(),
                   Text = r.RestaurantName
               })
               .ToList();

            return View();


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FoodItemName,FoodItemDescription,FoodItemImage,Price,RestaurantId")] FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("RestaurantId: " + foodItem.RestaurantId);

                _context.FoodItems.Add(foodItem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }



            return View(foodItem);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = _foodItemService.GetDetailsForFoodItem(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            ViewBag.Restaurants = _context.Restaurants
              .Select(r => new SelectListItem
              {
                  Value = r.Id.ToString(),
                  Text = r.RestaurantName
              })
              .ToList();


            return View(foodItem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,FoodItemName,FoodItemDescription,FoodItemImage,Price,RestaurantId")] FoodItem foodItem)
        {
            if (id != foodItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _foodItemService.UpdateExistingFoodItem(foodItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(foodItem);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = _foodItemService.GetDetailsForFoodItem(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            return View(foodItem);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _foodItemService.DeleteFoodItem(id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult AddProductToCart(Guid Id)
        {
            var result = _shoppingCartService.getProductInfo(Id);
            if (result != null)
            {
                return View(result);
            }
            return View();
        }


        [HttpPost]
        public IActionResult AddToCartConfirmed(AddToCartDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _shoppingCartService.AddToShoppingConfirmed(userId, model);

            if (result != null)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
            else { return View(model); }

        }

    }
}
