using System;
using System.Collections.Generic;
using System.Text;

namespace Bread.ExamSystem.Project.Model.Dto
{
    public class WrongQuestionRecord
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }

        public int WrongNumber { get; set; }
    }
}
