using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using odev3.Models;
using odev3.Service.User;
using odev3.Models.User;

namespace odev3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public UserController(IUserService _userService, IMapper _mapper)
        {
            userService = _userService;
            mapper = _mapper;
        }
        [HttpGet]
        public UserListModel<UserViewModel> Get()
        {
            return userService.Get();
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
        public bool LoginUser([FromBody] LoginUserModel user)
        {
            return userService.Login(user);
        }

        [HttpPut]
        [Route("update")]
        public bool UpdateUser([FromBody] UpdateUserModel updatedUser, int id)
        {
            return userService.Update(updatedUser, id);
        }

        [HttpDelete]
        [Route("delete")]
        public bool DeleteUser(int id)
        {
            return userService.Delete(id);
        }


    }
}