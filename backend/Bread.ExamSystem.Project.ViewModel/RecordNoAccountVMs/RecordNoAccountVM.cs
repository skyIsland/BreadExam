using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.RecordNoAccountVMs
{
    public partial class RecordNoAccountVM : BaseCRUDVM<RecordNoAccount>
    {
        public List<ComboSelectListItem> AllExaminationSetups { get; set; }

        public RecordNoAccountVM()
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
