using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace Bread.ExamSystem.Project.Model
{
    /// <summary>
    /// 登录用户
    /// </summary>
    [Table("FrameworkUsers")]
    public class WebUser : FrameworkUserBase
    {
        [Display(Name = "部门名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        public Guid UnitWorkID { get; set; }
        [Display(Name = "部门名称")]
        public UnitWork UnitWork { get; set; }
        /// <summary>
        /// 外部登录
        /// </summary>
        [Display(Name = "外部登录")]
        public string OpenID { get; set; }
    }
}
