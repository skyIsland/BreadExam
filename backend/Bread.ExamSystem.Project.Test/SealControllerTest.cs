using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Bread.ExamSystem.Project.Controllers;
using Bread.ExamSystem.Project.ViewModel.SealVMs;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.DataAccess;


namespace Bread.ExamSystem.Project.Test
{
    [TestClass]
    public class SealControllerTest
    {
        private SealController _controller;
        private string _seed;

        public SealControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<SealController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as SealListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(SealVM));

            SealVM vm = rv.Model as SealVM;
            Seal v = new Seal();
			
            v.Name = "IWR1mznNuiMcPG30";
            v.SType = "9vc2QaP4PbYUxRU";
            v.PhotoId = AddFileAttachment();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Seal>().Find(v.ID);
				
                Assert.AreEqual(data.Name, "IWR1mznNuiMcPG30");
                Assert.AreEqual(data.SType, "9vc2QaP4PbYUxRU");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Seal v = new Seal();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Name = "IWR1mznNuiMcPG30";
                v.SType = "9vc2QaP4PbYUxRU";
                v.PhotoId = AddFileAttachment();
                context.Set<Seal>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(SealVM));

            SealVM vm = rv.Model as SealVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new Seal();
            v.ID = vm.Entity.ID;
       		
            v.Name = "ERJWO";
            v.SType = "hKBgJfXJDiB";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.SType", "");
            vm.FC.Add("Entity.PhotoId", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Seal>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "ERJWO");
                Assert.AreEqual(data.SType, "hKBgJfXJDiB");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Seal v = new Seal();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Name = "IWR1mznNuiMcPG30";
                v.SType = "9vc2QaP4PbYUxRU";
                v.PhotoId = AddFileAttachment();
                context.Set<Seal>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(SealVM));

            SealVM vm = rv.Model as SealVM;
            v = new Seal();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Seal>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Seal v = new Seal();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Name = "IWR1mznNuiMcPG30";
                v.SType = "9vc2QaP4PbYUxRU";
                v.PhotoId = AddFileAttachment();
                context.Set<Seal>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            Seal v1 = new Seal();
            Seal v2 = new Seal();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "IWR1mznNuiMcPG30";
                v1.SType = "9vc2QaP4PbYUxRU";
                v1.PhotoId = AddFileAttachment();
                v2.Name = "ERJWO";
                v2.SType = "hKBgJfXJDiB";
                v2.PhotoId = v1.PhotoId; 
                context.Set<Seal>().Add(v1);
                context.Set<Seal>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(SealBatchVM));

            SealBatchVM vm = rv.Model as SealBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Seal>().Find(v1.ID);
                var data2 = context.Set<Seal>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            Seal v1 = new Seal();
            Seal v2 = new Seal();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "IWR1mznNuiMcPG30";
                v1.SType = "9vc2QaP4PbYUxRU";
                v1.PhotoId = AddFileAttachment();
                v2.Name = "ERJWO";
                v2.SType = "hKBgJfXJDiB";
                v2.PhotoId = v1.PhotoId; 
                context.Set<Seal>().Add(v1);
                context.Set<Seal>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(SealBatchVM));

            SealBatchVM vm = rv.Model as SealBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Seal>().Find(v1.ID);
                var data2 = context.Set<Seal>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as SealListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "78CUk";
                v.FileExt = "p9ti";
                v.Path = "0lryfCnEKfKBd6ka0";
                v.Length = 94;
                v.UploadTime = DateTime.Parse("2023-07-07 23:11:24");
                v.SaveMode = "IzSGBfML";
                v.ExtraInfo = "sAyoApi03xH1";
                v.HandlerInfo = "hytqtsJf4";
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
