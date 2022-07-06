using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.SealVMs
{
    public partial class SealBatchVM : BaseBatchVM<Seal, Seal_BatchEdit>
    {
        public SealBatchVM()
        {
            ListVM = new SealListVM();
            LinkedVM = new Seal_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Seal_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
