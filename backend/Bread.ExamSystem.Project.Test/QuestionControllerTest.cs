using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Bread.ExamSystem.Project.Controllers;
using Bread.ExamSystem.Project.ViewModel.QuestionVMs;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.DataAccess;


namespace Bread.ExamSystem.Project.Test
{
    [TestClass]
    public class QuestionControllerTest
    {
        private QuestionController _controller;
        private string _seed;

        public QuestionControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<QuestionController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as QuestionListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(QuestionVM));

            QuestionVM vm = rv.Model as QuestionVM;
            Question v = new Question();
			
            v.ID = 84;
            v.QuestionText = "xb";
            v.Anwser = "eIdRymfs";
            v.OptionA = "Yu5z6oRObfPA";
            v.OptionB = "Wra9t1b1kWalER";
            v.OptionC = "LSgrOmPHTvucYb1";
            v.OptionD = "zingr";
            v.QuestionTypeID = AddQuestionType();
            v.Subject = "oz";
            v.Pars = "r33sPXRDfVzgRUhK";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Question>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 84);
                Assert.AreEqual(data.QuestionText, "xb");
                Assert.AreEqual(data.Anwser, "eIdRymfs");
                Assert.AreEqual(data.OptionA, "Yu5z6oRObfPA");
                Assert.AreEqual(data.OptionB, "Wra9t1b1kWalER");
                Assert.AreEqual(data.OptionC, "LSgrOmPHTvucYb1");
                Assert.AreEqual(data.OptionD, "zingr");
                Assert.AreEqual(data.Subject, "oz");
                Assert.AreEqual(data.Pars, "r33sPXRDfVzgRUhK");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Question v = new Question();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 84;
                v.QuestionText = "xb";
                v.Anwser = "eIdRymfs";
                v.OptionA = "Yu5z6oRObfPA";
                v.OptionB = "Wra9t1b1kWalER";
                v.OptionC = "LSgrOmPHTvucYb1";
                v.OptionD = "zingr";
                v.QuestionTypeID = AddQuestionType();
                v.Subject = "oz";
                v.Pars = "r33sPXRDfVzgRUhK";
                context.Set<Question>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(QuestionVM));

            QuestionVM vm = rv.Model as QuestionVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new Question();
            v.ID = vm.Entity.ID;
       		
            v.QuestionText = "3";
            v.Anwser = "O0Aaib2C8dM9TKM4p";
            v.OptionA = "jNO4cxnGfe7w8D";
            v.OptionB = "iD8ckIvJ4G7QkUEs";
            v.OptionC = "d6uy";
            v.OptionD = "9cbf8FED";
            v.Subject = "JEEULqqhI7";
            v.Pars = "JGug3kVkxdrBuiU";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.QuestionText", "");
            vm.FC.Add("Entity.Anwser", "");
            vm.FC.Add("Entity.OptionA", "");
            vm.FC.Add("Entity.OptionB", "");
            vm.FC.Add("Entity.OptionC", "");
            vm.FC.Add("Entity.OptionD", "");
            vm.FC.Add("Entity.QuestionTypeID", "");
            vm.FC.Add("Entity.Subject", "");
            vm.FC.Add("Entity.Pars", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Question>().Find(v.ID);
 				
                Assert.AreEqual(data.QuestionText, "3");
                Assert.AreEqual(data.Anwser, "O0Aaib2C8dM9TKM4p");
                Assert.AreEqual(data.OptionA, "jNO4cxnGfe7w8D");
                Assert.AreEqual(data.OptionB, "iD8ckIvJ4G7QkUEs");
                Assert.AreEqual(data.OptionC, "d6uy");
                Assert.AreEqual(data.OptionD, "9cbf8FED");
                Assert.AreEqual(data.Subject, "JEEULqqhI7");
                Assert.AreEqual(data.Pars, "JGug3kVkxdrBuiU");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Question v = new Question();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 84;
                v.QuestionText = "xb";
                v.Anwser = "eIdRymfs";
                v.OptionA = "Yu5z6oRObfPA";
                v.OptionB = "Wra9t1b1kWalER";
                v.OptionC = "LSgrOmPHTvucYb1";
                v.OptionD = "zingr";
                v.QuestionTypeID = AddQuestionType();
                v.Subject = "oz";
                v.Pars = "r33sPXRDfVzgRUhK";
                context.Set<Question>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(QuestionVM));

            QuestionVM vm = rv.Model as QuestionVM;
            v = new Question();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Question>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Question v = new Question();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 84;
                v.QuestionText = "xb";
                v.Anwser = "eIdRymfs";
                v.OptionA = "Yu5z6oRObfPA";
                v.OptionB = "Wra9t1b1kWalER";
                v.OptionC = "LSgrOmPHTvucYb1";
                v.OptionD = "zingr";
                v.QuestionTypeID = AddQuestionType();
                v.Subject = "oz";
                v.Pars = "r33sPXRDfVzgRUhK";
                context.Set<Question>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            Question v1 = new Question();
            Question v2 = new Question();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 84;
                v1.QuestionText = "xb";
                v1.Anwser = "eIdRymfs";
                v1.OptionA = "Yu5z6oRObfPA";
                v1.OptionB = "Wra9t1b1kWalER";
                v1.OptionC = "LSgrOmPHTvucYb1";
                v1.OptionD = "zingr";
                v1.QuestionTypeID = AddQuestionType();
                v1.Subject = "oz";
                v1.Pars = "r33sPXRDfVzgRUhK";
                v2.ID = 97;
                v2.QuestionText = "3";
                v2.Anwser = "O0Aaib2C8dM9TKM4p";
                v2.OptionA = "jNO4cxnGfe7w8D";
                v2.OptionB = "iD8ckIvJ4G7QkUEs";
                v2.OptionC = "d6uy";
                v2.OptionD = "9cbf8FED";
                v2.QuestionTypeID = v1.QuestionTypeID; 
                v2.Subject = "JEEULqqhI7";
                v2.Pars = "JGug3kVkxdrBuiU";
                context.Set<Question>().Add(v1);
                context.Set<Question>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(QuestionBatchVM));

            QuestionBatchVM vm = rv.Model as QuestionBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Question>().Find(v1.ID);
                var data2 = context.Set<Question>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            Question v1 = new Question();
            Question v2 = new Question();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 84;
                v1.QuestionText = "xb";
                v1.Anwser = "eIdRymfs";
                v1.OptionA = "Yu5z6oRObfPA";
                v1.OptionB = "Wra9t1b1kWalER";
                v1.OptionC = "LSgrOmPHTvucYb1";
                v1.OptionD = "zingr";
                v1.QuestionTypeID = AddQuestionType();
                v1.Subject = "oz";
                v1.Pars = "r33sPXRDfVzgRUhK";
                v2.ID = 97;
                v2.QuestionText = "3";
                v2.Anwser = "O0Aaib2C8dM9TKM4p";
                v2.OptionA = "jNO4cxnGfe7w8D";
                v2.OptionB = "iD8ckIvJ4G7QkUEs";
                v2.OptionC = "d6uy";
                v2.OptionD = "9cbf8FED";
                v2.QuestionTypeID = v1.QuestionTypeID; 
                v2.Subject = "JEEULqqhI7";
                v2.Pars = "JGug3kVkxdrBuiU";
                context.Set<Question>().Add(v1);
                context.Set<Question>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(QuestionBatchVM));

            QuestionBatchVM vm = rv.Model as QuestionBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Question>().Find(v1.ID);
                var data2 = context.Set<Question>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as QuestionListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Int32 AddQuestionType()
        {
            QuestionType v = new QuestionType();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ID = 65;
                v.Name = "h3xzu13kF63zDO";
                v.Remark = "a6qO0cAin20u";
                context.Set<QuestionType>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
