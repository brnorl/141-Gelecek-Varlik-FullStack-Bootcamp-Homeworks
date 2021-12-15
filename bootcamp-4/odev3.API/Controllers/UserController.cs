using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using odev3.Models;
using odev3.Service.User;
using odev3.Models.User;
using Microsoft.Extensions.Caching.Memory;
using odev3.API.Cache;
using odev3.API.Attribute;

namespace odev3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IUserCache userCache;

        public UserController(IUserService _userService, IMapper _mapper, IUserCache _userCache)
        {
            userService = _userService;
            mapper = _mapper;
            userCache = _userCache;
        }

        [HttpGet]
        [ServiceFilter(typeof(LoginFilter))]
        public IActionResult Get()
        {
            return Ok(userService.Get());
        }

        [HttpPost]
        [Route("register")]
        public bool CreateUser([FromBody] CreateUserModel newUser)
        {
            bool result;
            var data = mapper.Map<odev3.DB.Models.User>(newUser);
            result = userService.Insert(data);
            return result;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser([FromBody] LoginUserModel user)
        {
            if (userService.Login(user))//login işlemi başarılı ise 
            {//doğrulanan veri cachelenmek üzere ilgili fonksiyona gönderilir
                userCache.Cache(user);
                return Ok("Login success.");
            }
            //login işlemi başarısız ise zaten yetki dönmez.
            return BadRequest("Invalid email or password.");

        }

        [HttpPut]
        [Route("update")]
        [ServiceFilter(typeof(LoginFilter))]
        public IActionResult UpdateUser([FromBody] UpdateUserModel updatedUser, int id)
        {
            return Ok(userService.Update(updatedUser, id));
        }

        [HttpDelete]
        [Route("delete")]
        [ServiceFilter(typeof(LoginFilter))]
        public IActionResult DeleteUser(int id)
        {
            return Ok(userService.Delete(id));
        }


    }
}