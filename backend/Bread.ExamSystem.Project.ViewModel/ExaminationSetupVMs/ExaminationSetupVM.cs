using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.ExaminationSetupVMs
{
    public partial class ExaminationSetupVM : BaseCRUDVM<ExaminationSetup>
    {
        public List<ComboSelectListItem> AllSeals { get; set; }

        public ExaminationSetupVM()
        {
            SetInclude(x => x.Seal);
        }

        protected override void InitVM()
        {
            AllSeals = DC.Set<Seal>().GetSelectListItems(Wtm, y => y.Name);
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
