using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.QuestionVMs
{
    public partial class QuestionBatchVM : BaseBatchVM<Question, Question_BatchEdit>
    {
        public QuestionBatchVM()
        {
            ListVM = new QuestionListVM();
            LinkedVM = new Question_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Question_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
