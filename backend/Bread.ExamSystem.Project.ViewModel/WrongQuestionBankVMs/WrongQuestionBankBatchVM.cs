using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.WrongQuestionBankVMs
{
    public partial class WrongQuestionBankBatchVM : BaseBatchVM<WrongQuestionBank, WrongQuestionBank_BatchEdit>
    {
        public WrongQuestionBankBatchVM()
        {
            ListVM = new WrongQuestionBankListVM();
            LinkedVM = new WrongQuestionBank_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WrongQuestionBank_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
