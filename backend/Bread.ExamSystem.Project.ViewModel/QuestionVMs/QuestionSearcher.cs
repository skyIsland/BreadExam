using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.QuestionVMs
{
    public partial class QuestionSearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AllQuestionTypes { get; set; }
        [Display(Name = "题型")]
        public int? QuestionTypeID { get; set; }
        [Display(Name = "科目")]
        public String Subject { get; set; }

        protected override void InitVM()
        {
            AllQuestionTypes = DC.Set<QuestionType>().GetSelectListItems(Wtm, y => y.Name);
        }

    }
}
