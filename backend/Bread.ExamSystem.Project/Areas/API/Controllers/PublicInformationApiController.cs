using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bread.ExamSystem.Project.Model;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace Bread.ExamSystem.Project.Areas.API.Controllers
{
    [Area("API")]
    [Public]
    [ActionDescription("公共信息")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PublicInformationApiController : BaseApiController
    {
        /// <summary>
        /// 获取全部单位信息
        /// </summary>
        /// <returns></returns>
        [ActionDescription("获取全部单位信息")]
        [HttpGet]
        public async  Task<IActionResult> UnitWorkInFo()
        {
            var list = await DC.Set<UnitWork>()
                .AsNoTracking()
                .ToListAsync();
            return Ok(list);
        }
    }
}
