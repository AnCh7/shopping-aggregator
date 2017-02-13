using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAggregator.Api.Db;

namespace ShopAggregator.Api.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly ShopAggregatorUnitOfWork _uow;

        public ProductsController(ShopAggregatorUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public IActionResult GetAllProducts([FromQuery]int page = 0, [FromQuery]int pageSize = 10)
        {
            try
            {
                var query = _uow.ProductRepository.Get().ToList();
                var totalCount = query.Count;
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var results = query.Skip(pageSize * page)
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
        public IActionResult GetProductById(int id)
        {
            try
            {
                var product = _uow.ProductRepository.GetById(id);
                if (product != null)
                {
                    return Ok(product);
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