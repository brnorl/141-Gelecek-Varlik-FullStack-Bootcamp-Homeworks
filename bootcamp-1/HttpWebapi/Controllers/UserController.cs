using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
//Linq dökümantasyonu
//https://docs.microsoft.com/tr-tr/dotnet/csharp/linq/linq-in-csharp

namespace HttpWebapi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]

    public class UserController : ControllerBase
    {
        private static List<User> UserList = new List<User>()
        {
            new User{
                Id = 1,
                Name = "Baransel",
                Surname = "Oral",
                Email = "brnorl47@gmail.com",
                PhoneNumber = "123567893"
            },
            new User{
                Id = 2,
                Name = "Umut",
                Surname = "bozbek",
                Email = "umud@gmail.com",
                PhoneNumber = "123567893"
            },
            new User{
                Id = 3,
                Name = "Efe",
                Surname = "Karahanlı",
                Email = "efe13@gmail.com",
                PhoneNumber = "123567893"
            },
            new User{
                Id = 4,
                Name = "Üstün",
                Surname = "Kısa",
                Email = "kısa123@gmail.com",
                PhoneNumber = "123567893"
            },
            new User{
                Id = 5,
                Name = "Fethi",
                Surname = "Güngördü",
                Email = "brnorl47@gmail.com",
                PhoneNumber = "123567893"
            },
            new User{
                Id = 6,
                Name = "Arda",
                Surname = "Turan",
                Email = "arda123@gmail.com",
                PhoneNumber = "123567893"
            },
        };

        [HttpGet]
        public List<User> getUsers()
        {
            var userList = UserList.OrderBy(x => x.Id).ToList<User>();//linq
            return userList;
        }

        [HttpGet("{id}")]
        public User GetById(int id)
        {
            var user = UserList.Where(user => user.Id == id).SingleOrDefault();
            return user;
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User newUser)
        {
            var user = UserList.SingleOrDefault(x => x.Email == newUser.Email);
            if (user is not null)
            {
                return BadRequest();
            }
            UserList.Add(newUser);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = UserList.SingleOrDefault(x => x.Id == id);
            if (user is null)
            {
                return BadRequest();
            }
            user.Name = updatedUser.Name != default ? updatedUser.Name : user.Name;
            user.Surname = updatedUser.Surname != default ? updatedUser.Surname : user.Surname;
            user.Email = updatedUser.Email != default ? updatedUser.Email : user.Email;
            user.PhoneNumber = updatedUser.PhoneNumber != default ? updatedUser.PhoneNumber : user.PhoneNumber;

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = UserList.SingleOrDefault(x => x.Id == id);
            if (user is null)
            {
                return BadRequest();
            }
            UserList.Remove(user);
            return Ok();
        }


    }
}