using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.QuestionTypeVMs
{
    public partial class QuestionTypeBatchVM : BaseBatchVM<QuestionType, QuestionType_BatchEdit>
    {
        public QuestionTypeBatchVM()
        {
            ListVM = new QuestionTypeListVM();
            LinkedVM = new QuestionType_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class QuestionType_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
