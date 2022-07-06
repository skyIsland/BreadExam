using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Bread.ExamSystem.Project.Controllers;
using Bread.ExamSystem.Project.ViewModel.CollectionVMs;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.DataAccess;


namespace Bread.ExamSystem.Project.Test
{
    [TestClass]
    public class CollectionControllerTest
    {
        private CollectionController _controller;
        private string _seed;

        public CollectionControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<CollectionController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as CollectionListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(CollectionVM));

            CollectionVM vm = rv.Model as CollectionVM;
            Collection v = new Collection();
			
            v.ID = 79;
            v.QuestionID = AddQuestion();
            v.FrameworkUserID = AddFrameworkUser();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Collection>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 79);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Collection v = new Collection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 79;
                v.QuestionID = AddQuestion();
                v.FrameworkUserID = AddFrameworkUser();
                context.Set<Collection>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(CollectionVM));

            CollectionVM vm = rv.Model as CollectionVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new Collection();
            v.ID = vm.Entity.ID;
       		
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.QuestionID", "");
            vm.FC.Add("Entity.FrameworkUserID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Collection>().Find(v.ID);
 				
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Collection v = new Collection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 79;
                v.QuestionID = AddQuestion();
                v.FrameworkUserID = AddFrameworkUser();
                context.Set<Collection>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(CollectionVM));

            CollectionVM vm = rv.Model as CollectionVM;
            v = new Collection();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Collection>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Collection v = new Collection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 79;
                v.QuestionID = AddQuestion();
                v.FrameworkUserID = AddFrameworkUser();
                context.Set<Collection>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            Collection v1 = new Collection();
            Collection v2 = new Collection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 79;
                v1.QuestionID = AddQuestion();
                v1.FrameworkUserID = AddFrameworkUser();
                v2.ID = 72;
                v2.QuestionID = v1.QuestionID; 
                v2.FrameworkUserID = v1.FrameworkUserID; 
                context.Set<Collection>().Add(v1);
                context.Set<Collection>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(CollectionBatchVM));

            CollectionBatchVM vm = rv.Model as CollectionBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Collection>().Find(v1.ID);
                var data2 = context.Set<Collection>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            Collection v1 = new Collection();
            Collection v2 = new Collection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 79;
                v1.QuestionID = AddQuestion();
                v1.FrameworkUserID = AddFrameworkUser();
                v2.ID = 72;
                v2.QuestionID = v1.QuestionID; 
                v2.FrameworkUserID = v1.FrameworkUserID; 
                context.Set<Collection>().Add(v1);
                context.Set<Collection>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(CollectionBatchVM));

            CollectionBatchVM vm = rv.Model as CollectionBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Collection>().Find(v1.ID);
                var data2 = context.Set<Collection>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as CollectionListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Int32 AddQuestionType()
        {
            QuestionType v = new QuestionType();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ID = 93;
                v.Name = "qyxnvdm";
                v.Remark = "TVkorA6tEpBMFV5Kh";
                context.Set<QuestionType>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Int32 AddQuestion()
        {
            Question v = new Question();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ID = 16;
                v.QuestionText = "E3iZm";
                v.Anwser = "CDMpMQUEKljrc8";
                v.OptionA = "1j451Aq36KXzB5bcTs";
                v.OptionB = "g";
                v.OptionC = "1FUUvNqi";
                v.OptionD = "X2Uto4fOQ6ReojAG";
                v.QuestionTypeID = AddQuestionType();
                v.Subject = "NNhnqrEKhfmr7ylTo";
                v.Pars = "pMfLPMELc1ZqbdTma";
                context.Set<Question>().Add(v);
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

                v.UnitWorkName = "9fkbDYY";
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

                v.FileName = "pa";
                v.FileExt = "UIePp2a";
                v.Path = "d";
                v.Length = 85;
                v.UploadTime = DateTime.Parse("2023-05-20 23:09:03");
                v.SaveMode = "tCmAuleWYd";
                v.ExtraInfo = "y7XJJv8DOqZ";
                v.HandlerInfo = "w3OXF";
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

                v.Email = "eJnsUUd9Y6dHT0iTxkZtOL5qZD3jEYrpsrUCMFpMalQo";
                v.Gender = WalkingTec.Mvvm.Core.GenderEnum.Female;
                v.CellPhone = "A1q";
                v.HomePhone = "iY8XQ067ESImjef7";
                v.Address = "uWXPm8aLwdFFveINn3XVFHFMn4x9BpRnPShFg5hyNs7";
                v.ZipCode = "B6frN";
                v.UnitWorkID = AddUnitWork();
                v.OpenID = "fiYOlIk1BAXRZr";
                v.ITCode = "xxgBXwPOxmYnwsRHHRl1aCHKqrN4yY";
                v.Password = "W94";
                v.Name = "h7cXK49DoKfCzwBrqDsKUkwPKV01zpw8kG2znH9YiaQ9K";
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
