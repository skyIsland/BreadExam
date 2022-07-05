using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace Bread.ExamSystem.Project.Model
{
    /// <summary>
    /// 错题库
    /// </summary>
    public class WrongQuestionBank: BasePoco
    {
        public new int ID { get; set; }
        [Display(Name = "题目标识")]
        [Required(ErrorMessage = "题目标识")]
        public int QuestionID { get; set; }
        [Display(Name = "考试用户")]
        [Required(ErrorMessage = "考试用户是必须填写")]
        public Guid? WebUserID { get; set; }
        [Display(Name = "考试用户")]
        public WebUser WebUser { get; set; }
        [Display(Name = "做错次数")]
        public int WrongNumber { get; set; }

    }
}
