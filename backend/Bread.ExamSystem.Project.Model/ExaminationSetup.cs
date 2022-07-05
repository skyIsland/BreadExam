using Bread.ExamSystem.Project.Model.FrameworkEnumeration;
using System;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;

namespace Bread.ExamSystem.Project.Model
{
    /// <summary>
    /// 考试设置
    /// </summary>
    public class ExaminationSetup : BasePoco
    {
        public new int ID { get; set; }
        [Display(Name = "试卷名称")]
        [Required(ErrorMessage = "试卷名称是必填项")]
        public string Title { get; set; }
        [Display(Name = "开始时间")]
        [Required(ErrorMessage = "开始时间是必填项")]
        public DateTime StrTime { get; set; }
        [Display(Name = "结束时间")]
        [Required(ErrorMessage = "结束时间是必填项")]
        public DateTime EndTime { get; set; }
        [Display(Name = "是否可用")]
        public CourseEnum CourseEnum { get; set; }
        [Display(Name = "单选题数量")]
        [Required(ErrorMessage = "单选题数量是必填项")]
        public int DXNumer { get; set; }
        [Display(Name = "单选1题得分")]
        [Required(ErrorMessage = "单选题得分是必填项")]
        public double DXScore { get; set; }
        [Display(Name = "多选题数量")]
        [Required(ErrorMessage = "多选题数量是必填项")]
        public int DSXNumer { get; set; }
        [Display(Name = "多选1题得分")]
        [Required(ErrorMessage = "多选题得分是必填项")]
        public double DSXScore { get; set; }
        [Display(Name = "判断题数量")]
        [Required(ErrorMessage = "判断题数量是必填项")]
        public int PDNumer { get; set; }
        [Display(Name = "判断1题得分")]
        [Required(ErrorMessage = "判断题得分是必填项")]
        public double PDScore { get; set; }
        [Display(Name = "考试时间(分钟)")]
        [Required(ErrorMessage = "考试时间是必填项")]
        public int TestTime { get; set; }
        [Display(Name = "结业证件")]
        [Required(ErrorMessage = "考结业证件是必填项")]
        public Guid? SealId { get; set; }
        [Display(Name = "结业证件")]
        public Seal Seal { get; set; }
        [Display(Name = "是否登录")]
        [Required(ErrorMessage = "是否登录")]
        public ParticipationTypes ParticipationTypes { get; set; }
        [Display(Name = "科目")]
        [Required(ErrorMessage = "科目是必填项")]
        public string Subject { get; set; }
    }
}
