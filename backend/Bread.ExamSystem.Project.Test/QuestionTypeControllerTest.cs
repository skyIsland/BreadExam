using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Bread.ExamSystem.Project.Controllers;
using Bread.ExamSystem.Project.ViewModel.QuestionTypeVMs;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.DataAccess;


namespace Bread.ExamSystem.Project.Test
{
    [TestClass]
    public class QuestionTypeControllerTest
    {
        private QuestionTypeController _controller;
        private string _seed;

        public QuestionTypeControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<QuestionTypeController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as QuestionTypeListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(QuestionTypeVM));

            QuestionTypeVM vm = rv.Model as QuestionTypeVM;
            QuestionType v = new QuestionType();
			
            v.ID = 77;
            v.Name = "HJ";
            v.Remark = "8k2ILqcswjFttMsHQc";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<QuestionType>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 77);
                Assert.AreEqual(data.Name, "HJ");
                Assert.AreEqual(data.Remark, "8k2ILqcswjFttMsHQc");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            QuestionType v = new QuestionType();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 77;
                v.Name = "HJ";
                v.Remark = "8k2ILqcswjFttMsHQc";
                context.Set<QuestionType>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(QuestionTypeVM));

            QuestionTypeVM vm = rv.Model as QuestionTypeVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new QuestionType();
            v.ID = vm.Entity.ID;
       		
            v.Name = "4nO1y";
            v.Remark = "Tr9B";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Remark", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<QuestionType>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "4nO1y");
                Assert.AreEqual(data.Remark, "Tr9B");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            QuestionType v = new QuestionType();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 77;
                v.Name = "HJ";
                v.Remark = "8k2ILqcswjFttMsHQc";
                context.Set<QuestionType>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(QuestionTypeVM));

            QuestionTypeVM vm = rv.Model as QuestionTypeVM;
            v = new QuestionType();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<QuestionType>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            QuestionType v = new QuestionType();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 77;
                v.Name = "HJ";
                v.Remark = "8k2ILqcswjFttMsHQc";
                context.Set<QuestionType>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            QuestionType v1 = new QuestionType();
            QuestionType v2 = new QuestionType();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 77;
                v1.Name = "HJ";
                v1.Remark = "8k2ILqcswjFttMsHQc";
                v2.ID = 85;
                v2.Name = "4nO1y";
                v2.Remark = "Tr9B";
                context.Set<QuestionType>().Add(v1);
                context.Set<QuestionType>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(QuestionTypeBatchVM));

            QuestionTypeBatchVM vm = rv.Model as QuestionTypeBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<QuestionType>().Find(v1.ID);
                var data2 = context.Set<QuestionType>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            QuestionType v1 = new QuestionType();
            QuestionType v2 = new QuestionType();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 77;
                v1.Name = "HJ";
                v1.Remark = "8k2ILqcswjFttMsHQc";
                v2.ID = 85;
                v2.Name = "4nO1y";
                v2.Remark = "Tr9B";
                context.Set<QuestionType>().Add(v1);
                context.Set<QuestionType>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(QuestionTypeBatchVM));

            QuestionTypeBatchVM vm = rv.Model as QuestionTypeBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<QuestionType>().Find(v1.ID);
                var data2 = context.Set<QuestionType>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as QuestionTypeListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
