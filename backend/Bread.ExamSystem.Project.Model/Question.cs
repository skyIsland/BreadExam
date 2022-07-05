using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace Bread.ExamSystem.Project.Model
{
    /// <summary>
    /// 题库
    /// </summary>
    public class Question : BasePoco
    {
        public new int ID { get; set; }
        [Display(Name = "题干")]
        [Required(ErrorMessage = "题干是必填项")]
        public string QuestionText { get; set; }
        [Display(Name = "答案")]
        [Required(ErrorMessage = "答案是必填项")]
        public string Anwser { get; set; }
        [Display(Name = "A选项")]
        [Required(ErrorMessage = "A选项是必填项")]
        public string OptionA { get; set; }
        [Display(Name = "B选项")]
        [Required(ErrorMessage = "B选项是必填项")]
        public string OptionB { get; set; }
        [Display(Name = "C选项")]
        public string OptionC { get; set; }
        [Display(Name = "D选项")]
        public string OptionD { get; set; }
        [Display(Name = "题型")]
        [Required(ErrorMessage = "题型是必填项")]
        public int QuestionTypeID { get; set; }
        [Display(Name = "题型")]
        public QuestionType QuestionType { get; set; }
        [Display(Name = "科目")]
        [Required(ErrorMessage = "科目是必填项")]
        public string Subject { get; set; }
        [Display(Name = "题目解析")]
        public string Pars { get; set; }
    }
}
