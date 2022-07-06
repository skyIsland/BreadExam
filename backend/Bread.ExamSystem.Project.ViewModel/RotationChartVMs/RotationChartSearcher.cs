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
    public partial class RotationChartSearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AllExaminationSetups { get; set; }
        [Display(Name = "举办活动")]
        public int? ExaminationSetupID { get; set; }

        protected override void InitVM()
        {
            AllExaminationSetups = DC.Set<ExaminationSetup>().GetSelectListItems(Wtm, y => y.CreateBy);
        }

    }
}
