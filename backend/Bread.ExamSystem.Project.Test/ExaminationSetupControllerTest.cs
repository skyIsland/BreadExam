using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Bread.ExamSystem.Project.Controllers;
using Bread.ExamSystem.Project.ViewModel.ExaminationSetupVMs;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.DataAccess;


namespace Bread.ExamSystem.Project.Test
{
    [TestClass]
    public class ExaminationSetupControllerTest
    {
        private ExaminationSetupController _controller;
        private string _seed;

        public ExaminationSetupControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<ExaminationSetupController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as ExaminationSetupListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(ExaminationSetupVM));

            ExaminationSetupVM vm = rv.Model as ExaminationSetupVM;
            ExaminationSetup v = new ExaminationSetup();
			
            v.ID = 54;
            v.Title = "PqrGnaSrUM3wwkt1Q";
            v.StrTime = DateTime.Parse("2023-05-10 23:08:42");
            v.EndTime = DateTime.Parse("2023-05-18 23:08:42");
            v.CourseEnum = Bread.ExamSystem.Project.Model.FrameworkEnumeration.CourseEnum.正常;
            v.DXNumer = 57;
            v.DXScore = 88;
            v.DSXNumer = 34;
            v.DSXScore = 60;
            v.PDNumer = 95;
            v.PDScore = 78;
            v.TestTime = 77;
            v.SealId = AddSeal();
            v.ParticipationTypes = Bread.ExamSystem.Project.Model.FrameworkEnumeration.ParticipationTypes.非账号;
            v.Subject = "up4ZBM6NsVhCA";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ExaminationSetup>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 54);
                Assert.AreEqual(data.Title, "PqrGnaSrUM3wwkt1Q");
                Assert.AreEqual(data.StrTime, DateTime.Parse("2023-05-10 23:08:42"));
                Assert.AreEqual(data.EndTime, DateTime.Parse("2023-05-18 23:08:42"));
                Assert.AreEqual(data.CourseEnum, Bread.ExamSystem.Project.Model.FrameworkEnumeration.CourseEnum.正常);
                Assert.AreEqual(data.DXNumer, 57);
                Assert.AreEqual(data.DXScore, 88);
                Assert.AreEqual(data.DSXNumer, 34);
                Assert.AreEqual(data.DSXScore, 60);
                Assert.AreEqual(data.PDNumer, 95);
                Assert.AreEqual(data.PDScore, 78);
                Assert.AreEqual(data.TestTime, 77);
                Assert.AreEqual(data.ParticipationTypes, Bread.ExamSystem.Project.Model.FrameworkEnumeration.ParticipationTypes.非账号);
                Assert.AreEqual(data.Subject, "up4ZBM6NsVhCA");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            ExaminationSetup v = new ExaminationSetup();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 54;
                v.Title = "PqrGnaSrUM3wwkt1Q";
                v.StrTime = DateTime.Parse("2023-05-10 23:08:42");
                v.EndTime = DateTime.Parse("2023-05-18 23:08:42");
                v.CourseEnum = Bread.ExamSystem.Project.Model.FrameworkEnumeration.CourseEnum.正常;
                v.DXNumer = 57;
                v.DXScore = 88;
                v.DSXNumer = 34;
                v.DSXScore = 60;
                v.PDNumer = 95;
                v.PDScore = 78;
                v.TestTime = 77;
                v.SealId = AddSeal();
                v.ParticipationTypes = Bread.ExamSystem.Project.Model.FrameworkEnumeration.ParticipationTypes.非账号;
                v.Subject = "up4ZBM6NsVhCA";
                context.Set<ExaminationSetup>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(ExaminationSetupVM));

            ExaminationSetupVM vm = rv.Model as ExaminationSetupVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new ExaminationSetup();
            v.ID = vm.Entity.ID;
       		
            v.Title = "pago6EkPN";
            v.StrTime = DateTime.Parse("2021-10-25 23:08:42");
            v.EndTime = DateTime.Parse("2022-01-14 23:08:42");
            v.CourseEnum = Bread.ExamSystem.Project.Model.FrameworkEnumeration.CourseEnum.正常;
            v.DXNumer = 33;
            v.DXScore = 23;
            v.DSXNumer = 7;
            v.DSXScore = 94;
            v.PDNumer = 48;
            v.PDScore = 16;
            v.TestTime = 33;
            v.ParticipationTypes = Bread.ExamSystem.Project.Model.FrameworkEnumeration.ParticipationTypes.非账号;
            v.Subject = "v0";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.Title", "");
            vm.FC.Add("Entity.StrTime", "");
            vm.FC.Add("Entity.EndTime", "");
            vm.FC.Add("Entity.CourseEnum", "");
            vm.FC.Add("Entity.DXNumer", "");
            vm.FC.Add("Entity.DXScore", "");
            vm.FC.Add("Entity.DSXNumer", "");
            vm.FC.Add("Entity.DSXScore", "");
            vm.FC.Add("Entity.PDNumer", "");
            vm.FC.Add("Entity.PDScore", "");
            vm.FC.Add("Entity.TestTime", "");
            vm.FC.Add("Entity.SealId", "");
            vm.FC.Add("Entity.ParticipationTypes", "");
            vm.FC.Add("Entity.Subject", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ExaminationSetup>().Find(v.ID);
 				
                Assert.AreEqual(data.Title, "pago6EkPN");
                Assert.AreEqual(data.StrTime, DateTime.Parse("2021-10-25 23:08:42"));
                Assert.AreEqual(data.EndTime, DateTime.Parse("2022-01-14 23:08:42"));
                Assert.AreEqual(data.CourseEnum, Bread.ExamSystem.Project.Model.FrameworkEnumeration.CourseEnum.正常);
                Assert.AreEqual(data.DXNumer, 33);
                Assert.AreEqual(data.DXScore, 23);
                Assert.AreEqual(data.DSXNumer, 7);
                Assert.AreEqual(data.DSXScore, 94);
                Assert.AreEqual(data.PDNumer, 48);
                Assert.AreEqual(data.PDScore, 16);
                Assert.AreEqual(data.TestTime, 33);
                Assert.AreEqual(data.ParticipationTypes, Bread.ExamSystem.Project.Model.FrameworkEnumeration.ParticipationTypes.非账号);
                Assert.AreEqual(data.Subject, "v0");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            ExaminationSetup v = new ExaminationSetup();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 54;
                v.Title = "PqrGnaSrUM3wwkt1Q";
                v.StrTime = DateTime.Parse("2023-05-10 23:08:42");
                v.EndTime = DateTime.Parse("2023-05-18 23:08:42");
                v.CourseEnum = Bread.ExamSystem.Project.Model.FrameworkEnumeration.CourseEnum.正常;
                v.DXNumer = 57;
                v.DXScore = 88;
                v.DSXNumer = 34;
                v.DSXScore = 60;
                v.PDNumer = 95;
                v.PDScore = 78;
                v.TestTime = 77;
                v.SealId = AddSeal();
                v.ParticipationTypes = Bread.ExamSystem.Project.Model.FrameworkEnumeration.ParticipationTypes.非账号;
                v.Subject = "up4ZBM6NsVhCA";
                context.Set<ExaminationSetup>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(ExaminationSetupVM));

            ExaminationSetupVM vm = rv.Model as ExaminationSetupVM;
            v = new ExaminationSetup();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ExaminationSetup>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            ExaminationSetup v = new ExaminationSetup();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 54;
                v.Title = "PqrGnaSrUM3wwkt1Q";
                v.StrTime = DateTime.Parse("2023-05-10 23:08:42");
                v.EndTime = DateTime.Parse("2023-05-18 23:08:42");
                v.CourseEnum = Bread.ExamSystem.Project.Model.FrameworkEnumeration.CourseEnum.正常;
                v.DXNumer = 57;
                v.DXScore = 88;
                v.DSXNumer = 34;
                v.DSXScore = 60;
                v.PDNumer = 95;
                v.PDScore = 78;
                v.TestTime = 77;
                v.SealId = AddSeal();
                v.ParticipationTypes = Bread.ExamSystem.Project.Model.FrameworkEnumeration.ParticipationTypes.非账号;
                v.Subject = "up4ZBM6NsVhCA";
                context.Set<ExaminationSetup>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            ExaminationSetup v1 = new ExaminationSetup();
            ExaminationSetup v2 = new ExaminationSetup();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 54;
                v1.Title = "PqrGnaSrUM3wwkt1Q";
                v1.StrTime = DateTime.Parse("2023-05-10 23:08:42");
                v1.EndTime = DateTime.Parse("2023-05-18 23:08:42");
                v1.CourseEnum = Bread.ExamSystem.Project.Model.FrameworkEnumeration.CourseEnum.正常;
                v1.DXNumer = 57;
                v1.DXScore = 88;
                v1.DSXNumer = 34;
                v1.DSXScore = 60;
                v1.PDNumer = 95;
                v1.PDScore = 78;
                v1.TestTime = 77;
                v1.SealId = AddSeal();
                v1.ParticipationTypes = Bread.ExamSystem.Project.Model.FrameworkEnumeration.ParticipationTypes.非账号;
                v1.Subject = "up4ZBM6NsVhCA";
                v2.ID = 3;
                v2.Title = "pago6EkPN";
                v2.StrTime = DateTime.Parse("2021-10-25 23:08:42");
                v2.EndTime = DateTime.Parse("2022-01-14 23:08:42");
                v2.CourseEnum = Bread.ExamSystem.Project.Model.FrameworkEnumeration.CourseEnum.正常;
                v2.DXNumer = 33;
                v2.DXScore = 23;
                v2.DSXNumer = 7;
                v2.DSXScore = 94;
                v2.PDNumer = 48;
                v2.PDScore = 16;
                v2.TestTime = 33;
                v2.SealId = v1.SealId; 
                v2.ParticipationTypes = Bread.ExamSystem.Project.Model.FrameworkEnumeration.ParticipationTypes.非账号;
                v2.Subject = "v0";
                context.Set<ExaminationSetup>().Add(v1);
                context.Set<ExaminationSetup>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(ExaminationSetupBatchVM));

            ExaminationSetupBatchVM vm = rv.Model as ExaminationSetupBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<ExaminationSetup>().Find(v1.ID);
                var data2 = context.Set<ExaminationSetup>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            ExaminationSetup v1 = new ExaminationSetup();
            ExaminationSetup v2 = new ExaminationSetup();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 54;
                v1.Title = "PqrGnaSrUM3wwkt1Q";
                v1.StrTime = DateTime.Parse("2023-05-10 23:08:42");
                v1.EndTime = DateTime.Parse("2023-05-18 23:08:42");
                v1.CourseEnum = Bread.ExamSystem.Project.Model.FrameworkEnumeration.CourseEnum.正常;
                v1.DXNumer = 57;
                v1.DXScore = 88;
                v1.DSXNumer = 34;
                v1.DSXScore = 60;
                v1.PDNumer = 95;
                v1.PDScore = 78;
                v1.TestTime = 77;
                v1.SealId = AddSeal();
                v1.ParticipationTypes = Bread.ExamSystem.Project.Model.FrameworkEnumeration.ParticipationTypes.非账号;
                v1.Subject = "up4ZBM6NsVhCA";
                v2.ID = 3;
                v2.Title = "pago6EkPN";
                v2.StrTime = DateTime.Parse("2021-10-25 23:08:42");
                v2.EndTime = DateTime.Parse("2022-01-14 23:08:42");
                v2.CourseEnum = Bread.ExamSystem.Project.Model.FrameworkEnumeration.CourseEnum.正常;
                v2.DXNumer = 33;
                v2.DXScore = 23;
                v2.DSXNumer = 7;
                v2.DSXScore = 94;
                v2.PDNumer = 48;
                v2.PDScore = 16;
                v2.TestTime = 33;
                v2.SealId = v1.SealId; 
                v2.ParticipationTypes = Bread.ExamSystem.Project.Model.FrameworkEnumeration.ParticipationTypes.非账号;
                v2.Subject = "v0";
                context.Set<ExaminationSetup>().Add(v1);
                context.Set<ExaminationSetup>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(ExaminationSetupBatchVM));

            ExaminationSetupBatchVM vm = rv.Model as ExaminationSetupBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<ExaminationSetup>().Find(v1.ID);
                var data2 = context.Set<ExaminationSetup>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as ExaminationSetupListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "gbcieXmpxlYn5V";
                v.FileExt = "2";
                v.Path = "6";
                v.Length = 45;
                v.UploadTime = DateTime.Parse("2023-02-04 23:08:42");
                v.SaveMode = "B0kRNqhQ695G";
                v.ExtraInfo = "LQamte9zHU88QOiOX";
                v.HandlerInfo = "hqZ2msMlv785N73rcX";
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

                v.Name = "ZiZjsSr";
                v.SType = "L9SH7UMnVemEqE2y";
                v.PhotoId = AddFileAttachment();
                context.Set<Seal>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
