using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAggregator.Api.Db;
using ShopAggregator.Api.Models;

namespace ShopAggregator.Api.Controllers
{
    [Route("api/shops")]
    public class ShopsController : Controller
    {
        private readonly ShopAggregatorUnitOfWork _uow;

        public ShopsController(ShopAggregatorUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public IActionResult GetAllShops([FromQuery]int page = 0, [FromQuery]int pageSize = 10)
        {
            try
            {
                var shops = _uow.ShopRepository.Get().ToList();
                var totalCount = shops.Count;
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var results = shops.Skip(pageSize * page)
                    .Take(pageSize)
                    .ToList();

                var result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    Results = results
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetShopById(int id)
        {
            try
            {
                var shop = _uow.ShopRepository.GetById(id);
                if (shop != null)
                {
                    return Ok(shop);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}/products")]
        public IActionResult GetShopProducts(int id)
        {
            try
            {
                var shop = _uow.ShopRepository.GetById(id);
                if (shop != null)
                {
                    var products = (from p in _uow.ProductRepository.Get()
                                    from productId in _uow.StockRepository.Get(sp => sp.ShopId == id).Select(x => x.ProductId)
                                    where productId == p.Id
                                    select p);

                    return Ok(new Stock(shop, products.ToList()));
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}