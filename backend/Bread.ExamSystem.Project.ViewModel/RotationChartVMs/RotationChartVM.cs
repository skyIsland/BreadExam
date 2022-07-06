using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.RotationChartVMs
{
    public partial class RotationChartVM : BaseCRUDVM<RotationChart>
    {
        public List<ComboSelectListItem> AllExaminationSetups { get; set; }

        public RotationChartVM()
        {
            SetInclude(x => x.ExaminationSetup);
        }

        protected override void InitVM()
        {
            AllExaminationSetups = DC.Set<ExaminationSetup>().GetSelectListItems(Wtm, y => y.CreateBy);
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
