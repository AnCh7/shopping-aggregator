using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAggregator.Web.Common;
using ShopAggregator.Web.Models;
using ShopAggregator.Web.Services;

namespace ShopAggregator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopAggregatorService _service;

        public HomeController(ShopAggregatorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var result = await _service.GetAllShops(new PaginationRequest { Page = pageNumber, PageSize = 20 });
            if (result.Success)
            {
                return View(new PaginatedList<Shop>(result.Result.Results, pageNumber, result.Result.TotalCount));
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Stock(int id)
        {
            var result = await _service.GetShopProducts(id);
            if (result.Success)
            {
                return View(result.Result);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
