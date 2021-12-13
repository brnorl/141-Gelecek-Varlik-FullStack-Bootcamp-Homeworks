using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExtensionsWebApi.Attributes
{
    public class LoggerAttribute : Attribute
    {
        public LoggerAttribute()
        {//Attribute verilen http verb tetiklendiği anda çalışır
            var time = DateTime.Now;
            var message = "Logged in:" + time;
            Console.WriteLine(message);
            //Output--->Logged in:3.12.2021 19:17:33
        }
    }
}