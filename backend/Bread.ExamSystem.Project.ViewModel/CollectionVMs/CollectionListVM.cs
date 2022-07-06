using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.CollectionVMs
{
    public partial class CollectionListVM : BasePagedListVM<Collection_View, CollectionSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Collection", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800),
                this.MakeStandardAction("Collection", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("Collection", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("Collection", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("Collection", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("Collection", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("Collection", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("Collection", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<Collection_View>> InitGridHeader()
        {
            return new List<GridColumn<Collection_View>>{
                this.MakeGridHeader(x => x.Anwser_view),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Collection_View> GetSearchQuery()
        {
            var query = DC.Set<Collection>()
                .Select(x => new Collection_View
                {
				    ID = x.ID,
                    Anwser_view = x.Question.Anwser,
                    Name_view = x.FrameworkUser.Name,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Collection_View : Collection{
        [Display(Name = "答案")]
        public String Anwser_view { get; set; }
        [Display(Name = "_Admin.Name")]
        public String Name_view { get; set; }

    }
}
