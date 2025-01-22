using FoodApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Web.Controllers
{
    public class PartnerController : Controller
    {
        private readonly IPartnerService _partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;

        }
        public IActionResult Index()
        {
            var  restaurant = _partnerService.GetRestaurants();
            return View(restaurant);
        }
    }
}
