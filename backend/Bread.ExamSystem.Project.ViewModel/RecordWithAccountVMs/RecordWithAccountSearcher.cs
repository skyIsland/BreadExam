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
    public partial class RecordWithAccountSearcher : BaseSearcher
    {
        [Display(Name = "参与时间")]
        public DateRange ParticipationTime { get; set; }

        protected override void InitVM()
        {
        }

    }
}
