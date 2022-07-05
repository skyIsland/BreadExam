using System;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;

namespace Bread.ExamSystem.Project.Model
{
    /// <summary>
    /// 收藏
    /// </summary>
    public class Collection : BasePoco
    {
        public new int ID { get; set; }
        [Display(Name = "收藏题目")]
        [Required(ErrorMessage = "收藏题目")]
        public  int QuestionID { get; set; }
        [Display(Name = "收藏题目")]
        public Question Question { get; set; }
        [Display(Name = "用户")]
        [Required(ErrorMessage = "用户是必须填写")]
        public Guid? WebUserID { get; set; }
        [Display(Name = "用户")]
        public WebUser WebUser { get; set; }
        
    }
}
