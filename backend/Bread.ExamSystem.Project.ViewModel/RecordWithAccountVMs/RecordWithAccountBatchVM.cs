using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.RecordWithAccountVMs
{
    public partial class RecordWithAccountBatchVM : BaseBatchVM<RecordWithAccount, RecordWithAccount_BatchEdit>
    {
        public RecordWithAccountBatchVM()
        {
            ListVM = new RecordWithAccountListVM();
            LinkedVM = new RecordWithAccount_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class RecordWithAccount_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
