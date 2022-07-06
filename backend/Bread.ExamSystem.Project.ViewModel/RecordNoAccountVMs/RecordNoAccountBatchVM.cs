using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.RecordNoAccountVMs
{
    public partial class RecordNoAccountBatchVM : BaseBatchVM<RecordNoAccount, RecordNoAccount_BatchEdit>
    {
        public RecordNoAccountBatchVM()
        {
            ListVM = new RecordNoAccountListVM();
            LinkedVM = new RecordNoAccount_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class RecordNoAccount_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
