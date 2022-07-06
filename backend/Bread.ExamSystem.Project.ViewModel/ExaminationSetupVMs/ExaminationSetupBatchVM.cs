using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.ExaminationSetupVMs
{
    public partial class ExaminationSetupBatchVM : BaseBatchVM<ExaminationSetup, ExaminationSetup_BatchEdit>
    {
        public ExaminationSetupBatchVM()
        {
            ListVM = new ExaminationSetupListVM();
            LinkedVM = new ExaminationSetup_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class ExaminationSetup_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
