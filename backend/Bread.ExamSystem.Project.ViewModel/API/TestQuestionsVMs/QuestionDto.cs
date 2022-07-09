using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bread.ExamSystem.Project.ViewModel.API.TestQuestionsVMs
{
    public class QuestionDto
    {
        public int ID { get; set; }      
        public string QuestionText { get; set; }      
        public string Anwser { get; set; }
        public string OptionA { get; set; }        
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }     
        public string QuestionType { get; set; }     
        public string Subject { get; set; }
        public string Pars { get; set; }
    }
}
