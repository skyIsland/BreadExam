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
    public partial class RecordNoAccountSearcher : BaseSearcher
    {
        [Display(Name = "所在单位")]
        public String UnitWork { get; set; }
        [Display(Name = "手机号码")]
        public String Phone { get; set; }

        protected override void InitVM()
        {
        }

    }
}
