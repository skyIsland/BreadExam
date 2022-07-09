using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.Model.Dto;
using Bread.ExamSystem.Project.Model.FrameworkEnumeration;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace Bread.ExamSystem.Project.Areas.API.Controllers
{
    [Area("API")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("账号考试/练习")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ExaminationSetupWithAcApiController : BaseApiController
    {
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var user = await DC.Set<FrameworkUser>()
                .Include(x => x.UnitWork)
                .Select(x => new
                {
                    ID = x.ID,
                    UserName = x.Name,
                    UnitWork = x.UnitWork.UnitWorkName,
                    PhotoId = x.PhotoId
                })
                .SingleOrDefaultAsync(x => x.ID.ToString() == Wtm.LoginUserInfo.UserId);
            return Ok(user);

        }
        /// <summary>
        /// 获取所有状态正常考试信息
        /// </summary>
        /// <returns></returns>
        [ActionDescription("获取所有状态正常考试信息")]
        [HttpGet]
        public async Task<IActionResult> AllAtctivitiesList()
        {
            var list = await DC.Set<ExaminationSetup>()
                .Where(x => x.CourseEnum == CourseEnum.正常 && x.ParticipationTypes == ParticipationTypes.账号)
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
        /// 试题收藏
        /// </summary>
        /// <param name="questionId">问题id</param>
        /// <returns></returns>
        [ActionDescription("试题收藏")]
        [HttpGet]
        public async Task<IActionResult> QuestionsCollection(int questionId)
        {
            if (questionId == 0)
            {
                return Content("系统错误");
            }
            Collection collection = new Collection
            {
                QuestionID = questionId,
                FrameworkUserID = new Guid(Wtm.LoginUserInfo.UserId)
            };
            await DC.Set<Collection>().AddAsync(collection);
            int q = await DC.SaveChangesAsync();
            if (q > 0)
            {
                return Content("收藏成功");
            }
            return Content("系统错误");

        }
        /// <summary>
        /// 参加考试
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionDescription("参加考试")]
        [HttpGet]
        public async Task<IActionResult> PartExamination(int id)
        {
            var recordWithAccount = await DC.Set<RecordWithAccount>()
                .FirstOrDefaultAsync(x => x.ExaminationSetupID == id && x.FrameworkUserID == new Guid(Wtm.LoginUserInfo.UserId));
            if (recordWithAccount == null)
            {
                RecordWithAccount record = new RecordWithAccount
                {
                    ExaminationSetupID = id,
                    ParticipationTime = DateTime.Now,
                    FrameworkUserID = new Guid(Wtm.LoginUserInfo.UserId),
                    Achievement = 0
                };
                await DC.Set<RecordWithAccount>().AddAsync(record);
                int q = await DC.SaveChangesAsync();
                if (q > 0)
                {
                    return Content(record.ID.ToString());
                }
                return Content("系统错误");
            }
            else
            {
                return Content("您已参加过本场考试，感谢您的参与");
            }

            //检查是否参加考试

        }
        /// <summary>
        /// 获取考试记录下拉加载更多
        /// </summary>
        /// <param name="pageNum">当前页</param>
        /// <param name="pageSize">每页显示多少</param>
        /// <returns></returns>
        [ActionDescription("获取考试记录下拉加载更多")]
        [HttpGet]
        public async Task<IActionResult> ExaminationRecord(int pageNum, int pageSize)
        {
            var list = await DC.Set<RecordWithAccount>()
               .Include(x => x.ExaminationSetup)
               .Include(x => x.FrameworkUser)
               .Include(x => x.FrameworkUser.UnitWork)
               .AsNoTracking()
               .Where(x => x.FrameworkUserID == new Guid(Wtm.LoginUserInfo.UserId))
               .OrderByDescending(x => x.ID)
               .Skip((pageNum - 1) * pageSize)
               .Take(pageSize)
               .OrderByDescending(x => x.ParticipationTime)
               .Select(x => new
               {
                   id = x.ID,
                   fen = x.Achievement,
                   xm = x.FrameworkUser.Name,
                   dw = x.FrameworkUser.UnitWork.UnitWorkName,
                   ks = x.ExaminationSetup.Title,
                   acid = x.ExaminationSetupID
               })
                .ToListAsync();
            bool hasNext = true;
            var totalSize = DC.Set<RecordWithAccount>().Where(x => x.FrameworkUserID == new Guid(Wtm.LoginUserInfo.UserId)).Count();
            if (pageNum== (totalSize + pageSize - 1) / pageSize)
            {
                hasNext = false;
            }
            var temp = new
            {
                data = list,
                totalPage = (totalSize + pageSize - 1) / pageSize,
                totalSize,
                hasNext
            };
            return Ok(temp);

        }
        /// <summary>
        /// 获取错题记录
        /// </summary>
        /// <param name="pageNum">当前页</param>
        /// <param name="pageSize">每页显示多少</param>
        /// <returns></returns>
        [ActionDescription("获取错题记录")]
        [HttpGet]
        public async Task<IActionResult> WrongQuestionRecord(int pageNum, int pageSize)
        {
            var list = await DC.Set<WrongQuestionBank>()
               .AsNoTracking()
               .Where(x => x.FrameworkUserID == new Guid(Wtm.LoginUserInfo.UserId))
               .OrderByDescending(x => x.ID)
               .Skip((pageNum - 1)* pageSize)           
               .Take(pageSize)               
               .ToListAsync();
            List<WrongQuestionRecord> listwqr = new List<WrongQuestionRecord>(); 
            foreach (var item in list)
            {
                var qt = await DC.Set<Question>().FirstOrDefaultAsync(x => x.ID == item.QuestionID);
                if (qt!=null)
                {
                    WrongQuestionRecord model = new WrongQuestionRecord
                    {
                        QuestionID = item.QuestionID,
                        QuestionText = qt.QuestionText,
                        WrongNumber = item.WrongNumber
                    };
                    listwqr.Add(model);
                }                
            }
            bool hasNext = true;
            var totalSize = DC.Set<WrongQuestionBank>().Where(x => x.FrameworkUserID == new Guid(Wtm.LoginUserInfo.UserId)).Count();
            if (pageNum == (totalSize + pageSize - 1) / pageSize)
            {
                hasNext = false;
            }
            var temp = new
            {
                data = listwqr.OrderByDescending(x=>x.WrongNumber),
                totalPage = (totalSize + pageSize - 1) / pageSize,
                totalSize,
                hasNext
            };
            return Ok(temp);

        }
    }
}
