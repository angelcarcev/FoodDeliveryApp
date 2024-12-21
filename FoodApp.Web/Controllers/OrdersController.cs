using ClosedXML.Excel;
using FoodApp.Domain.Domain;
using FoodApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: Orders
        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }

        // GET: Orders/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tempBaseEntity = new BaseEntity { Id = id.Value };

            var order = _orderService.GetDetailsForOrder(tempBaseEntity);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpGet]
        public FileContentResult ExportAllOrders()
        {
            string fileName = "Orders.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Orders");
                worksheet.Cell(1, 1).Value = "OrderID";
                worksheet.Cell(1, 2).Value = "Customer UserName";
                worksheet.Cell(1, 3).Value = "Total Price";

                var data = _orderService.GetAllOrders();

                for (int i = 0; i < data.Count(); i++)
                {
                    var item = data[i];
                    worksheet.Cell(i + 2, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 2, 2).Value = item.Owner.UserName;
                    var total = 0;
                    for (int j = 0; j < item.FoodItemsInOrder.Count(); j++)
                    {
                        worksheet.Cell(1, 4 + j).Value = "Product - " + (j + 1);
                        worksheet.Cell(i + 2, 4 + j).Value = item.FoodItemsInOrder.ElementAt(j).FoodItem.FoodItemName;
                        total += (item.FoodItemsInOrder.ElementAt(j).Quantity * item.FoodItemsInOrder.ElementAt(j).FoodItem.Price);
                    }
                    worksheet.Cell(i + 2, 3).Value = total;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }
    }
}
