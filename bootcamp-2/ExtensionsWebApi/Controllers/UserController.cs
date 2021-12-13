using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ExtensionsWebApi.Database;
using ExtensionsWebApi.UserOperations;
using static ExtensionsWebApi.UserOperations.CreateUserCommand;
using ExtensionsWebApi.Attributes;
//Linq dökümantasyonu
//https://docs.microsoft.com/tr-tr/dotnet/csharp/linq/linq-in-csharp

namespace ExtensionsWebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]

    public class UserController : ControllerBase
    {
        private readonly UsersDbContext _context;

        public UserController(UsersDbContext context)
        {
            _context = context;
        }

        [LoggerAttribute]//Verilen Http verb'in çalıştığı anı loglar
        [HttpGet]
        public IActionResult getUsers()
        {
            //Belirli işlemi yapan classa contexti gönderip class içinde işlemler yaptırdım
            GetUsersCommand command = new GetUsersCommand(_context);
            var result = command.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]//fixed...
        public IActionResult GetById(int id)
        {
            UsersDetailViewModel result;//geri gönderilen veri modeli
            try
            {
                GetUsersDetailCommand command = new GetUsersDetailCommand(_context);
                command.UserId = id;//gelen id ile databasedeki modeller arasında id eşleştirmesi
                result = command.Handle();//eşleşilen modeli geri gönderen Handle() metodu
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);// GetUsersDetailCommand classından dönen hatayı basar.
            }
            return Ok(result);//200 kodu ile eşleşen veriyi döner
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context);

            try
            {
                command.Model = newUser;//Body'den alınan modeli komut classındaki modele eşitler
                command.Handle();//veriyi database'e ekler --(context.SaveChanges())
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);//CreateUserCommand classından dönen hatayı basar
            }
            return Ok();//200 kodu
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserModel updatedUser)
        {
            UpdateUserCommand command = new UpdateUserCommand(_context);

            try
            {
                command.Model = updatedUser;//değiştirilen model ile class içindeki modeli eşitler
                command.UserId = id;//id doğrulaması
                command.Handle();//database kayıt
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);//UpdateUserCommand classından dönen hatayı basar
            }

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                DeleteUserCommand command = new DeleteUserCommand(_context);
                command.userId = id;//id doğrulaması
                command.Handle();//database'den veriyi siler ---Remove()
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);//DeleteUserCommand classından dönen hatayı basar
            }
            return Ok();
        }


    }
}