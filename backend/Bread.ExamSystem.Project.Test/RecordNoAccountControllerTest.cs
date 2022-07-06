using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Bread.ExamSystem.Project.Controllers;
using Bread.ExamSystem.Project.ViewModel.RecordNoAccountVMs;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.DataAccess;


namespace Bread.ExamSystem.Project.Test
{
    [TestClass]
    public class RecordNoAccountControllerTest
    {
        private RecordNoAccountController _controller;
        private string _seed;

        public RecordNoAccountControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<RecordNoAccountController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as RecordNoAccountListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(RecordNoAccountVM));

            RecordNoAccountVM vm = rv.Model as RecordNoAccountVM;
            RecordNoAccount v = new RecordNoAccount();
			
            v.ID = 22;
            v.ExaminationSetupID = AddExaminationSetup();
            v.ParticipationTime = DateTime.Parse("2021-03-19 23:09:51");
            v.UserName = "EfsLwq9S1ejCOEIe17H";
            v.UnitWork = "YE4KLnmb5rfZtpOuqv";
            v.Achievement = 41;
            v.QuestionId = "YfC";
            v.QuestionAnswer = "HeifvnltyOV9GNbA";
            v.ExamineeAnswers = "PRkebTVPRsJU4HYkeLB";
            v.Phone = "qa1IhCJmQcepB4hKz1";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<RecordNoAccount>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 22);
                Assert.AreEqual(data.ParticipationTime, DateTime.Parse("2021-03-19 23:09:51"));
                Assert.AreEqual(data.UserName, "EfsLwq9S1ejCOEIe17H");
                Assert.AreEqual(data.UnitWork, "YE4KLnmb5rfZtpOuqv");
                Assert.AreEqual(data.Achievement, 41);
                Assert.AreEqual(data.QuestionId, "YfC");
                Assert.AreEqual(data.QuestionAnswer, "HeifvnltyOV9GNbA");
                Assert.AreEqual(data.ExamineeAnswers, "PRkebTVPRsJU4HYkeLB");
                Assert.AreEqual(data.Phone, "qa1IhCJmQcepB4hKz1");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            RecordNoAccount v = new RecordNoAccount();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 22;
                v.ExaminationSetupID = AddExaminationSetup();
                v.ParticipationTime = DateTime.Parse("2021-03-19 23:09:51");
                v.UserName = "EfsLwq9S1ejCOEIe17H";
                v.UnitWork = "YE4KLnmb5rfZtpOuqv";
                v.Achievement = 41;
                v.QuestionId = "YfC";
                v.QuestionAnswer = "HeifvnltyOV9GNbA";
                v.ExamineeAnswers = "PRkebTVPRsJU4HYkeLB";
                v.Phone = "qa1IhCJmQcepB4hKz1";
                context.Set<RecordNoAccount>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(RecordNoAccountVM));

            RecordNoAccountVM vm = rv.Model as RecordNoAccountVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new RecordNoAccount();
            v.ID = vm.Entity.ID;
       		
            v.ParticipationTime = DateTime.Parse("2022-07-02 23:09:51");
            v.UserName = "8";
            v.UnitWork = "qJu4VXrpHoJ9nU36Zh";
            v.Achievement = 30;
            v.QuestionId = "SP";
            v.QuestionAnswer = "15bqb55s";
            v.ExamineeAnswers = "xEwf";
            v.Phone = "9pwGPv";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.ExaminationSetupID", "");
            vm.FC.Add("Entity.ParticipationTime", "");
            vm.FC.Add("Entity.UserName", "");
            vm.FC.Add("Entity.UnitWork", "");
            vm.FC.Add("Entity.Achievement", "");
            vm.FC.Add("Entity.QuestionId", "");
            vm.FC.Add("Entity.QuestionAnswer", "");
            vm.FC.Add("Entity.ExamineeAnswers", "");
            vm.FC.Add("Entity.Phone", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<RecordNoAccount>().Find(v.ID);
 				
                Assert.AreEqual(data.ParticipationTime, DateTime.Parse("2022-07-02 23:09:51"));
                Assert.AreEqual(data.UserName, "8");
                Assert.AreEqual(data.UnitWork, "qJu4VXrpHoJ9nU36Zh");
                Assert.AreEqual(data.Achievement, 30);
                Assert.AreEqual(data.QuestionId, "SP");
                Assert.AreEqual(data.QuestionAnswer, "15bqb55s");
                Assert.AreEqual(data.ExamineeAnswers, "xEwf");
                Assert.AreEqual(data.Phone, "9pwGPv");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            RecordNoAccount v = new RecordNoAccount();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 22;
                v.ExaminationSetupID = AddExaminationSetup();
                v.ParticipationTime = DateTime.Parse("2021-03-19 23:09:51");
                v.UserName = "EfsLwq9S1ejCOEIe17H";
                v.UnitWork = "YE4KLnmb5rfZtpOuqv";
                v.Achievement = 41;
                v.QuestionId = "YfC";
                v.QuestionAnswer = "HeifvnltyOV9GNbA";
                v.ExamineeAnswers = "PRkebTVPRsJU4HYkeLB";
                v.Phone = "qa1IhCJmQcepB4hKz1";
                context.Set<RecordNoAccount>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(RecordNoAccountVM));

            RecordNoAccountVM vm = rv.Model as RecordNoAccountVM;
            v = new RecordNoAccount();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<RecordNoAccount>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            RecordNoAccount v = new RecordNoAccount();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 22;
                v.ExaminationSetupID = AddExaminationSetup();
                v.ParticipationTime = DateTime.Parse("2021-03-19 23:09:51");
                v.UserName = "EfsLwq9S1ejCOEIe17H";
                v.UnitWork = "YE4KLnmb5rfZtpOuqv";
                v.Achievement = 41;
                v.QuestionId = "YfC";
                v.QuestionAnswer = "HeifvnltyOV9GNbA";
                v.ExamineeAnswers = "PRkebTVPRsJU4HYkeLB";
                v.Phone = "qa1IhCJmQcepB4hKz1";
                context.Set<RecordNoAccount>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            RecordNoAccount v1 = new RecordNoAccount();
            RecordNoAccount v2 = new RecordNoAccount();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 22;
                v1.ExaminationSetupID = AddExaminationSetup();
                v1.ParticipationTime = DateTime.Parse("2021-03-19 23:09:51");
                v1.UserName = "EfsLwq9S1ejCOEIe17H";
                v1.UnitWork = "YE4KLnmb5rfZtpOuqv";
                v1.Achievement = 41;
                v1.QuestionId = "YfC";
                v1.QuestionAnswer = "HeifvnltyOV9GNbA";
                v1.ExamineeAnswers = "PRkebTVPRsJU4HYkeLB";
                v1.Phone = "qa1IhCJmQcepB4hKz1";
                v2.ID = 33;
                v2.ExaminationSetupID = v1.ExaminationSetupID; 
                v2.ParticipationTime = DateTime.Parse("2022-07-02 23:09:51");
                v2.UserName = "8";
                v2.UnitWork = "qJu4VXrpHoJ9nU36Zh";
                v2.Achievement = 30;
                v2.QuestionId = "SP";
                v2.QuestionAnswer = "15bqb55s";
                v2.ExamineeAnswers = "xEwf";
                v2.Phone = "9pwGPv";
                context.Set<RecordNoAccount>().Add(v1);
                context.Set<RecordNoAccount>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(RecordNoAccountBatchVM));

            RecordNoAccountBatchVM vm = rv.Model as RecordNoAccountBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<RecordNoAccount>().Find(v1.ID);
                var data2 = context.Set<RecordNoAccount>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            RecordNoAccount v1 = new RecordNoAccount();
            RecordNoAccount v2 = new RecordNoAccount();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 22;
                v1.ExaminationSetupID = AddExaminationSetup();
                v1.ParticipationTime = DateTime.Parse("2021-03-19 23:09:51");
                v1.UserName = "EfsLwq9S1ejCOEIe17H";
                v1.UnitWork = "YE4KLnmb5rfZtpOuqv";
                v1.Achievement = 41;
                v1.QuestionId = "YfC";
                v1.QuestionAnswer = "HeifvnltyOV9GNbA";
                v1.ExamineeAnswers = "PRkebTVPRsJU4HYkeLB";
                v1.Phone = "qa1IhCJmQcepB4hKz1";
                v2.ID = 33;
                v2.ExaminationSetupID = v1.ExaminationSetupID; 
                v2.ParticipationTime = DateTime.Parse("2022-07-02 23:09:51");
                v2.UserName = "8";
                v2.UnitWork = "qJu4VXrpHoJ9nU36Zh";
                v2.Achievement = 30;
                v2.QuestionId = "SP";
                v2.QuestionAnswer = "15bqb55s";
                v2.ExamineeAnswers = "xEwf";
                v2.Phone = "9pwGPv";
                context.Set<RecordNoAccount>().Add(v1);
                context.Set<RecordNoAccount>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(RecordNoAccountBatchVM));

            RecordNoAccountBatchVM vm = rv.Model as RecordNoAccountBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<RecordNoAccount>().Find(v1.ID);
                var data2 = context.Set<RecordNoAccount>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as RecordNoAccountListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "DsGiUSeSA";
                v.FileExt = "lp0DuKMk";
                v.Path = "3zkv3fdpF3uxx";
                v.Length = 72;
                v.UploadTime = DateTime.Parse("2021-12-06 23:09:51");
                v.SaveMode = "0Gk7QfO7IqcSJZl";
                v.ExtraInfo = "UMcJRqDxiDj2HDg9";
                v.HandlerInfo = "O3t98FQVhFQzV4B1";
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

                v.Name = "87dim12xzRHA";
                v.SType = "8qfpak8W7Fajm9J5";
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

                v.ID = 84;
                v.Title = "nNUnpBMLXmDD";
                v.StrTime = DateTime.Parse("2023-01-06 23:09:51");
                v.EndTime = DateTime.Parse("2023-04-02 23:09:51");
                v.CourseEnum = Bread.ExamSystem.Project.Model.FrameworkEnumeration.CourseEnum.正常;
                v.DXNumer = 66;
                v.DXScore = 59;
                v.DSXNumer = 7;
                v.DSXScore = 54;
                v.PDNumer = 48;
                v.PDScore = 58;
                v.TestTime = 20;
                v.SealId = AddSeal();
                v.ParticipationTypes = Bread.ExamSystem.Project.Model.FrameworkEnumeration.ParticipationTypes.账号;
                v.Subject = "Por8vM8Cftp8c";
                context.Set<ExaminationSetup>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
