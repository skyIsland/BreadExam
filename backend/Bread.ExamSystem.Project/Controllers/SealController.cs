using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using Bread.ExamSystem.Project.ViewModel.SealVMs;
using Bread.ExamSystem.Project.Model;
using Bread.Util;
using System.Drawing;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace Bread.ExamSystem.Project.Controllers
{
    
    [ActionDescription("印章管理")]
    public partial class SealController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SealController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

        }

        #region 自定义方法

        /// <summary>
        /// 印章生成
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        /// <returns></returns>
        public async Task<IActionResult> GeneratePic(string name1, string name2)
        {
            string dw = "请输入单位";
            string lx = "专用章";

            if (!string.IsNullOrWhiteSpace(name1))
            {
                dw = name1;
            }
            if (!string.IsNullOrWhiteSpace(name2))
            {
                lx = name2 + "章";
            }
            string webRootPath = _hostingEnvironment.WebRootPath;
            Image img = Image.FromFile(webRootPath + "//images//zj//tim.JPG");
            Image imglogo = Image.FromFile(webRootPath + "//images//zj//logo.png");
            int width = img.Size.Width;   // 图片的宽度
            int height = img.Size.Height;   // 图片的高度
            Graphics g = Graphics.FromImage(img);
            StringFormat TitleFormat = new StringFormat();
            StringFormat TitleFormat1 = new StringFormat();
            TitleFormat.Alignment = StringAlignment.Center; //居中
            TitleFormat1.LineAlignment = StringAlignment.Far;
            g.DrawImage(imglogo, 230, 150, 150, 150);


            int iSize = 200;
            int Var_Font_Size = (iSize * 8) / 100;
            int Star_Font_Size = (iSize * 15) / 100;
            int circularity_W = iSize / 50;                          //设置圆画笔的粗细
            int tem_Line = iSize;
            //实例化并设置圆的绘制区域
            Rectangle rect = new Rectangle(circularity_W, circularity_W, tem_Line - (circularity_W << 1), tem_Line - (circularity_W << 1));
            Font Var_Font = new Font("Arial", Var_Font_Size, FontStyle.Bold);       //实例化并设置字符串的字体样式
            Font star_Font = new Font("Arial", Star_Font_Size, FontStyle.Regular);  //实例化并设置星号的字体样式
            string star_Str = "★";
            Image MyImage = new Bitmap(iSize, iSize);
            Graphics g1 = Graphics.FromImage(MyImage); //实例化Graphics类
            g1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;     //消除绘制图形的锯齿
            g1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g1.Clear(Color.Transparent);                                           //以白色清空panel1控件的背景
            Pen myPen = new Pen(Color.Red, circularity_W);                  //设置画笔的颜色
            g1.DrawEllipse(myPen, rect);                                     //绘制圆
            SizeF Var_Size1 = new SizeF(rect.Width, rect.Width);            //实例化SizeF类
            Var_Size1 = g1.MeasureString(star_Str, star_Font);               //对指定的字符串进行测量
            g1.DrawString(star_Str, star_Font, myPen.Brush, new PointF(((float)(rect.Width) / 2) + circularity_W - (Var_Size1.Width / 2),
                ((float)(rect.Height) / 2) + ((Var_Size1.Height) / 5)));    //指定位置绘制星号
            SizeF Var_Size2 = new SizeF(rect.Width, rect.Width);            //实例化SizeF类
            Var_Size2 = g1.MeasureString(lx, Var_Font);
            g1.DrawString(lx, Var_Font, myPen.Brush, new PointF(((float)(rect.Width) / 2) + circularity_W - (Var_Size2.Width / 2),
                ((float)(rect.Height) / 2) + Var_Size1.Height + (Var_Size2.Height / 10)));//指定位置绘制公章类型字符串

            string str_Name = dw;
            int length = str_Name.Length;
            float angle = -(((length * 20) / 2) - 10);                    //设置文字的旋转角度
            SizeF Var_Size3 = new SizeF(rect.Width, rect.Width);
            Var_Size3 = g1.MeasureString(str_Name.Substring(0, 1), Var_Font);

            for (int i = 0; i < length; i++)
            {
                //将指定的平移添加到g的变换矩阵前
                g1.TranslateTransform((float)tem_Line / 2, (float)tem_Line / 2);
                g1.RotateTransform(angle);                                   //将指定的旋转用于Graphics类的变换矩阵
                Brush myBrush = Brushes.Red;                                //定义画笔颜色
                g1.DrawString(str_Name.Substring(i, 1), Var_Font, myBrush, -(float)circularity_W / 2 - Var_Size3.Width / 2, (((float)tem_Line) * -45 / 100));   //显示旋转文字
                g1.ResetTransform();                                         //将Graphics类的全局变换矩阵重置为单位矩阵
                angle += 20;                                                //设置下一个文字的角度
            }

            g.DrawImage(MyImage, 350, height - 250, 150, 150);
            var imgdata = ImgData.PhotoImageInsert(img);
            if (imgdata == null)
            {
                imgdata = new byte[0];
            }
            await Response.Body.WriteAsync(imgdata, 0, imgdata.Count());
            return new EmptyResult();
        }
        public async Task<IActionResult> SavePic(string name1, string name2)
        {
            try
            {
                string dw = "请输入单位";
                string lx = "专用章";
                #region 样本生成
                if (!string.IsNullOrWhiteSpace(name1))
                {
                    dw = name1;
                }
                if (!string.IsNullOrWhiteSpace(name2))
                {
                    lx = name2 + "章";
                }
                string webRootPath = _hostingEnvironment.WebRootPath;
                Image img = Image.FromFile(webRootPath + "//images//zj//tim.JPG");
                Image imglogo = Image.FromFile(webRootPath + "//images//zj//logo.png");
                int width = img.Size.Width;   // 图片的宽度
                int height = img.Size.Height;   // 图片的高度
                Graphics g = Graphics.FromImage(img);
                StringFormat TitleFormat = new StringFormat();
                StringFormat TitleFormat1 = new StringFormat();
                TitleFormat.Alignment = StringAlignment.Center; //居中
                TitleFormat1.LineAlignment = StringAlignment.Far;
                g.DrawImage(imglogo, 150, 150, 300, 60);
                int iSize = 200;
                int Var_Font_Size = (iSize * 8) / 100;
                int Star_Font_Size = (iSize * 15) / 100;
                int circularity_W = iSize / 50;                          //设置圆画笔的粗细
                int tem_Line = iSize;
                //实例化并设置圆的绘制区域
                Rectangle rect = new Rectangle(circularity_W, circularity_W, tem_Line - (circularity_W << 1), tem_Line - (circularity_W << 1));
                Font Var_Font = new Font("Arial", Var_Font_Size, FontStyle.Bold);       //实例化并设置字符串的字体样式
                Font star_Font = new Font("Arial", Star_Font_Size, FontStyle.Regular);  //实例化并设置星号的字体样式
                string star_Str = "★";
                Image MyImage = new Bitmap(iSize, iSize);
                Graphics g1 = Graphics.FromImage(MyImage); //实例化Graphics类
                g1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;     //消除绘制图形的锯齿
                g1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g1.Clear(Color.Transparent);                                           //以白色清空panel1控件的背景
                Pen myPen = new Pen(Color.Red, circularity_W);                  //设置画笔的颜色
                g1.DrawEllipse(myPen, rect);                                     //绘制圆
                SizeF Var_Size1 = new SizeF(rect.Width, rect.Width);            //实例化SizeF类
                Var_Size1 = g1.MeasureString(star_Str, star_Font);               //对指定的字符串进行测量
                g1.DrawString(star_Str, star_Font, myPen.Brush, new PointF(((float)(rect.Width) / 2) + circularity_W - (Var_Size1.Width / 2),
                    ((float)(rect.Height) / 2) + ((Var_Size1.Height) / 5)));    //指定位置绘制星号
                SizeF Var_Size2 = new SizeF(rect.Width, rect.Width);            //实例化SizeF类
                Var_Size2 = g1.MeasureString(lx, Var_Font);
                g1.DrawString(lx, Var_Font, myPen.Brush, new PointF(((float)(rect.Width) / 2) + circularity_W - (Var_Size2.Width / 2),
                    ((float)(rect.Height) / 2) + Var_Size1.Height + (Var_Size2.Height / 10)));//指定位置绘制公章类型字符串

                string str_Name = dw;
                int length = str_Name.Length;
                float angle = -(((length * 20) / 2) - 10);                    //设置文字的旋转角度
                SizeF Var_Size3 = new SizeF(rect.Width, rect.Width);
                Var_Size3 = g1.MeasureString(str_Name.Substring(0, 1), Var_Font);

                for (int i = 0; i < length; i++)
                {
                    //将指定的平移添加到g的变换矩阵前
                    g1.TranslateTransform((float)tem_Line / 2, (float)tem_Line / 2);
                    g1.RotateTransform(angle);                                   //将指定的旋转用于Graphics类的变换矩阵
                    Brush myBrush = Brushes.Red;                                //定义画笔颜色
                    g1.DrawString(str_Name.Substring(i, 1), Var_Font, myBrush, -(float)circularity_W / 2 - Var_Size3.Width / 2, (((float)tem_Line) * -45 / 100));   //显示旋转文字
                    g1.ResetTransform();                                         //将Graphics类的全局变换矩阵重置为单位矩阵
                    angle += 20;                                                //设置下一个文字的角度
                }

                #endregion
                g.DrawImage(MyImage, 350, height - 250, 150, 150);
                var imgdata = ImgData.PhotoImageInsert(img);
                FileAttachment file = new FileAttachment
                {
                    UploadTime =DateTime.Now,
                    FileName = name1 + name2 + ".JPG",
                    FileExt = ".JPG",
                    Length = imgdata.Length,                 
                    FileData = imgdata,
                    SaveMode = "Database"
                };
                await DC.Set<FileAttachment>().AddAsync(file);
                int q = await DC.SaveChangesAsync();
                if (q > 0)
                {
                    return Content(file.ID.ToString());
                }
                return Content("上传失败");
            }
            catch (Exception)
            {

                return Content("上传失败");
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveModel(string name1, string name2, string imgid)
        {
            if (string.IsNullOrWhiteSpace(imgid))
            {
                return Content("证件未完成制作");
            }
            Seal seal = new Seal() { Name = name1, SType = name2, PhotoId = new Guid(imgid) };
            await DC.Set<Seal>().AddAsync(seal);
            int q = await DC.SaveChangesAsync();
            if (q > 0)
            {
                return Content("制作成功");
            }
            return Content("制作失败");
        }

        #endregion

        #region Search
        [ActionDescription("Sys.Search")]
        public ActionResult Index()
        {
            var vm = Wtm.CreateVM<SealListVM>();
            return PartialView(vm);
        }

        [ActionDescription("Sys.Search")]
        [HttpPost]
        public string Search(SealSearcher searcher)
        {
            var vm = Wtm.CreateVM<SealListVM>(passInit: true);
            if (ModelState.IsValid)
            {
                vm.Searcher = searcher;
                return vm.GetJson(false);
            }
            else
            {
                return vm.GetError();
            }
        }

        #endregion

        #region Create
        [ActionDescription("Sys.Create")]
        public ActionResult Create()
        {
            var vm = Wtm.CreateVM<SealVM>();
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.Create")]
        public ActionResult Create(SealVM vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    vm.DoReInit();
                    return PartialView(vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGrid();
                }
            }
        }
        #endregion

        #region Edit
        [ActionDescription("Sys.Edit")]
        public ActionResult Edit(string id)
        {
            var vm = Wtm.CreateVM<SealVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Edit")]
        [HttpPost]
        [ValidateFormItemOnly]
        public ActionResult Edit(SealVM vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                vm.DoEdit();
                if (!ModelState.IsValid)
                {
                    vm.DoReInit();
                    return PartialView(vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGridRow(vm.Entity.ID);
                }
            }
        }
        #endregion

        #region Delete
        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<SealVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        [HttpPost]
        public ActionResult Delete(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<SealVM>(id);
            vm.DoDelete();
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid();
            }
        }
        #endregion

        #region Details
        [ActionDescription("Sys.Details")]
        public ActionResult Details(string id)
        {
            var vm = Wtm.CreateVM<SealVM>(id);
            return PartialView(vm);
        }
        #endregion

        #region BatchEdit
        [HttpPost]
        [ActionDescription("Sys.BatchEdit")]
        public ActionResult BatchEdit(string[] IDs)
        {
            var vm = Wtm.CreateVM<SealBatchVM>(Ids: IDs);
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.BatchEdit")]
        public ActionResult DoBatchEdit(SealBatchVM vm, IFormCollection nouse)
        {
            if (!ModelState.IsValid || !vm.DoBatchEdit())
            {
                return PartialView("BatchEdit",vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.BatchEditSuccess", vm.Ids.Length]);
            }
        }
        #endregion

        #region BatchDelete
        [HttpPost]
        [ActionDescription("Sys.BatchDelete")]
        public ActionResult BatchDelete(string[] IDs)
        {
            var vm = Wtm.CreateVM<SealBatchVM>(Ids: IDs);
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.BatchDelete")]
        public ActionResult DoBatchDelete(SealBatchVM vm, IFormCollection nouse)
        {
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return PartialView("BatchDelete",vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.BatchDeleteSuccess", vm.Ids.Length]);
            }
        }
        #endregion

        #region Import
		[ActionDescription("Sys.Import")]
        public ActionResult Import()
        {
            var vm = Wtm.CreateVM<SealImportVM>();
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.Import")]
        public ActionResult Import(SealImportVM vm, IFormCollection nouse)
        {
            if (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData())
            {
                return PartialView(vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.ImportSuccess", vm.EntityList.Count.ToString()]);
            }
        }
        #endregion

        [ActionDescription("Sys.Export")]
        [HttpPost]
        public IActionResult ExportExcel(SealListVM vm)
        {
            return vm.GetExportData();
        }

    }
}
