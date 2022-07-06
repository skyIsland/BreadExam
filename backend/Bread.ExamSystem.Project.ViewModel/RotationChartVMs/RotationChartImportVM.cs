using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.RotationChartVMs
{
    public partial class RotationChartTemplateVM : BaseTemplateVM
    {
        [Display(Name = "举办活动")]
        public ExcelPropety ExaminationSetup_Excel = ExcelPropety.CreateProperty<RotationChart>(x => x.ExaminationSetupID);

	    protected override void InitVM()
        {
            ExaminationSetup_Excel.DataType = ColumnDataType.ComboBox;
            ExaminationSetup_Excel.ListItems = DC.Set<ExaminationSetup>().GetSelectListItems(Wtm, y => y.CreateBy);
        }

    }

    public class RotationChartImportVM : BaseImportVM<RotationChartTemplateVM, RotationChart>
    {

    }

}
