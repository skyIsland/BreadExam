using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.RecordWithAccountVMs
{
    public partial class RecordWithAccountTemplateVM : BaseTemplateVM
    {
        [Display(Name = "参与试卷")]
        public ExcelPropety ExaminationSetup_Excel = ExcelPropety.CreateProperty<RecordWithAccount>(x => x.ExaminationSetupID);
        [Display(Name = "参与时间")]
        public ExcelPropety ParticipationTime_Excel = ExcelPropety.CreateProperty<RecordWithAccount>(x => x.ParticipationTime);
        [Display(Name = "考试用户")]
        public ExcelPropety FrameworkUser_Excel = ExcelPropety.CreateProperty<RecordWithAccount>(x => x.FrameworkUserID);
        [Display(Name = "考试成绩")]
        public ExcelPropety Achievement_Excel = ExcelPropety.CreateProperty<RecordWithAccount>(x => x.Achievement);
        [Display(Name = "抽中考题")]
        public ExcelPropety QuestionId_Excel = ExcelPropety.CreateProperty<RecordWithAccount>(x => x.QuestionId);
        [Display(Name = "正确答案")]
        public ExcelPropety QuestionAnswer_Excel = ExcelPropety.CreateProperty<RecordWithAccount>(x => x.QuestionAnswer);
        [Display(Name = "考生答案")]
        public ExcelPropety ExamineeAnswers_Excel = ExcelPropety.CreateProperty<RecordWithAccount>(x => x.ExamineeAnswers);

	    protected override void InitVM()
        {
            ExaminationSetup_Excel.DataType = ColumnDataType.ComboBox;
            ExaminationSetup_Excel.ListItems = DC.Set<ExaminationSetup>().GetSelectListItems(Wtm, y => y.CreateBy);
            FrameworkUser_Excel.DataType = ColumnDataType.ComboBox;
            FrameworkUser_Excel.ListItems = DC.Set<FrameworkUser>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class RecordWithAccountImportVM : BaseImportVM<RecordWithAccountTemplateVM, RecordWithAccount>
    {

    }

}
