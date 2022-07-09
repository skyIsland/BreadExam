using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.ViewModel.API.TestQuestionsVMs;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace Bread.ExamSystem.Project.Areas.API.Controllers
{
    [Area("API")]
    [Public]
    [ActionDescription("试卷组装")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestQuestionsApiController : BaseApiController
    {
        /// <summary>
        /// 组装试卷
        /// </summary>
        /// <param name="acid">试卷id</param>
        /// <param name="jlid">考试记录id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        [ActionDescription("组装试卷")]
        [HttpGet]
        public async Task<IActionResult> GetTestPaper(int acid, int jlid, string type)
        {
            var model = await DC.Set<ExaminationSetup>()
               .FirstOrDefaultAsync(x => x.ID == acid);
            //试卷id          
            List<QuestionDto> questions = new List<QuestionDto>();
            //单选
            var dxlist = await GetQuestions(DC.Set<QuestionType>().FirstOrDefault(x => x.Name == "单选").ID, model.DXNumer, model.Subject);
            //多选
            var dsxlist = await GetQuestions(DC.Set<QuestionType>().FirstOrDefault(x => x.Name == "多选").ID, model.DSXNumer, model.Subject);
            //判断
            var pdlist = await GetQuestions(DC.Set<QuestionType>().FirstOrDefault(x => x.Name == "判断").ID, model.PDNumer, model.Subject);
            questions.AddRange(dxlist);
            questions.AddRange(pdlist);
            questions.AddRange(dsxlist);

            if (type == "非账号")
            {
                var recordNoAccount = await DC.Set<RecordNoAccount>().SingleOrDefaultAsync(x => x.ID == jlid);
                recordNoAccount.QuestionId = string.Join(",", questions.Select(x => x.ID).ToArray());
                recordNoAccount.QuestionAnswer = string.Join(",", questions.Select(x => x.Anwser).ToArray());
                DC.Set<RecordNoAccount>().Update(recordNoAccount);
                await DC.SaveChangesAsync();
            }
            else
            {
                var recordWithAccount = await DC.Set<RecordWithAccount>().SingleOrDefaultAsync(x => x.ID == jlid);
                recordWithAccount.QuestionId = string.Join(",", questions.Select(x => x.ID).ToArray());
                recordWithAccount.QuestionAnswer = string.Join(",", questions.Select(x => x.Anwser).ToArray());
                DC.Set<RecordWithAccount>().Update(recordWithAccount);
                await DC.SaveChangesAsync();
            }
            //考试时间 毫秒
            var jssj = model.TestTime * 60 * 1000;
            var txsj = (model.TestTime - 5) * 60 * 1000;
            var redata = new
            {
                esid = acid,
                sj = model.TestTime,
                jssj = jssj,
                txsj = txsj,
                questions = questions,
            };
            return Ok(redata);
        }
        /// <summary>
        /// 判分
        /// </summary>
        /// <returns></returns>
        [ActionDescription("判分")]
        [HttpPost]
        public async Task<IActionResult> Fraction(Fractiondto dto)
        {
            if (ModelState.IsValid)
            {
                double fen = 0;
                var course = await DC.Set<ExaminationSetup>()
                .SingleOrDefaultAsync(x => x.ID == dto.Acid);

                var lr = await DC.Set<RecordNoAccount>()
              .SingleOrDefaultAsync(x => x.ID == dto.Jlid);

                string[] userAnswers = dto.ExamineeAnswers;
                string[] rightAnswers = lr.QuestionAnswer.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                fen = await Settlement(userAnswers, rightAnswers, course.DXNumer, course.DSXNumer, course.PDNumer, course.DXScore, course.DSXScore, course.PDScore);
                lr.Achievement = fen;
                lr.ExamineeAnswers = string.Join(",", dto.ExamineeAnswers);
                DC.Set<RecordNoAccount>().Update(lr);
                await DC.SaveChangesAsync();


                return Content(fen.ToString());
            }
            return BadRequest(ModelState);
        }
        [AuthorizeJwtWithCookie]
        [HttpPost]
        public async Task<IActionResult> FractionAc(Fractiondto dto)
        {
            if (ModelState.IsValid)
            {
                double fen = 0;
                var lr = await DC.Set<RecordWithAccount>()
                      .SingleOrDefaultAsync(x => x.ID == dto.Jlid);
                var course = await DC.Set<ExaminationSetup>()
                   .SingleOrDefaultAsync(x => x.ID == dto.Acid);
                string[] userAnswers = dto.ExamineeAnswers;
                string[] rightAnswers = lr.QuestionAnswer.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] QuestionIds = lr.QuestionId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                fen = await Settlement(userAnswers, rightAnswers, course.DXNumer, course.DSXNumer, course.PDNumer, course.DXScore, course.DSXScore, course.PDScore, QuestionIds);
                lr.Achievement = fen;
                lr.ExamineeAnswers = string.Join(",", dto.ExamineeAnswers);
                DC.Set<RecordWithAccount>().Update(lr);
                await DC.SaveChangesAsync();
                return Content(fen.ToString());
            }
            return BadRequest(ModelState);
        }
        /// <summary>
        /// 查看证书
        /// </summary>
        /// <param name="id">参加考试id</param>
        /// <param name="username">用户名</param>
        /// <param name="unitwork">单位</param>
        /// <param name="fen">得分</param>
        /// <returns></returns>
        [Public]
        [HttpGet]
        [ActionDescription("查看证书")]
        public async Task<IActionResult> Certificate(int id, string username, string unitwork, string fen)
        {
            var course = await DC.Set<ExaminationSetup>()
                 .SingleOrDefaultAsync(x => x.ID == id);
            var seal = await DC.Set<Seal>()
                 .SingleOrDefaultAsync(x => x.ID == course.SealId);
            var imgid = seal.PhotoId;
            var file = await DC.Set<FileAttachment>()
                 .SingleOrDefaultAsync(x => x.ID == imgid);
            var imgdata = file.FileData;
            var img = Util.ImgData.ReturnPhoto(imgdata);
            int width = img.Size.Width;   // 图片的宽度
            int height = img.Size.Height;   // 图片的高度
            Graphics g = Graphics.FromImage(img);
            StringFormat TitleFormat = new StringFormat();
            StringFormat TitleFormat1 = new StringFormat();
            TitleFormat.Alignment = StringAlignment.Center; //居中
            TitleFormat1.LineAlignment = StringAlignment.Far;
            g.DrawString("合格证书", new Font("黑体", 25), Brushes.Black, new Point(width / 2, 250), TitleFormat);
            g.DrawString(unitwork + username + ":", new Font("宋体", 15), Brushes.Black, new Point(150, 350));
            g.DrawString(course.Title, new Font("宋体", 15), Brushes.Black, new Point(150, 400));
            g.DrawString("考成成绩" + fen + "分", new Font("宋体", 15), Brushes.Red, new Point(150, 450));
            g.DrawString("徐州易联财税", new Font("黑体", 15), Brushes.Black, new Point(width / 2 + 60, height - 150), TitleFormat1);

            //g.DrawString("证件号：00001", new Font("黑体", 15), Brushes.Black, new Point(width / 8, height - 175));
            var imgre = Util.ImgData.PhotoImageInsert(img);
            await Response.Body.WriteAsync(imgre, 0, imgdata.Count());
            return new EmptyResult();
            //return new FileContentResult(imgre, "image/jpeg");
        }
        /// <summary>
        /// 查看单个题目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Public]
        [HttpGet]
        [ActionDescription("查看单个题目")]
        public async Task<IActionResult> GetOneQuestion(int id)
        {
            var model = await DC.Set<Question>()
                 .Include(x => x.QuestionType)                    
                .Select(x => new QuestionDto
                {
                    ID = x.ID,
                    QuestionText = x.QuestionText,
                    Anwser = x.Anwser,
                    OptionA = x.OptionA,
                    OptionB = x.OptionB,
                    OptionC = x.OptionC,
                    OptionD = x.OptionD,
                    QuestionType = x.QuestionType.Name,
                    Subject = x.QuestionText,
                    Pars = x.Pars,
                })
                .SingleOrDefaultAsync(x => x.ID == id);
            if (model!=null)
            {
                return Ok(model);
            }
            return BadRequest("题目不存在或被删除");
        }
        /// <summary>
        /// 随机抽题
        /// </summary>
        /// <param name="questionType"></param>
        /// <param name="number"></param>
        /// <param name="sub"></param>
        /// <returns></returns>      
        private async Task<List<QuestionDto>> GetQuestions(int questionType, int number, string sub)
        {
            List<QuestionDto> QuestionList = new List<QuestionDto>();
            var collection = await DC.Set<Question>()
                .Include(x => x.QuestionType)
                .Where(x => x.Subject == sub)
                .Where(x => x.QuestionTypeID == questionType)
                .Select(x => new QuestionDto
                {
                    ID = x.ID,
                    QuestionText = x.QuestionText,
                    Anwser = x.Anwser,
                    OptionA = x.OptionA,
                    OptionB = x.OptionB,
                    OptionC = x.OptionC,
                    OptionD = x.OptionD,
                    QuestionType = x.QuestionType.Name,
                    Subject = x.QuestionText,
                    Pars = x.Pars,
                })
                .ToListAsync();
            Random rd = new Random();
            for (int i = 0; i < number; i++)
            {
                int index = rd.Next(collection.Count());
                var model = collection[index];
                QuestionList.Add(model);
                collection.Remove(model);
            }
            return QuestionList;
        }
        /// <summary>   
        /// 从数组中截取一部分成新的数组   
        /// </summary>   
        /// <param name="Source">原数组</param>   
        /// <param name="StartIndex">原数组的起始位置</param>   
        /// <param name="EndIndex">原数组的截止位置</param>   
        /// <returns></returns>   
        private string[] SplitArray(string[] Source, int StartIndex, int EndIndex)
        {

            try
            {
                string[] result = new string[EndIndex - StartIndex + 1];
                for (int i = 0; i <= EndIndex - StartIndex; i++) result[i] = Source[i + StartIndex];
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// 分数结算
        /// </summary>
        /// <param name="userAnswers">用户答案</param>
        /// <param name="rightAnswers">正确答案</param>
        /// <param name="DXNumer">单选题数量</param>
        /// <param name="DSXNumer">多选题数量</param>
        /// <param name="PDNumer">判断题数量</param>
        /// <param name="DXScore">单选题得分</param>
        /// <param name="DSXScore">多选题得分</param>
        /// <param name="PDScore">判断题得分</param>
        /// <returns></returns>
        private async Task<double> Settlement(string[] userAnswers, string[] rightAnswers, int DXNumer, int DSXNumer, int PDNumer, double DXScore, double DSXScore, double PDScore)
        {
            string[] userDX = SplitArray(userAnswers, 0, DXNumer - 1);
            string[] userPD = SplitArray(userAnswers, DXNumer, DXNumer + PDNumer - 1);
            string[] userDSX = SplitArray(userAnswers, DXNumer + PDNumer, DXNumer + PDNumer + DSXNumer - 1);

            string[] userDXR = SplitArray(rightAnswers, 0, DXNumer - 1);
            string[] userPDR = SplitArray(rightAnswers, DXNumer, DXNumer + PDNumer - 1);
            string[] userDSXR = SplitArray(rightAnswers, DXNumer + PDNumer, DXNumer + PDNumer + DSXNumer - 1);

            int userDXEnRight = 0;
            int userDSXEnRight = 0;
            int userPDEnRight = 0;
            List<WrongQuestionBank> listadd = new List<WrongQuestionBank>();
            List<WrongQuestionBank> listup = new List<WrongQuestionBank>();
            for (int i = 0; i < userDXR.Length; i++)
            {
                if (userDX[i] == userDXR[i])
                {
                    userDXEnRight++;
                }


            }
            for (int i = 0; i < userPDR.Length; i++)
            {
                if (userPD[i] == userPDR[i])
                {
                    userPDEnRight++;
                }

            }
            for (int i = 0; i < userDSXR.Length; i++)
            {
                if (userDSX[i] == userDSXR[i])
                {
                    userDSXEnRight++;
                }

            }
            double fen = userDXEnRight * DXScore + userDSXEnRight * DSXScore + userPDEnRight * PDScore;
            await DC.Set<WrongQuestionBank>().AddRangeAsync(listadd);
            DC.Set<WrongQuestionBank>().UpdateRange(listup);
            await DC.SaveChangesAsync();
            return fen;
        }
        private async Task<double> Settlement(string[] userAnswers, string[] rightAnswers, int DXNumer, int DSXNumer, int PDNumer, double DXScore, double DSXScore, double PDScore, string[] questionId)
        {
            string[] userDX = SplitArray(userAnswers, 0, DXNumer - 1);
            string[] userPD = SplitArray(userAnswers, DXNumer, DXNumer + PDNumer - 1);
            string[] userDSX = SplitArray(userAnswers, DXNumer + PDNumer, DXNumer + PDNumer + DSXNumer - 1);

            string[] userDXR = SplitArray(rightAnswers, 0, DXNumer - 1);
            string[] userPDR = SplitArray(rightAnswers, DXNumer, DXNumer + PDNumer - 1);
            string[] userDSXR = SplitArray(rightAnswers, DXNumer + PDNumer, DXNumer + PDNumer + DSXNumer - 1);

            int userDXEnRight = 0;
            int userDSXEnRight = 0;
            int userPDEnRight = 0;
            List<WrongQuestionBank> listadd = new List<WrongQuestionBank>();
            List<WrongQuestionBank> listup = new List<WrongQuestionBank>();
            for (int i = 0; i < userDXR.Length; i++)
            {
                if (userDX[i] == userDXR[i])
                {
                    userDXEnRight++;
                }
                else
                {

                    var questionIddx = Convert.ToInt32(questionId[i]);

                    var chk = await DC.Set<WrongQuestionBank>().SingleOrDefaultAsync(x => x.QuestionID == questionIddx && x.FrameworkUserID == new Guid(Wtm.LoginUserInfo.UserId));
                    if (chk != null)
                    {
                        chk.WrongNumber += 1;
                        listup.Add(chk);
                    }
                    else
                    {
                        WrongQuestionBank bank = new WrongQuestionBank
                        {
                            QuestionID = questionIddx,
                            FrameworkUserID = new Guid(Wtm.LoginUserInfo.UserId),
                            WrongNumber = 1
                        };
                        listadd.Add(bank);
                    }

                }

            }
            for (int i = 0; i < userPDR.Length; i++)
            {
                if (userPD[i] == userPDR[i])
                {
                    userPDEnRight++;
                }
                else
                {

                    var questionIdpd = Convert.ToInt32(questionId[i + userDXR.Length]);

                    var chk = await DC.Set<WrongQuestionBank>().SingleOrDefaultAsync(x => x.QuestionID == questionIdpd && x.FrameworkUserID == new Guid(Wtm.LoginUserInfo.UserId));
                    if (chk != null)
                    {
                        chk.WrongNumber += 1;
                        listup.Add(chk);
                    }
                    else
                    {
                        WrongQuestionBank bank = new WrongQuestionBank
                        {
                            QuestionID = questionIdpd,
                            FrameworkUserID = new Guid(Wtm.LoginUserInfo.UserId),
                            WrongNumber = 1
                        };
                        listadd.Add(bank);
                    }

                }
            }
            for (int i = 0; i < userDSXR.Length; i++)
            {
                if (userDSX[i] == userDSXR[i])
                {
                    userDSXEnRight++;
                }
                else
                {

                    var questionIddxx = Convert.ToInt32(questionId[i + userDXR.Length + userPDR.Length]);

                    var chk = await DC.Set<WrongQuestionBank>().SingleOrDefaultAsync(x => x.QuestionID == questionIddxx && x.FrameworkUserID == new Guid(Wtm.LoginUserInfo.UserId));
                    if (chk != null)
                    {
                        chk.WrongNumber += 1;
                        listup.Add(chk);
                    }
                    else
                    {
                        WrongQuestionBank bank = new WrongQuestionBank
                        {
                            QuestionID = questionIddxx,
                            FrameworkUserID = new Guid(Wtm.LoginUserInfo.UserId),
                            WrongNumber = 1
                        };
                        listadd.Add(bank);
                    }

                }
            }
            double fen = userDXEnRight * DXScore + userDSXEnRight * DSXScore + userPDEnRight * PDScore;
            await DC.Set<WrongQuestionBank>().AddRangeAsync(listadd);
            DC.Set<WrongQuestionBank>().UpdateRange(listup);
            await DC.SaveChangesAsync();
            return fen;
        }
    }
}
