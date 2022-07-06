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
    public partial class CollectionBatchVM : BaseBatchVM<Collection, Collection_BatchEdit>
    {
        public CollectionBatchVM()
        {
            ListVM = new CollectionListVM();
            LinkedVM = new Collection_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Collection_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
