using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace Bread.ExamSystem.Project.Model
{
    /// <summary>
    /// 免账号参与考试记录
    /// </summary>
    public class RecordNoAccount : BasePoco
    {
        public new int ID { get; set; }
        [Display(Name = "参与试卷")]
        [Required(ErrorMessage = "参与试卷是必须填写")]
        public int ExaminationSetupID { get; set; }
        [Display(Name = "参与试卷")]
        public ExaminationSetup ExaminationSetup { get; set; }
        [Display(Name = "参与时间")]
        [Required(ErrorMessage = "参与时间是必须填写")]
        public DateTime ParticipationTime { get; set; }
        [Display(Name = "参与人")]
        [Required(ErrorMessage = "参与时间是必须填写")]
        public string UserName { get; set; }
        [Display(Name = "所在单位")]
        [Required(ErrorMessage = "所在单位是必须填写")]
        public string UnitWork { get; set; }
        [Display(Name = "考试成绩")]
        [Required(ErrorMessage = "考试成绩是必须填写")]
        public double Achievement { get; set; }
        [Display(Name = "抽中考题")]
        public string QuestionId { get; set; }
        [Display(Name = "正确答案")]
        public string QuestionAnswer { get; set; }
        [Display(Name = "考生答案")]
        public string ExamineeAnswers { get; set; }
        [Display(Name = "手机号码")]
        public string Phone { get; set; }


    }
}
