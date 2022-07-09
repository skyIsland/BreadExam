using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bread.ExamSystem.Project.ViewModel.API.TestQuestionsVMs
{
    public class Fractiondto
    {
        [Required(ErrorMessage = "参加活动是未填写")]
        public int Acid { get; set; }
        [Required(ErrorMessage = "参加活动是未填写")]
        public int Jlid { get; set; }
        [Required(ErrorMessage = "参加活动是未填写")]
        public string[] ExamineeAnswers { get; set; }
        [Required(ErrorMessage = "考试类型")]
        public string Type { get; set; }

    }
}
