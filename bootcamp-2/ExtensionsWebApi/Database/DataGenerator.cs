using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExtensionsWebApi.Database
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //context değişkenine database atılması -->
            using (var context = new UsersDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<UsersDbContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }
                context.Users.AddRange(//database içine Users yaratılması
                    new User
                    {
                        Name = "Baransel",
                        Surname = "Oral",
                        Email = "brnorl47@gmail.com",
                        PhoneNumber = "123567893"
                    },
                    new User
                    {
                        Name = "Umut",
                        Surname = "bozbek",
                        Email = "umud@gmail.com",
                        PhoneNumber = "123567893"
                    },
                    new User
                    {
                        Name = "Efe",
                        Surname = "Karahanlı",
                        Email = "efe13@gmail.com",
                        PhoneNumber = "123567893"
                    },
                    new User
                    {
                        Name = "Üstün",
                        Surname = "Kısa",
                        Email = "kısa123@gmail.com",
                        PhoneNumber = "123567893"
                    },
                    new User
                    {
                        Name = "Fethi",
                        Surname = "Güngördü",
                        Email = "brnorl47@gmail.com",
                        PhoneNumber = "123567893"
                    },
                    new User
                    {
                        Name = "Arda",
                        Surname = "Turan",
                        Email = "arda123@gmail.com",
                        PhoneNumber = "123567893"
                    }
                        );
                //Database içerisindeki context'e kayıt işlemi --önemli,kalıcı olmasını sağlar--
                context.SaveChanges();
            }
        }
    }
}