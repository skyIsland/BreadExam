using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace Bread.ExamSystem.Project.Model
{
    /// <summary>
    /// 题型
    /// </summary>
    public class QuestionType : BasePoco
    {
        public new int ID { get; set; }
        [Display(Name = "题型")]
        [Required(ErrorMessage = "题型")]
        public string Name { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }

    }
}
