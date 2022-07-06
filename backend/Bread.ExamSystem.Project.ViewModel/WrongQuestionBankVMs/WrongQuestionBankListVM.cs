using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.WrongQuestionBankVMs
{
    public partial class WrongQuestionBankListVM : BasePagedListVM<WrongQuestionBank_View, WrongQuestionBankSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("WrongQuestionBank", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800),
                this.MakeStandardAction("WrongQuestionBank", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("WrongQuestionBank", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("WrongQuestionBank", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("WrongQuestionBank", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("WrongQuestionBank", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("WrongQuestionBank", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("WrongQuestionBank", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<WrongQuestionBank_View>> InitGridHeader()
        {
            return new List<GridColumn<WrongQuestionBank_View>>{
                this.MakeGridHeader(x => x.QuestionID),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.WrongNumber),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WrongQuestionBank_View> GetSearchQuery()
        {
            var query = DC.Set<WrongQuestionBank>()
                .Select(x => new WrongQuestionBank_View
                {
				    ID = x.ID,
                    QuestionID = x.QuestionID,
                    Name_view = x.FrameworkUser.Name,
                    WrongNumber = x.WrongNumber,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WrongQuestionBank_View : WrongQuestionBank{
        [Display(Name = "_Admin.Name")]
        public String Name_view { get; set; }

    }
}
