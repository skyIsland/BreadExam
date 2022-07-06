using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Bread.ExamSystem.Project.Controllers;
using Bread.ExamSystem.Project.ViewModel.RotationChartVMs;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.DataAccess;


namespace Bread.ExamSystem.Project.Test
{
    [TestClass]
    public class RotationChartControllerTest
    {
        private RotationChartController _controller;
        private string _seed;

        public RotationChartControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<RotationChartController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as RotationChartListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(RotationChartVM));

            RotationChartVM vm = rv.Model as RotationChartVM;
            RotationChart v = new RotationChart();
			
            v.ID = 29;
            v.PhotoId = AddFileAttachment();
            v.ExaminationSetupID = AddExaminationSetup();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<RotationChart>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 29);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            RotationChart v = new RotationChart();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 29;
                v.PhotoId = AddFileAttachment();
                v.ExaminationSetupID = AddExaminationSetup();
                context.Set<RotationChart>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(RotationChartVM));

            RotationChartVM vm = rv.Model as RotationChartVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new RotationChart();
            v.ID = vm.Entity.ID;
       		
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.PhotoId", "");
            vm.FC.Add("Entity.ExaminationSetupID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<RotationChart>().Find(v.ID);
 				
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            RotationChart v = new RotationChart();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 29;
                v.PhotoId = AddFileAttachment();
                v.ExaminationSetupID = AddExaminationSetup();
                context.Set<RotationChart>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(RotationChartVM));

            RotationChartVM vm = rv.Model as RotationChartVM;
            v = new RotationChart();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<RotationChart>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            RotationChart v = new RotationChart();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 29;
                v.PhotoId = AddFileAttachment();
                v.ExaminationSetupID = AddExaminationSetup();
                context.Set<RotationChart>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            RotationChart v1 = new RotationChart();
            RotationChart v2 = new RotationChart();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 29;
                v1.PhotoId = AddFileAttachment();
                v1.ExaminationSetupID = AddExaminationSetup();
                v2.ID = 54;
                v2.PhotoId = v1.PhotoId; 
                v2.ExaminationSetupID = v1.ExaminationSetupID; 
                context.Set<RotationChart>().Add(v1);
                context.Set<RotationChart>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(RotationChartBatchVM));

            RotationChartBatchVM vm = rv.Model as RotationChartBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<RotationChart>().Find(v1.ID);
                var data2 = context.Set<RotationChart>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            RotationChart v1 = new RotationChart();
            RotationChart v2 = new RotationChart();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 29;
                v1.PhotoId = AddFileAttachment();
                v1.ExaminationSetupID = AddExaminationSetup();
                v2.ID = 54;
                v2.PhotoId = v1.PhotoId; 
                v2.ExaminationSetupID = v1.ExaminationSetupID; 
                context.Set<RotationChart>().Add(v1);
                context.Set<RotationChart>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(RotationChartBatchVM));

            RotationChartBatchVM vm = rv.Model as RotationChartBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<RotationChart>().Find(v1.ID);
                var data2 = context.Set<RotationChart>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as RotationChartListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "H3oN1Qka8b7cDnx1Q";
                v.FileExt = "hD1Tdr";
                v.Path = "yhdrPyIWFSkjcq";
                v.Length = 36;
                v.UploadTime = DateTime.Parse("2022-05-26 23:10:52");
                v.SaveMode = "oJG0Llwqmf";
                v.ExtraInfo = "XGH";
                v.HandlerInfo = "AEfqchAsVSye7A86";
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddSeal()
        {
            Seal v = new Seal();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Name = "UosQBeI";
                v.SType = "7oR8bTQ9VDeQv";
                v.PhotoId = AddFileAttachment();
                context.Set<Seal>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Int32 AddExaminationSetup()
        {
            ExaminationSetup v = new ExaminationSetup();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ID = 36;
                v.Title = "oRP23Odyd";
                v.StrTime = DateTime.Parse("2022-10-11 23:10:52");
                v.EndTime = DateTime.Parse("2022-04-08 23:10:52");
                v.CourseEnum = Bread.ExamSystem.Project.Model.FrameworkEnumeration.CourseEnum.正常;
                v.DXNumer = 40;
                v.DXScore = 61;
                v.DSXNumer = 77;
                v.DSXScore = 21;
                v.PDNumer = 37;
                v.PDScore = 16;
                v.TestTime = 70;
                v.SealId = AddSeal();
                v.ParticipationTypes = Bread.ExamSystem.Project.Model.FrameworkEnumeration.ParticipationTypes.账号;
                v.Subject = "FJgMr5EJFNW4";
                context.Set<ExaminationSetup>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
