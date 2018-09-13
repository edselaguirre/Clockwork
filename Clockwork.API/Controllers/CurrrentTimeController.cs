using System;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;
using System.Threading.Tasks;
using Clockwork.API.Helper;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class CurrentTimeController : Controller
    {
        // GET api/currenttime
        [HttpGet]
        public async Task<IActionResult> Get(string userTimezoneId)
        {
            TimeZoneInfo getTimeZoneId = TimeZoneInfo.FindSystemTimeZoneById(userTimezoneId);

            var utcTime = DateTime.UtcNow;
            var serverTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, getTimeZoneId);           
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

            int pageNo = 1;
            int pageSize = 10;
            //adding page to my data.
            Pagination<CurrentTimeQuery> paginationData;



            var returnVal = new CurrentTimeQuery
            {
                UTCTime = utcTime,
                ClientIp = ip,
                Time = serverTime
            };

            using (var db = new ClockworkContext())
            {
                db.CurrentTimeQueries.Add(returnVal);
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                foreach (var CurrentTimeQuery in db.CurrentTimeQueries)
                {
                    Console.WriteLine(" - {0}", CurrentTimeQuery.UTCTime);
                }

                //default to desc so new entry can be easily see
                var data = db.CurrentTimeQueries.AsNoTracking().OrderByDescending(x => x.CurrentTimeQueryId);
                paginationData = await Pagination<CurrentTimeQuery>.CreateAsync(data, pageNo, pageSize);
            }

            return Ok(new
            {
                currentData = returnVal
                ,
                displayedData = paginationData
                ,
                pageNumber = 1
                ,
                pageNext = paginationData.HasNextPage
                ,
                pagePrev = paginationData.HasPreviousPage
                ,
                totalPage = paginationData.TotalPages
            });
        }
    }    
}
