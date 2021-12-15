using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using odev3.API.Attribute;
using odev3.DB.Models;
using odev3.Models.Product;
using odev3.Service.Product;
using System.Linq;
using System.Collections.Generic;
using odev3.Models.Pagination;
using odev3.Service.User;

namespace odev3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PageController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IUserService userService;

        public PageController(IProductService _productService, IUserService _userService)
        {
            productService = _productService;
            userService = _userService;
        }

        [HttpGet]
        [Route("Product")]
        public IActionResult GetProductPage([FromQuery] PaginationFilter filter)
        {
            var list = productService.Get().productList;
            return Ok(productService.GetPaged(filter.PageNumber, filter.PageSize, list));
        }
        [HttpGet]
        [Route("User")]
        public IActionResult GetUserPage([FromQuery] PaginationFilter filter)
        {
            return Ok(userService.GetPaged(filter.PageNumber, filter.PageSize));
        }



    }
}