using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace Bread.ExamSystem.Project.Model
{
    /// <summary>
    /// 登录账号参与考试记录
    /// </summary>
    public class RecordWithAccount : BasePoco
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
        [Display(Name = "考试用户")]
        [Required(ErrorMessage = "考试用户是必须填写")]
        public Guid? FrameworkUserID { get; set; }
        [Display(Name = "考试用户")]
        public FrameworkUser FrameworkUser { get; set; }
        [Display(Name = "考试成绩")]
        [Required(ErrorMessage = "考试成绩是必须填写")]
        public double Achievement { get; set; }
        [Display(Name = "抽中考题")]
        public string QuestionId { get; set; }
        [Display(Name = "正确答案")]
        public string QuestionAnswer { get; set; }
        [Display(Name = "考生答案")]
        public string ExamineeAnswers { get; set; }

    }
}
