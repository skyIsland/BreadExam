using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.SealVMs
{
    public partial class SealTemplateVM : BaseTemplateVM
    {
        [Display(Name = "单位名称")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<Seal>(x => x.Name);
        [Display(Name = "证件类型")]
        public ExcelPropety SType_Excel = ExcelPropety.CreateProperty<Seal>(x => x.SType);

	    protected override void InitVM()
        {
        }

    }

    public class SealImportVM : BaseImportVM<SealTemplateVM, Seal>
    {

    }

}
