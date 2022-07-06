using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.RecordNoAccountVMs
{
    public partial class RecordNoAccountListVM : BasePagedListVM<RecordNoAccount_View, RecordNoAccountSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("RecordNoAccount", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800),
                this.MakeStandardAction("RecordNoAccount", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("RecordNoAccount", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("RecordNoAccount", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("RecordNoAccount", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("RecordNoAccount", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("RecordNoAccount", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("RecordNoAccount", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<RecordNoAccount_View>> InitGridHeader()
        {
            return new List<GridColumn<RecordNoAccount_View>>{
                this.MakeGridHeader(x => x.CreateBy_view),
                this.MakeGridHeader(x => x.ParticipationTime),
                this.MakeGridHeader(x => x.UserName),
                this.MakeGridHeader(x => x.UnitWork),
                this.MakeGridHeader(x => x.Achievement),
                this.MakeGridHeader(x => x.QuestionId),
                this.MakeGridHeader(x => x.QuestionAnswer),
                this.MakeGridHeader(x => x.ExamineeAnswers),
                this.MakeGridHeader(x => x.Phone),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<RecordNoAccount_View> GetSearchQuery()
        {
            var query = DC.Set<RecordNoAccount>()
                .CheckContain(Searcher.UnitWork, x=>x.UnitWork)
                .CheckContain(Searcher.Phone, x=>x.Phone)
                .Select(x => new RecordNoAccount_View
                {
				    ID = x.ID,
                    CreateBy_view = x.ExaminationSetup.CreateBy,
                    ParticipationTime = x.ParticipationTime,
                    UserName = x.UserName,
                    UnitWork = x.UnitWork,
                    Achievement = x.Achievement,
                    QuestionId = x.QuestionId,
                    QuestionAnswer = x.QuestionAnswer,
                    ExamineeAnswers = x.ExamineeAnswers,
                    Phone = x.Phone,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class RecordNoAccount_View : RecordNoAccount{
        [Display(Name = "_Admin.CreateBy")]
        public String CreateBy_view { get; set; }

    }
}
