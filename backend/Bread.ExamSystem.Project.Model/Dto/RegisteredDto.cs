using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bread.ExamSystem.Project.Model.Dto
{
    public class RegisteredDto
    {
        
        [Required(ErrorMessage = "账号不能为空")]
        [StringLength(50, ErrorMessage = "{0}stringmax{1}")]
        public string ITCode { get; set; }
        [Required(ErrorMessage = "姓名不能为空")]       
        public string UserName { get; set; }
        [Required(ErrorMessage = "密码不能为空")]     
        public string Pwd { get; set; }
        [Required(ErrorMessage = "确认密码不能为空")]
        public string PwdC { get; set; }
        [Required(ErrorMessage = "单位不能为空")]
        public string UnitWork { get; set; }

    }
}
