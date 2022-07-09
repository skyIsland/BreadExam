using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using Bread.ExamSystem.Project.Model;
using System.Threading.Tasks;
using Bread.ExamSystem.Project.Model.FrameworkEnumeration;
using Microsoft.EntityFrameworkCore;
using Bread.ExamSystem.Project.ViewModel.API.ExaminationSetupNoAcVMs;

namespace TestSystem.Controllers
{
    [Area("API")]
    [Public]
    [ActionDescription("非账号考试")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public partial class ExaminationSetupNoAcApiController : BaseApiController
    {
        /// <summary>
        /// 获取所有状态正常考试信息
        /// </summary>
        /// <returns></returns>
        [ActionDescription("获取所有状态正常考试信息")]
        [HttpGet]
        public async Task<IActionResult> AllAtctivitiesList()
        {
            var list = await DC.Set<ExaminationSetup>()
                .Where(x => x.CourseEnum == CourseEnum.正常 && x.ParticipationTypes == ParticipationTypes.非账号)
                .Where(x => x.StrTime <= DateTime.Now && DateTime.Now <= x.EndTime)
                .Select(x => new
                {
                    id = x.ID,
                    title = x.Title,
                    strTime = x.StrTime.ToString("yyyy-MM-dd"),
                    endTime = x.EndTime.ToString("yyyy-MM-dd"),
                    testTime = x.TestTime
                })
                .ToListAsync();
            return Ok(list);
        }
        /// <summary>
        /// 获取单场考试信息
        /// </summary>
        /// <param name="id">试卷id</param>
        /// <returns></returns>
        [ActionDescription("获取单场考试信息")]
        [HttpGet]
        public async Task<IActionResult> AtctivitiesListOne(int id)
        {
            //获取考试数据
            var es = await DC.Set<ExaminationSetup>()
                .SingleOrDefaultAsync(x => x.ID == id);
            //获取试卷配套轮播图
            var lisrrc = await DC.Set<RotationChart>()
                .Where(x => x.ExaminationSetupID == id)
                .ToListAsync();
            var redata = new
            {
                ks = es,
                lbt = lisrrc
            };
            return Ok(redata);
        }
        /// <summary>
        /// 检查是或否已报名参加
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> SingUp(SingUpDto dto)
        {
            if (ModelState.IsValid)
            {
                var chk = await DC.Set<RecordNoAccount>()
                .Where(x => x.ExaminationSetupID == dto.ExaminationSetupID
                && x.UserName == dto.UserName
                && x.UnitWork == dto.UnitWork
                && x.Phone == dto.Phone
                )
                .ToListAsync();
                if (chk.Count > 0)
                {
                    return Content("您已参加过本场考试");
                }
                RecordNoAccount lr = new RecordNoAccount
                {
                    ExaminationSetupID = dto.ExaminationSetupID,
                    ParticipationTime = DateTime.Now,
                    UserName = dto.UserName,
                    UnitWork = dto.UnitWork,
                    Phone = dto.Phone
                };
                await DC.Set<RecordNoAccount>().AddAsync(lr);
                await DC.SaveChangesAsync();
                return Content(lr.ID.ToString());
            }
            return BadRequest(ModelState);


        }
        


    }
}
