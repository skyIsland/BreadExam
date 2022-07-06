using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.ViewModel.SealVMs;
using Microsoft.EntityFrameworkCore;

namespace Bread.ExamSystem.Project.ViewModel.ExaminationSetupVMs
{
    public partial class ExaminationSetupVM : BaseCRUDVM<ExaminationSetup>
    {
        //public List<ComboSelectListItem> AllSeals { get; set; }

        public List<ComboSelectListItem> SubjectSelect { get; set; }

        public SealListVM Seals { get; set; }

        public ExaminationSetupVM()
        {
            SetInclude(x => x.Seal);
        }

        protected override void InitVM()
        {
            Seals = new SealListVM();
            Seals.CopyContext(this);

            var Subjects = DC.Set<Question>().Select(x => x.Subject).Distinct().AsNoTracking();
            List<ComboSelectListItem> rv1 = new List<ComboSelectListItem>();
            foreach (var item in Subjects)
            {
                rv1.Add(new ComboSelectListItem { Text = item, Value = item });
            }
            SubjectSelect = rv1;
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
