using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bread.ExamSystem.Project.Model.Dto
{
    public class ChangePwdDto
    {
        [Required(ErrorMessage = "旧密码不能为空")]
        public string PwdOld { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        public string PwdNew { get; set; }
        [Required(ErrorMessage = "确认密码不能为空")]
        public string PwdNewC { get; set; }
    }
}
