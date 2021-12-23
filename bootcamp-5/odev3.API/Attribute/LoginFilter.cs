using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using odev3.API.Cache;

namespace odev3.API.Attribute
{
    public class LoginFilter : ActionFilterAttribute
    {
        private readonly IUserCache userCache;

        public LoginFilter(IUserCache _userCache)
        {//cache kullanımı 
            userCache = _userCache;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {//cachelenmiş login verisi kontrolü
            if (userCache.GetCachedUser() is null)
            {//cachedeki kayıtlı user null ise işlemi kesip login uyarısı verir.
                context.Result = new BadRequestObjectResult("You are not logged in...");
            }
        }


    }
}