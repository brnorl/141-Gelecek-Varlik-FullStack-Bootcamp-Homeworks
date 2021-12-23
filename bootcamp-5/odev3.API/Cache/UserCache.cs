using System;
using Microsoft.Extensions.Caching.Memory;
using odev3.Models.User;

namespace odev3.API.Cache
{
    public class UserCache : IUserCache
    {
        private const string KEY = "user";
        private readonly IMemoryCache memoryCache;
        public UserCache(IMemoryCache _memoryCache)
        {
            memoryCache = _memoryCache;
        }
        public void Cache(LoginUserModel user)
        {//Giriş yapılan veriyi cacheleme.
            var option = new MemoryCacheEntryOptions
            {//cache için ayarlamalar, cache'de tutulacak verinin yaşam süresi ayarlanır.
                SlidingExpiration = TimeSpan.FromHours(10),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(20),
                //Denemek için 10sn aralıklarla ayarlamalar yaptım...Özelleştirilebilir.
            };
            //Key,alınan model ve ayarlarla beraber Set işlemi
            memoryCache.Set<LoginUserModel>(KEY, user, option);
        }
        public LoginUserModel GetCachedUser()
        {//cachelenmiş veriyi döner.----kontrol için---
            LoginUserModel user = new LoginUserModel();
            if (!memoryCache.TryGetValue(KEY, out user))
            {//cache'de yoksa null model döner
                return user;
            }
            //varsa olan modeli döner.
            return memoryCache.Get<LoginUserModel>(KEY);
        }
    }
}