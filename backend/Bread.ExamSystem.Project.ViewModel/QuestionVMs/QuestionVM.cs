using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.QuestionVMs
{
    public partial class QuestionVM : BaseCRUDVM<Question>
    {
        public List<ComboSelectListItem> AllQuestionTypes { get; set; }

        public QuestionVM()
        {
            SetInclude(x => x.QuestionType);
        }

        protected override void InitVM()
        {
            AllQuestionTypes = DC.Set<QuestionType>().GetSelectListItems(Wtm, y => y.Name);
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
