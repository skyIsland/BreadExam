using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.Model;


namespace Bread.ExamSystem.Project.ViewModel.CollectionVMs
{
    public partial class CollectionTemplateVM : BaseTemplateVM
    {
        [Display(Name = "收藏题目")]
        public ExcelPropety Question_Excel = ExcelPropety.CreateProperty<Collection>(x => x.QuestionID);
        [Display(Name = "用户")]
        public ExcelPropety FrameworkUser_Excel = ExcelPropety.CreateProperty<Collection>(x => x.FrameworkUserID);

	    protected override void InitVM()
        {
            Question_Excel.DataType = ColumnDataType.ComboBox;
            Question_Excel.ListItems = DC.Set<Question>().GetSelectListItems(Wtm, y => y.Anwser);
            FrameworkUser_Excel.DataType = ColumnDataType.ComboBox;
            FrameworkUser_Excel.ListItems = DC.Set<FrameworkUser>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class CollectionImportVM : BaseImportVM<CollectionTemplateVM, Collection>
    {

    }

}
