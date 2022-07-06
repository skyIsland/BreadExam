using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.RecordWithAccountVMs
{
    public partial class RecordWithAccountVM : BaseCRUDVM<RecordWithAccount>
    {
        public List<ComboSelectListItem> AllExaminationSetups { get; set; }
        public List<ComboSelectListItem> AllFrameworkUsers { get; set; }

        public RecordWithAccountVM()
        {
            SetInclude(x => x.ExaminationSetup);
            SetInclude(x => x.FrameworkUser);
        }

        protected override void InitVM()
        {
            AllExaminationSetups = DC.Set<ExaminationSetup>().GetSelectListItems(Wtm, y => y.CreateBy);
            AllFrameworkUsers = DC.Set<FrameworkUser>().GetSelectListItems(Wtm, y => y.Name);
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
