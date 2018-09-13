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
    public class PageController : Controller
    {
        //get next page of data
        [HttpGet]
        public async Task<IActionResult> GetPageNo(int pageNo)
        {
            var db = new ClockworkContext();           
            Pagination<CurrentTimeQuery> paginationData;
            int pageSize = 10;
            var data = db.CurrentTimeQueries.AsNoTracking().OrderByDescending(x => x.CurrentTimeQueryId);
            paginationData = await Pagination<CurrentTimeQuery>.CreateAsync(data, pageNo, pageSize);

            return Ok(new
            {
                displayedData = paginationData
                ,
                pageNumber = pageNo
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
