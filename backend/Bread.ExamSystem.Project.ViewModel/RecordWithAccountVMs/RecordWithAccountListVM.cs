using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.RecordWithAccountVMs
{
    public partial class RecordWithAccountListVM : BasePagedListVM<RecordWithAccount_View, RecordWithAccountSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("RecordWithAccount", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800),
                this.MakeStandardAction("RecordWithAccount", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("RecordWithAccount", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("RecordWithAccount", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("RecordWithAccount", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("RecordWithAccount", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("RecordWithAccount", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("RecordWithAccount", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<RecordWithAccount_View>> InitGridHeader()
        {
            return new List<GridColumn<RecordWithAccount_View>>{
                this.MakeGridHeader(x => x.CreateBy_view),
                this.MakeGridHeader(x => x.ParticipationTime),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.Achievement),
                this.MakeGridHeader(x => x.QuestionId),
                this.MakeGridHeader(x => x.QuestionAnswer),
                this.MakeGridHeader(x => x.ExamineeAnswers),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<RecordWithAccount_View> GetSearchQuery()
        {
            var query = DC.Set<RecordWithAccount>()
                .CheckBetween(Searcher.ParticipationTime?.GetStartTime(), Searcher.ParticipationTime?.GetEndTime(), x => x.ParticipationTime, includeMax: false)
                .Select(x => new RecordWithAccount_View
                {
				    ID = x.ID,
                    CreateBy_view = x.ExaminationSetup.CreateBy,
                    ParticipationTime = x.ParticipationTime,
                    Name_view = x.FrameworkUser.Name,
                    Achievement = x.Achievement,
                    QuestionId = x.QuestionId,
                    QuestionAnswer = x.QuestionAnswer,
                    ExamineeAnswers = x.ExamineeAnswers,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class RecordWithAccount_View : RecordWithAccount{
        [Display(Name = "_Admin.CreateBy")]
        public String CreateBy_view { get; set; }
        [Display(Name = "_Admin.Name")]
        public String Name_view { get; set; }

    }
}
