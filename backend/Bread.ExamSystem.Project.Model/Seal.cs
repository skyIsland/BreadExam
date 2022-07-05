using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace Bread.ExamSystem.Project.Model
{
    /// <summary>
    /// 印章
    /// </summary>
    public class Seal : BasePoco
    {
        [Display(Name = "单位名称")]
        [Required(ErrorMessage = "单位名称是必填项")]
        public string Name { get; set; }
        [Display(Name = "证件类型")]
        [Required(ErrorMessage = "证件类型是必填项")]
        public string SType { get; set; }
        [Display(Name = "证件图")]
        public Guid? PhotoId { get; set; }
        [Display(Name = "证件图")]
        public FileAttachment Photo { get; set; }
    }
}
