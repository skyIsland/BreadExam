using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace Bread.ExamSystem.Project.Model
{
    /// <summary>
    /// 单位
    /// </summary>
    public class UnitWork : TreePoco<UnitWork>
    {
        [Display(Name = "部门名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        public string UnitWorkName { get; set; }
    }
}
