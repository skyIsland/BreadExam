using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace Bread.ExamSystem.Project.Model
{
    /// <summary>
    /// 轮播图设置
    /// </summary>
    public class RotationChart : BasePoco
    {
        public new int ID { get; set; }
        [Display(Name = "轮播图")]
        public Guid? PhotoId { get; set; }
        [Display(Name = "轮播图")]
        public FileAttachment Photo { get; set; }
        [Display(Name = "举办活动")]
        [Required(ErrorMessage = "举办活动是必填项")]
        public int ExaminationSetupID { get; set; }
        [Display(Name = "举办活动")]
        public ExaminationSetup ExaminationSetup { get; set; }
    }
}
