using Clockwork.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class TimezoneInfoController : Controller
    {
        [HttpGet]
        public IActionResult GetTimezoneInfo()
        {
            List<TimezoneInfo> TimezoneInfoList = new List<TimezoneInfo>();
            var userTimezoneInfo = new TimezoneInfo();
            //collect timezone list
            foreach (var item in TimeZoneInfo.GetSystemTimeZones())
            {
                TimezoneInfoList.Add(new TimezoneInfo
                {
                    Id = item.Id
                    ,
                    Name = item.Id + "-" + item.DisplayName
                });
                //get user timezone for default value
                if (TimeZoneInfo.Local.Id == item.Id)
                {
                    userTimezoneInfo = new TimezoneInfo()
                    {
                        Id = item.Id
                        ,
                        Name = item.DisplayName
                    };
                }
            }
            return Ok(new
            {
                userTimezone = userTimezoneInfo
                ,
                TimezoneList = TimezoneInfoList
            });
        }
    }
}
