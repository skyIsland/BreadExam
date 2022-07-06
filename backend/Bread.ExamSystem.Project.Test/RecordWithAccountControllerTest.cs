using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Bread.ExamSystem.Project.Controllers;
using Bread.ExamSystem.Project.ViewModel.RecordWithAccountVMs;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.DataAccess;


namespace Bread.ExamSystem.Project.Test
{
    [TestClass]
    public class RecordWithAccountControllerTest
    {
        private RecordWithAccountController _controller;
        private string _seed;

        public RecordWithAccountControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<RecordWithAccountController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as RecordWithAccountListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(RecordWithAccountVM));

            RecordWithAccountVM vm = rv.Model as RecordWithAccountVM;
            RecordWithAccount v = new RecordWithAccount();
			
            v.ID = 30;
            v.ExaminationSetupID = AddExaminationSetup();
            v.ParticipationTime = DateTime.Parse("2023-09-04 23:10:11");
            v.FrameworkUserID = AddFrameworkUser();
            v.Achievement = 88;
            v.QuestionId = "hjnyHlxuHbR7l";
            v.QuestionAnswer = "lPhMUKneFt";
            v.ExamineeAnswers = "CRG4kM26qp7kLg";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<RecordWithAccount>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 30);
                Assert.AreEqual(data.ParticipationTime, DateTime.Parse("2023-09-04 23:10:11"));
                Assert.AreEqual(data.Achievement, 88);
                Assert.AreEqual(data.QuestionId, "hjnyHlxuHbR7l");
                Assert.AreEqual(data.QuestionAnswer, "lPhMUKneFt");
                Assert.AreEqual(data.ExamineeAnswers, "CRG4kM26qp7kLg");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            RecordWithAccount v = new RecordWithAccount();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 30;
                v.ExaminationSetupID = AddExaminationSetup();
                v.ParticipationTime = DateTime.Parse("2023-09-04 23:10:11");
                v.FrameworkUserID = AddFrameworkUser();
                v.Achievement = 88;
                v.QuestionId = "hjnyHlxuHbR7l";
                v.QuestionAnswer = "lPhMUKneFt";
                v.ExamineeAnswers = "CRG4kM26qp7kLg";
                context.Set<RecordWithAccount>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(RecordWithAccountVM));

            RecordWithAccountVM vm = rv.Model as RecordWithAccountVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new RecordWithAccount();
            v.ID = vm.Entity.ID;
       		
            v.ParticipationTime = DateTime.Parse("2023-01-07 23:10:11");
            v.Achievement = 38;
            v.QuestionId = "lM0wo4aec6eG5S";
            v.QuestionAnswer = "va";
            v.ExamineeAnswers = "i";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.ExaminationSetupID", "");
            vm.FC.Add("Entity.ParticipationTime", "");
            vm.FC.Add("Entity.FrameworkUserID", "");
            vm.FC.Add("Entity.Achievement", "");
            vm.FC.Add("Entity.QuestionId", "");
            vm.FC.Add("Entity.QuestionAnswer", "");
            vm.FC.Add("Entity.ExamineeAnswers", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<RecordWithAccount>().Find(v.ID);
 				
                Assert.AreEqual(data.ParticipationTime, DateTime.Parse("2023-01-07 23:10:11"));
                Assert.AreEqual(data.Achievement, 38);
                Assert.AreEqual(data.QuestionId, "lM0wo4aec6eG5S");
                Assert.AreEqual(data.QuestionAnswer, "va");
                Assert.AreEqual(data.ExamineeAnswers, "i");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            RecordWithAccount v = new RecordWithAccount();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 30;
                v.ExaminationSetupID = AddExaminationSetup();
                v.ParticipationTime = DateTime.Parse("2023-09-04 23:10:11");
                v.FrameworkUserID = AddFrameworkUser();
                v.Achievement = 88;
                v.QuestionId = "hjnyHlxuHbR7l";
                v.QuestionAnswer = "lPhMUKneFt";
                v.ExamineeAnswers = "CRG4kM26qp7kLg";
                context.Set<RecordWithAccount>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(RecordWithAccountVM));

            RecordWithAccountVM vm = rv.Model as RecordWithAccountVM;
            v = new RecordWithAccount();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<RecordWithAccount>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            RecordWithAccount v = new RecordWithAccount();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 30;
                v.ExaminationSetupID = AddExaminationSetup();
                v.ParticipationTime = DateTime.Parse("2023-09-04 23:10:11");
                v.FrameworkUserID = AddFrameworkUser();
                v.Achievement = 88;
                v.QuestionId = "hjnyHlxuHbR7l";
                v.QuestionAnswer = "lPhMUKneFt";
                v.ExamineeAnswers = "CRG4kM26qp7kLg";
                context.Set<RecordWithAccount>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            RecordWithAccount v1 = new RecordWithAccount();
            RecordWithAccount v2 = new RecordWithAccount();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 30;
                v1.ExaminationSetupID = AddExaminationSetup();
                v1.ParticipationTime = DateTime.Parse("2023-09-04 23:10:11");
                v1.FrameworkUserID = AddFrameworkUser();
                v1.Achievement = 88;
                v1.QuestionId = "hjnyHlxuHbR7l";
                v1.QuestionAnswer = "lPhMUKneFt";
                v1.ExamineeAnswers = "CRG4kM26qp7kLg";
                v2.ID = 2;
                v2.ExaminationSetupID = v1.ExaminationSetupID; 
                v2.ParticipationTime = DateTime.Parse("2023-01-07 23:10:11");
                v2.FrameworkUserID = v1.FrameworkUserID; 
                v2.Achievement = 38;
                v2.QuestionId = "lM0wo4aec6eG5S";
                v2.QuestionAnswer = "va";
                v2.ExamineeAnswers = "i";
                context.Set<RecordWithAccount>().Add(v1);
                context.Set<RecordWithAccount>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(RecordWithAccountBatchVM));

            RecordWithAccountBatchVM vm = rv.Model as RecordWithAccountBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<RecordWithAccount>().Find(v1.ID);
                var data2 = context.Set<RecordWithAccount>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            RecordWithAccount v1 = new RecordWithAccount();
            RecordWithAccount v2 = new RecordWithAccount();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 30;
                v1.ExaminationSetupID = AddExaminationSetup();
                v1.ParticipationTime = DateTime.Parse("2023-09-04 23:10:11");
                v1.FrameworkUserID = AddFrameworkUser();
                v1.Achievement = 88;
                v1.QuestionId = "hjnyHlxuHbR7l";
                v1.QuestionAnswer = "lPhMUKneFt";
                v1.ExamineeAnswers = "CRG4kM26qp7kLg";
                v2.ID = 2;
                v2.ExaminationSetupID = v1.ExaminationSetupID; 
                v2.ParticipationTime = DateTime.Parse("2023-01-07 23:10:11");
                v2.FrameworkUserID = v1.FrameworkUserID; 
                v2.Achievement = 38;
                v2.QuestionId = "lM0wo4aec6eG5S";
                v2.QuestionAnswer = "va";
                v2.ExamineeAnswers = "i";
                context.Set<RecordWithAccount>().Add(v1);
                context.Set<RecordWithAccount>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(RecordWithAccountBatchVM));

            RecordWithAccountBatchVM vm = rv.Model as RecordWithAccountBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<RecordWithAccount>().Find(v1.ID);
                var data2 = context.Set<RecordWithAccount>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as RecordWithAccountListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "fPHy6CtU";
                v.FileExt = "Q51P";
                v.Path = "8GlQas8k4rB64";
                v.Length = 66;
                v.UploadTime = DateTime.Parse("2023-05-19 23:10:11");
                v.SaveMode = "CEJT0t";
                v.ExtraInfo = "O4AjTEvSGAu5RVJj4Et";
                v.HandlerInfo = "g77h5J";
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

                v.Name = "9";
                v.SType = "LOykCe";
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

                v.ID = 40;
                v.Title = "UXt6QCKiwJZ";
                v.StrTime = DateTime.Parse("2022-10-21 23:10:11");
                v.EndTime = DateTime.Parse("2023-04-05 23:10:11");
                v.CourseEnum = Bread.ExamSystem.Project.Model.FrameworkEnumeration.CourseEnum.禁用;
                v.DXNumer = 39;
                v.DXScore = 39;
                v.DSXNumer = 3;
                v.DSXScore = 17;
                v.PDNumer = 71;
                v.PDScore = 13;
                v.TestTime = 90;
                v.SealId = AddSeal();
                v.ParticipationTypes = Bread.ExamSystem.Project.Model.FrameworkEnumeration.ParticipationTypes.非账号;
                v.Subject = "TJpVP4muYKg896DNWqN";
                context.Set<ExaminationSetup>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddUnitWork()
        {
            UnitWork v = new UnitWork();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.UnitWorkName = "UUnYsldqLy4RqC";
                context.Set<UnitWork>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddFrameworkUser()
        {
            FrameworkUser v = new FrameworkUser();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Email = "rTFWdRyvJ";
                v.Gender = WalkingTec.Mvvm.Core.GenderEnum.Female;
                v.CellPhone = "JiAwzHXEN6TDVNDf";
                v.HomePhone = "f5GnTpl5wiwH1jCd5TA8GN";
                v.Address = "IphaGqyM9hr2eAHxbIOqA9jvpv9aP6cYg9gKnzT9PoFrMrIQoo3hHddDC7r2iz8R";
                v.ZipCode = "y";
                v.UnitWorkID = AddUnitWork();
                v.OpenID = "z";
                v.ITCode = "M7jcbJotniCHNDZXGT8";
                v.Password = "HYDeBPCPAeDxfPGm6D8iaiH";
                v.Name = "wDKsX";
                v.IsValid = true;
                v.PhotoId = AddFileAttachment();
                context.Set<FrameworkUser>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
