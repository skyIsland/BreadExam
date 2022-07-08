using System;
using System.IO;

namespace Bread.Util
{
    /// <summary>
    /// 图片二进制相互转化
    /// </summary>
    public static class ImgData
    {
        /// <summary>
        /// 图片转二进制 
        /// </summary>
        /// <param name="imagepath">路径</param>
        /// <returns></returns>
        public static byte[] GetPictureData(string imagepath)
        {
            /**/////根据图片文件的路径使用文件流打开，并保存为byte[] 
            FileStream fs = new FileStream(imagepath, FileMode.Open);//可以是其他重载方法 
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return byData;
        }
        /// <summary>
        /// 图片转二进制 
        /// </summary>
        /// <param name="imgPhoto">图片</param>
        /// <returns></returns>
        public static byte[] PhotoImageInsert(System.Drawing.Image imgPhoto)
        {
            //将Image转换成流数据，并保存为byte[] 
            MemoryStream mstream = new MemoryStream();
            imgPhoto.Save(mstream, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] byData = new Byte[mstream.Length];
            mstream.Position = 0;
            mstream.Read(byData, 0, byData.Length);
            mstream.Close();
            return byData;
        }
        /// <summary>
        /// 二进制转图片
        /// </summary>
        /// <param name="streamByte"></param>
        /// <returns></returns>
        public static System.Drawing.Image ReturnPhoto(byte[] streamByte)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }
    }
}
