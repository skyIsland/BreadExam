using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.UnitWorkVMs
{
    public partial class UnitWorkListVM : BasePagedListVM<UnitWork_View, UnitWorkSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("UnitWork", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800),
                this.MakeStandardAction("UnitWork", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("UnitWork", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("UnitWork", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("UnitWork", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("UnitWork", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("UnitWork", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("UnitWork", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<UnitWork_View>> InitGridHeader()
        {
            return new List<GridColumn<UnitWork_View>>{
                this.MakeGridHeader(x => x.UnitWorkName),
                this.MakeGridHeader(x => x.UnitWorkName_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<UnitWork_View> GetSearchQuery()
        {
            var query = DC.Set<UnitWork>()
                .Select(x => new UnitWork_View
                {
				    ID = x.ID,
                    UnitWorkName = x.UnitWorkName,
                    UnitWorkName_view = x.Parent.UnitWorkName,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class UnitWork_View : UnitWork{
        [Display(Name = "上级部门")]
        public String UnitWorkName_view { get; set; }

    }
}
