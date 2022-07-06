using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.RecordNoAccountVMs
{
    public partial class RecordNoAccountTemplateVM : BaseTemplateVM
    {
        [Display(Name = "参与试卷")]
        public ExcelPropety ExaminationSetup_Excel = ExcelPropety.CreateProperty<RecordNoAccount>(x => x.ExaminationSetupID);
        [Display(Name = "参与时间")]
        public ExcelPropety ParticipationTime_Excel = ExcelPropety.CreateProperty<RecordNoAccount>(x => x.ParticipationTime);
        [Display(Name = "参与人")]
        public ExcelPropety UserName_Excel = ExcelPropety.CreateProperty<RecordNoAccount>(x => x.UserName);
        [Display(Name = "所在单位")]
        public ExcelPropety UnitWork_Excel = ExcelPropety.CreateProperty<RecordNoAccount>(x => x.UnitWork);
        [Display(Name = "考试成绩")]
        public ExcelPropety Achievement_Excel = ExcelPropety.CreateProperty<RecordNoAccount>(x => x.Achievement);
        [Display(Name = "抽中考题")]
        public ExcelPropety QuestionId_Excel = ExcelPropety.CreateProperty<RecordNoAccount>(x => x.QuestionId);
        [Display(Name = "正确答案")]
        public ExcelPropety QuestionAnswer_Excel = ExcelPropety.CreateProperty<RecordNoAccount>(x => x.QuestionAnswer);
        [Display(Name = "考生答案")]
        public ExcelPropety ExamineeAnswers_Excel = ExcelPropety.CreateProperty<RecordNoAccount>(x => x.ExamineeAnswers);
        [Display(Name = "手机号码")]
        public ExcelPropety Phone_Excel = ExcelPropety.CreateProperty<RecordNoAccount>(x => x.Phone);

	    protected override void InitVM()
        {
            ExaminationSetup_Excel.DataType = ColumnDataType.ComboBox;
            ExaminationSetup_Excel.ListItems = DC.Set<ExaminationSetup>().GetSelectListItems(Wtm, y => y.CreateBy);
        }

    }

    public class RecordNoAccountImportVM : BaseImportVM<RecordNoAccountTemplateVM, RecordNoAccount>
    {

    }

}
