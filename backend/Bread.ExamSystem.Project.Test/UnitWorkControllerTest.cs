using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Bread.ExamSystem.Project.Controllers;
using Bread.ExamSystem.Project.ViewModel.UnitWorkVMs;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.DataAccess;


namespace Bread.ExamSystem.Project.Test
{
    [TestClass]
    public class UnitWorkControllerTest
    {
        private UnitWorkController _controller;
        private string _seed;

        public UnitWorkControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<UnitWorkController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as UnitWorkListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(UnitWorkVM));

            UnitWorkVM vm = rv.Model as UnitWorkVM;
            UnitWork v = new UnitWork();
			
            v.UnitWorkName = "n5zbE5Q15Kmiwss0gTK";
            v.ParentId = AddUnitWork();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<UnitWork>().Find(v.ID);
				
                Assert.AreEqual(data.UnitWorkName, "n5zbE5Q15Kmiwss0gTK");
            }

        }

        [TestMethod]
        public void EditTest()
        {
            UnitWork v = new UnitWork();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.UnitWorkName = "n5zbE5Q15Kmiwss0gTK";
                v.ParentId = AddUnitWork();
                context.Set<UnitWork>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(UnitWorkVM));

            UnitWorkVM vm = rv.Model as UnitWorkVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new UnitWork();
            v.ID = vm.Entity.ID;
       		
            v.UnitWorkName = "WK0ON9qN1XPNXHS";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.UnitWorkName", "");
            vm.FC.Add("Entity.ParentId", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<UnitWork>().Find(v.ID);
 				
                Assert.AreEqual(data.UnitWorkName, "WK0ON9qN1XPNXHS");
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            UnitWork v = new UnitWork();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.UnitWorkName = "n5zbE5Q15Kmiwss0gTK";
                v.ParentId = AddUnitWork();
                context.Set<UnitWork>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(UnitWorkVM));

            UnitWorkVM vm = rv.Model as UnitWorkVM;
            v = new UnitWork();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<UnitWork>().Find(v.ID);
                Assert.AreEqual(data, null);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            UnitWork v = new UnitWork();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.UnitWorkName = "n5zbE5Q15Kmiwss0gTK";
                v.ParentId = AddUnitWork();
                context.Set<UnitWork>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            UnitWork v1 = new UnitWork();
            UnitWork v2 = new UnitWork();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.UnitWorkName = "n5zbE5Q15Kmiwss0gTK";
                v1.ParentId = AddUnitWork();
                v2.UnitWorkName = "WK0ON9qN1XPNXHS";
                v2.ParentId = v1.ParentId; 
                context.Set<UnitWork>().Add(v1);
                context.Set<UnitWork>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(UnitWorkBatchVM));

            UnitWorkBatchVM vm = rv.Model as UnitWorkBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<UnitWork>().Find(v1.ID);
                var data2 = context.Set<UnitWork>().Find(v2.ID);
 				
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            UnitWork v1 = new UnitWork();
            UnitWork v2 = new UnitWork();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.UnitWorkName = "n5zbE5Q15Kmiwss0gTK";
                v1.ParentId = AddUnitWork();
                v2.UnitWorkName = "WK0ON9qN1XPNXHS";
                v2.ParentId = v1.ParentId; 
                context.Set<UnitWork>().Add(v1);
                context.Set<UnitWork>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(UnitWorkBatchVM));

            UnitWorkBatchVM vm = rv.Model as UnitWorkBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<UnitWork>().Find(v1.ID);
                var data2 = context.Set<UnitWork>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        private Guid AddUnitWork()
        {
            UnitWork v = new UnitWork();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.UnitWorkName = "sA";
                context.Set<UnitWork>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
