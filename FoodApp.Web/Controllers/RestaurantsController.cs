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

namespace FoodApp.Web.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IFoodItemService _foodItemService;



        public RestaurantsController(IRestaurantService restaurantService, IFoodItemService foodItemService)
        {
            _restaurantService = restaurantService;
            _foodItemService = foodItemService;
        }

        public IActionResult Index()
        {
            return View(_restaurantService.GetAllRestaurants());
        }


        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = _restaurantService.GetDetailsForRestaurant(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,RestaurantName,RestaurantDescription,RestaurantImage,RestaurantAddress")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                restaurant.Id = Guid.NewGuid();
                _restaurantService.CreateNewRestaurant(restaurant);
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }




        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = _restaurantService.GetDetailsForRestaurant(id);
            if (foodItem == null)
            {
                return NotFound();
            }
            return View(foodItem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,RestaurantName,RestaurantDescription,RestaurantImage,RestaurantAddress")] Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _restaurantService.UpdateExistingProduct(restaurant);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = _restaurantService.GetDetailsForRestaurant(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _restaurantService.DeleteRestaurant(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
