using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.WrongQuestionBankVMs
{
    public partial class WrongQuestionBankTemplateVM : BaseTemplateVM
    {
        [Display(Name = "题目标识")]
        public ExcelPropety QuestionID_Excel = ExcelPropety.CreateProperty<WrongQuestionBank>(x => x.QuestionID);
        [Display(Name = "考试用户")]
        public ExcelPropety FrameworkUser_Excel = ExcelPropety.CreateProperty<WrongQuestionBank>(x => x.FrameworkUserID);
        [Display(Name = "做错次数")]
        public ExcelPropety WrongNumber_Excel = ExcelPropety.CreateProperty<WrongQuestionBank>(x => x.WrongNumber);

	    protected override void InitVM()
        {
            FrameworkUser_Excel.DataType = ColumnDataType.ComboBox;
            FrameworkUser_Excel.ListItems = DC.Set<FrameworkUser>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class WrongQuestionBankImportVM : BaseImportVM<WrongQuestionBankTemplateVM, WrongQuestionBank>
    {

    }

}
