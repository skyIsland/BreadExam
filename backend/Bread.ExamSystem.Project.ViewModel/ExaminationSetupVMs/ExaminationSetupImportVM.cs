using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.Model.FrameworkEnumeration;


namespace Bread.ExamSystem.Project.ViewModel.ExaminationSetupVMs
{
    public partial class ExaminationSetupTemplateVM : BaseTemplateVM
    {
        [Display(Name = "试卷名称")]
        public ExcelPropety Title_Excel = ExcelPropety.CreateProperty<ExaminationSetup>(x => x.Title);
        [Display(Name = "开始时间")]
        public ExcelPropety StrTime_Excel = ExcelPropety.CreateProperty<ExaminationSetup>(x => x.StrTime);
        [Display(Name = "结束时间")]
        public ExcelPropety EndTime_Excel = ExcelPropety.CreateProperty<ExaminationSetup>(x => x.EndTime);
        [Display(Name = "是否可用")]
        public ExcelPropety CourseEnum_Excel = ExcelPropety.CreateProperty<ExaminationSetup>(x => x.CourseEnum);
        [Display(Name = "单选题数量")]
        public ExcelPropety DXNumer_Excel = ExcelPropety.CreateProperty<ExaminationSetup>(x => x.DXNumer);
        [Display(Name = "单选1题得分")]
        public ExcelPropety DXScore_Excel = ExcelPropety.CreateProperty<ExaminationSetup>(x => x.DXScore);
        [Display(Name = "多选题数量")]
        public ExcelPropety DSXNumer_Excel = ExcelPropety.CreateProperty<ExaminationSetup>(x => x.DSXNumer);
        [Display(Name = "多选1题得分")]
        public ExcelPropety DSXScore_Excel = ExcelPropety.CreateProperty<ExaminationSetup>(x => x.DSXScore);
        [Display(Name = "判断题数量")]
        public ExcelPropety PDNumer_Excel = ExcelPropety.CreateProperty<ExaminationSetup>(x => x.PDNumer);
        [Display(Name = "判断1题得分")]
        public ExcelPropety PDScore_Excel = ExcelPropety.CreateProperty<ExaminationSetup>(x => x.PDScore);
        [Display(Name = "考试时间(分钟)")]
        public ExcelPropety TestTime_Excel = ExcelPropety.CreateProperty<ExaminationSetup>(x => x.TestTime);
        [Display(Name = "结业证件")]
        public ExcelPropety Seal_Excel = ExcelPropety.CreateProperty<ExaminationSetup>(x => x.SealId);
        [Display(Name = "是否登录")]
        public ExcelPropety ParticipationTypes_Excel = ExcelPropety.CreateProperty<ExaminationSetup>(x => x.ParticipationTypes);
        [Display(Name = "科目")]
        public ExcelPropety Subject_Excel = ExcelPropety.CreateProperty<ExaminationSetup>(x => x.Subject);

	    protected override void InitVM()
        {
            Seal_Excel.DataType = ColumnDataType.ComboBox;
            Seal_Excel.ListItems = DC.Set<Seal>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class ExaminationSetupImportVM : BaseImportVM<ExaminationSetupTemplateVM, ExaminationSetup>
    {

    }

}
