using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.RotationChartVMs
{
    public partial class RotationChartBatchVM : BaseBatchVM<RotationChart, RotationChart_BatchEdit>
    {
        public RotationChartBatchVM()
        {
            ListVM = new RotationChartListVM();
            LinkedVM = new RotationChart_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class RotationChart_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
