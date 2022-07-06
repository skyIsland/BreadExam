using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.Model.FrameworkEnumeration;


namespace Bread.ExamSystem.Project.ViewModel.ExaminationSetupVMs
{
    public partial class ExaminationSetupListVM : BasePagedListVM<ExaminationSetup_View, ExaminationSetupSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("ExaminationSetup", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800),
                this.MakeStandardAction("ExaminationSetup", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("ExaminationSetup", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("ExaminationSetup", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("ExaminationSetup", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("ExaminationSetup", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("ExaminationSetup", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("ExaminationSetup", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<ExaminationSetup_View>> InitGridHeader()
        {
            return new List<GridColumn<ExaminationSetup_View>>{
                this.MakeGridHeader(x => x.Title),
                this.MakeGridHeader(x => x.StrTime),
                this.MakeGridHeader(x => x.EndTime),
                this.MakeGridHeader(x => x.CourseEnum),
                this.MakeGridHeader(x => x.DXNumer),
                this.MakeGridHeader(x => x.DXScore),
                this.MakeGridHeader(x => x.DSXNumer),
                this.MakeGridHeader(x => x.DSXScore),
                this.MakeGridHeader(x => x.PDNumer),
                this.MakeGridHeader(x => x.PDScore),
                this.MakeGridHeader(x => x.TestTime),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.ParticipationTypes),
                this.MakeGridHeader(x => x.Subject),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<ExaminationSetup_View> GetSearchQuery()
        {
            var query = DC.Set<ExaminationSetup>()
                .CheckContain(Searcher.Title, x=>x.Title)
                .CheckBetween(Searcher.StrTime?.GetStartTime(), Searcher.StrTime?.GetEndTime(), x => x.StrTime, includeMax: false)
                .CheckEqual(Searcher.ParticipationTypes, x=>x.ParticipationTypes)
                .Select(x => new ExaminationSetup_View
                {
				    ID = x.ID,
                    Title = x.Title,
                    StrTime = x.StrTime,
                    EndTime = x.EndTime,
                    CourseEnum = x.CourseEnum,
                    DXNumer = x.DXNumer,
                    DXScore = x.DXScore,
                    DSXNumer = x.DSXNumer,
                    DSXScore = x.DSXScore,
                    PDNumer = x.PDNumer,
                    PDScore = x.PDScore,
                    TestTime = x.TestTime,
                    Name_view = x.Seal.Name,
                    ParticipationTypes = x.ParticipationTypes,
                    Subject = x.Subject,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class ExaminationSetup_View : ExaminationSetup{
        [Display(Name = "单位名称")]
        public String Name_view { get; set; }

    }
}
