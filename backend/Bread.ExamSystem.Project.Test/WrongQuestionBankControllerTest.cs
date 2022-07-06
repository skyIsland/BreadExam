using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Bread.ExamSystem.Project.Controllers;
using Bread.ExamSystem.Project.ViewModel.WrongQuestionBankVMs;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.DataAccess;


namespace Bread.ExamSystem.Project.Test
{
    [TestClass]
    public class WrongQuestionBankControllerTest
    {
        private WrongQuestionBankController _controller;
        private string _seed;

        public WrongQuestionBankControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<WrongQuestionBankController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as WrongQuestionBankListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(WrongQuestionBankVM));

            WrongQuestionBankVM vm = rv.Model as WrongQuestionBankVM;
            WrongQuestionBank v = new WrongQuestionBank();
			
            v.ID = 77;
            v.QuestionID = 26;
            v.FrameworkUserID = AddFrameworkUser();
            v.WrongNumber = 33;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<WrongQuestionBank>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 77);
                Assert.AreEqual(data.QuestionID, 26);
                Assert.AreEqual(data.WrongNumber, 33);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            WrongQuestionBank v = new WrongQuestionBank();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 77;
                v.QuestionID = 26;
                v.FrameworkUserID = AddFrameworkUser();
                v.WrongNumber = 33;
                context.Set<WrongQuestionBank>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(WrongQuestionBankVM));

            WrongQuestionBankVM vm = rv.Model as WrongQuestionBankVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new WrongQuestionBank();
            v.ID = vm.Entity.ID;
       		
            v.QuestionID = 96;
            v.WrongNumber = 45;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.QuestionID", "");
            vm.FC.Add("Entity.FrameworkUserID", "");
            vm.FC.Add("Entity.WrongNumber", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<WrongQuestionBank>().Find(v.ID);
 				
                Assert.AreEqual(data.QuestionID, 96);
                Assert.AreEqual(data.WrongNumber, 45);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            WrongQuestionBank v = new WrongQuestionBank();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 77;
                v.QuestionID = 26;
                v.FrameworkUserID = AddFrameworkUser();
                v.WrongNumber = 33;
                context.Set<WrongQuestionBank>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(WrongQuestionBankVM));

            WrongQuestionBankVM vm = rv.Model as WrongQuestionBankVM;
            v = new WrongQuestionBank();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<WrongQuestionBank>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            WrongQuestionBank v = new WrongQuestionBank();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 77;
                v.QuestionID = 26;
                v.FrameworkUserID = AddFrameworkUser();
                v.WrongNumber = 33;
                context.Set<WrongQuestionBank>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            WrongQuestionBank v1 = new WrongQuestionBank();
            WrongQuestionBank v2 = new WrongQuestionBank();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 77;
                v1.QuestionID = 26;
                v1.FrameworkUserID = AddFrameworkUser();
                v1.WrongNumber = 33;
                v2.ID = 29;
                v2.QuestionID = 96;
                v2.FrameworkUserID = v1.FrameworkUserID; 
                v2.WrongNumber = 45;
                context.Set<WrongQuestionBank>().Add(v1);
                context.Set<WrongQuestionBank>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(WrongQuestionBankBatchVM));

            WrongQuestionBankBatchVM vm = rv.Model as WrongQuestionBankBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<WrongQuestionBank>().Find(v1.ID);
                var data2 = context.Set<WrongQuestionBank>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            WrongQuestionBank v1 = new WrongQuestionBank();
            WrongQuestionBank v2 = new WrongQuestionBank();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 77;
                v1.QuestionID = 26;
                v1.FrameworkUserID = AddFrameworkUser();
                v1.WrongNumber = 33;
                v2.ID = 29;
                v2.QuestionID = 96;
                v2.FrameworkUserID = v1.FrameworkUserID; 
                v2.WrongNumber = 45;
                context.Set<WrongQuestionBank>().Add(v1);
                context.Set<WrongQuestionBank>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(WrongQuestionBankBatchVM));

            WrongQuestionBankBatchVM vm = rv.Model as WrongQuestionBankBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<WrongQuestionBank>().Find(v1.ID);
                var data2 = context.Set<WrongQuestionBank>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as WrongQuestionBankListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddUnitWork()
        {
            UnitWork v = new UnitWork();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.UnitWorkName = "4";
                context.Set<UnitWork>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "b";
                v.FileExt = "0QAS6j";
                v.Path = "K";
                v.Length = 81;
                v.UploadTime = DateTime.Parse("2023-05-02 23:17:57");
                v.SaveMode = "eQA";
                v.ExtraInfo = "YTl12vOW0NM9gD5";
                v.HandlerInfo = "h5ruMJpFWt1tPQbq";
                context.Set<FileAttachment>().Add(v);
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

                v.Email = "5UfUs4yibSt4JED2Q0gnL1LA0fnuzxyzGpyc";
                v.Gender = WalkingTec.Mvvm.Core.GenderEnum.Male;
                v.CellPhone = "fncsN1Vi1";
                v.HomePhone = "aAyc0iX7d7F";
                v.Address = "8sMGNxtw9oLNQEeWtefHxGoFi";
                v.ZipCode = "r7EvQSh4a1Tj5D9H";
                v.UnitWorkID = AddUnitWork();
                v.OpenID = "QMCOcDiUBBFk7n";
                v.ITCode = "3I3t9PqWXBV4JWvBuqHssDNqqj0tyM9j";
                v.Password = "vIlQZRN3Bo9Zh6kFqL";
                v.Name = "R6A3kdyqCvV6ujIQIFBLUXXc2ThSWpeZo";
                v.IsValid = false;
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
