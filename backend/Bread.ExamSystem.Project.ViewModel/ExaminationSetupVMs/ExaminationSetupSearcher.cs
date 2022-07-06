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
    public partial class ExaminationSetupSearcher : BaseSearcher
    {
        [Display(Name = "试卷名称")]
        public String Title { get; set; }
        [Display(Name = "开始时间")]
        public DateRange StrTime { get; set; }
        [Display(Name = "是否登录")]
        public ParticipationTypes? ParticipationTypes { get; set; }

        protected override void InitVM()
        {
        }

    }
}
