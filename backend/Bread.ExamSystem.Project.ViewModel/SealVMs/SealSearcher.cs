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
    public partial class SealSearcher : BaseSearcher
    {
        [Display(Name = "单位名称")]
        public String Name { get; set; }

        protected override void InitVM()
        {
        }

    }
}
