using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bread.ExamSystem.Project.ViewModel.API.ExaminationSetupNoAcVMs
{
    public class SingUpDto
    {
        [Required(ErrorMessage = "参加活动是未填写")]
        public int ExaminationSetupID { get; set; }
        [Required(ErrorMessage = "用户名未填写")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "单位名未填写")]
        public string UnitWork { get; set; }
        [Required(ErrorMessage = "手机号码")]
        public string Phone { get; set; }
    }
}
