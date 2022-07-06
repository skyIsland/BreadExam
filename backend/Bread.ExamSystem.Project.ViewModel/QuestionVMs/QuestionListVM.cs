using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.QuestionVMs
{
    public partial class QuestionListVM : BasePagedListVM<Question_View, QuestionSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Question", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800),
                this.MakeStandardAction("Question", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("Question", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("Question", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("Question", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("Question", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("Question", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("Question", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<Question_View>> InitGridHeader()
        {
            return new List<GridColumn<Question_View>>{
                this.MakeGridHeader(x => x.QuestionText),
                this.MakeGridHeader(x => x.Anwser),
                this.MakeGridHeader(x => x.OptionA),
                this.MakeGridHeader(x => x.OptionB),
                this.MakeGridHeader(x => x.OptionC),
                this.MakeGridHeader(x => x.OptionD),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.Subject),
                this.MakeGridHeader(x => x.Pars),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Question_View> GetSearchQuery()
        {
            var query = DC.Set<Question>()
                .CheckEqual(Searcher.QuestionTypeID, x=>x.QuestionTypeID)
                .CheckContain(Searcher.Subject, x=>x.Subject)
                .Select(x => new Question_View
                {
				    ID = x.ID,
                    QuestionText = x.QuestionText,
                    Anwser = x.Anwser,
                    OptionA = x.OptionA,
                    OptionB = x.OptionB,
                    OptionC = x.OptionC,
                    OptionD = x.OptionD,
                    Name_view = x.QuestionType.Name,
                    Subject = x.Subject,
                    Pars = x.Pars,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Question_View : Question{
        [Display(Name = "题型")]
        public String Name_view { get; set; }

    }
}
