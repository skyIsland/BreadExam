using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.CollectionVMs
{
    public partial class CollectionVM : BaseCRUDVM<Collection>
    {
        public List<ComboSelectListItem> AllQuestions { get; set; }
        public List<ComboSelectListItem> AllFrameworkUsers { get; set; }

        public CollectionVM()
        {
            SetInclude(x => x.Question);
            SetInclude(x => x.FrameworkUser);
        }

        protected override void InitVM()
        {
            AllQuestions = DC.Set<Question>().GetSelectListItems(Wtm, y => y.Anwser);
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
