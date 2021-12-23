using odev3.Models.User;

namespace odev3.API.Cache
{
    public interface IUserCache
    {
        public void Cache(LoginUserModel user);
        public LoginUserModel GetCachedUser();


    }
}