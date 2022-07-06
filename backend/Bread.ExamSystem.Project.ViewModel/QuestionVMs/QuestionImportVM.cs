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
    public partial class QuestionTemplateVM : BaseTemplateVM
    {
        [Display(Name = "题干")]
        public ExcelPropety QuestionText_Excel = ExcelPropety.CreateProperty<Question>(x => x.QuestionText);
        [Display(Name = "答案")]
        public ExcelPropety Anwser_Excel = ExcelPropety.CreateProperty<Question>(x => x.Anwser);
        [Display(Name = "A选项")]
        public ExcelPropety OptionA_Excel = ExcelPropety.CreateProperty<Question>(x => x.OptionA);
        [Display(Name = "B选项")]
        public ExcelPropety OptionB_Excel = ExcelPropety.CreateProperty<Question>(x => x.OptionB);
        [Display(Name = "C选项")]
        public ExcelPropety OptionC_Excel = ExcelPropety.CreateProperty<Question>(x => x.OptionC);
        [Display(Name = "D选项")]
        public ExcelPropety OptionD_Excel = ExcelPropety.CreateProperty<Question>(x => x.OptionD);
        [Display(Name = "题型")]
        public ExcelPropety QuestionType_Excel = ExcelPropety.CreateProperty<Question>(x => x.QuestionTypeID);
        [Display(Name = "科目")]
        public ExcelPropety Subject_Excel = ExcelPropety.CreateProperty<Question>(x => x.Subject);
        [Display(Name = "题目解析")]
        public ExcelPropety Pars_Excel = ExcelPropety.CreateProperty<Question>(x => x.Pars);

	    protected override void InitVM()
        {
            QuestionType_Excel.DataType = ColumnDataType.ComboBox;
            QuestionType_Excel.ListItems = DC.Set<QuestionType>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class QuestionImportVM : BaseImportVM<QuestionTemplateVM, Question>
    {

    }

}
