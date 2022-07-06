using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.QuestionTypeVMs
{
    public partial class QuestionTypeTemplateVM : BaseTemplateVM
    {
        [Display(Name = "题型")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<QuestionType>(x => x.Name);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<QuestionType>(x => x.Remark);

	    protected override void InitVM()
        {
        }

    }

    public class QuestionTypeImportVM : BaseImportVM<QuestionTypeTemplateVM, QuestionType>
    {

    }

}
