using System;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Data;
using YS.Admin.Data.Repository;
using YS.Admin.Entity.SiteManage;
using YS.Admin.Model.Param.SiteManage;
using System.Drawing;
using QRCoder;
using System.Drawing.Imaging;
using ThoughtWorks.QRCode.Codec.Data;
using System.IO;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TagLib.Ogg;
using System.Drawing.Text;

namespace YS.Admin.Service.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-09 13:08
    /// 描 述：证书管理服务类
    /// </summary>
    public class CertificateService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<CertificateEntity>> GetList(CertificateListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<CertificateEntity>> GetPageList(CertificateListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<CertificateEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<CertificateEntity>(id);
        }
        public async Task<CertificateEntity> IDCodeGetEntity(string IDCode)
        {
            var expression = LinqExtensions.True<CertificateEntity>();
            expression = expression.And(d => d.IDCode == IDCode);
            expression = expression.And(d => d.BaseIsDelete == 0);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList().FirstOrDefault();
        }
        #endregion

        private Bitmap ByteArrayToBitmap(byte[] byteArray)
        {
            try
            {
                // 创建 MemoryStream 从 byte 数组
                using (MemoryStream ms = new MemoryStream(byteArray))
                {
                    // 从 MemoryStream 创建 Bitmap
                    return new Bitmap(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"转换 byte 数组为 Bitmap 时发生错误: {ex.Message}");
                return null;
            }
        }
        #region 提交数据
        public async Task SaveForm(CertificateEntity entity)
        {


            string fontFilePath = Directory.GetCurrentDirectory() + "\\Font\\SourceHanSansCN-Regular.otf"; // 字体文件路径

            // 创建一个 PrivateFontCollection 实例
            PrivateFontCollection privateFontCollection = new PrivateFontCollection();
            privateFontCollection.AddFontFile(fontFilePath);

            // 获取自定义字体家族
            FontFamily fontFamily = privateFontCollection.Families[0];


            Brush brush = Brushes.Black; // 文字颜色


            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                //生成IDcode
                //GZ + 年份 + 六位自增序号
                string starcode = "GZ";
                string year = DateTime.Now.Year.ToString();
                var expression = LinqExtensions.True<CertificateEntity>();
                var t = await this.BaseRepository().FindList(expression);
                string endcode = (t.Count() + 1).ToString().PadLeft(6, '0');
                entity.IDCode = starcode + year + endcode;


                //从全局配置中获取证书图片
                string ConfigBaseCertificate = GlobalContext.SystemConfig.BaseCertificate;
                entity.BaseCertificate = "\\upload\\Certificate\\" + entity.IDCode + ".png";
                //生成二维码
                byte[] code = GetPTQRCode("https://www.gzpm010/zs/" + entity.IDCode, 20);

                // 获取绝对路径
                string imagePath = GlobalContext.HostingEnvironment.ContentRootPath + ConfigBaseCertificate;
                string saveImagePath = GlobalContext.HostingEnvironment.ContentRootPath + entity.BaseCertificate;


                using (Bitmap baseImage = new Bitmap(imagePath))
                {
                    // 创建绘图对象
                    using (Graphics graphics = Graphics.FromImage(baseImage))
                    {
                        // 加载叠加图片
                        using (Bitmap overlayImage = new Bitmap(ByteArrayToBitmap(code)))
                        {
                            // 将叠加图片绘制到基础图片上
                            graphics.DrawImage(overlayImage, 1137, 1855, 205, 205);
                        }

                        // 设置绘图质量
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                        // 绘制文字
                        {
                            //编号
                            Font font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel); // 文字字体和大小
                            PointF textPosition = new PointF(1684, 915); // 文字位置 
                            graphics.DrawString(entity.IDCode, font, brush, textPosition);
                        }
                        {
                            //描述文本
                            Font font = new Font(fontFamily, 42, FontStyle.Bold, GraphicsUnit.Pixel); // 文字字体和大小
                            PointF textPosition = new PointF(548, 1220); // 文字位置 

                            graphics.DrawString(entity.Details, font, brush, textPosition);
                        }
                        {
                            //鉴定机构
                            Font font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel); // 文字字体和大小
                            PointF textPosition = new PointF(755, 1850); // 文字位置 

                            graphics.DrawString(entity.AppraisalInstitution, font, brush, textPosition);
                        }

                        {
                            //备案名称
                            Font font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel); // 文字字体和大小
                            PointF textPosition = new PointF(755, 1935); // 文字位置 

                            graphics.DrawString(entity.RecordName, font, brush, textPosition);
                        }
                        {
                            //鉴定结果
                            Font font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel); // 文字字体和大小
                            PointF textPosition = new PointF(755, 2018); // 文字位置 

                            graphics.DrawString(entity.IdentificationResult, font, brush, textPosition);
                        }
                        {
                            //日期
                            Font font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel); // 文字字体和大小
                            PointF textPosition = new PointF(1648, 2514); // 文字位置 

                            DateTime dt = entity.BaseCreateTime.Value;
                            graphics.DrawString(dt.ToString("yyyy年MM月dd日"), font, brush, textPosition);
                        }


                        {
                            //签名
                            Font font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel); // 文字字体和大小
                            PointF textPosition = new PointF(505, 2514); // 文字位置 
                            string Appraisal = entity.Appraisal1 + "      " + entity.Appraisal2;
                            graphics.DrawString(Appraisal, font, brush, textPosition);
                        }
                    }
                    // 保存合成后的图片
                    baseImage.Save(saveImagePath, ImageFormat.Jpeg);
                }

                await this.BaseRepository().Insert(entity);
            }
            else
            {

                await entity.Modify();
                CertificateEntity ce = await this.BaseRepository().FindEntity<CertificateEntity>(entity.Id.ParseToLong());


                //从全局配置中获取证书图片
                string ConfigBaseCertificate = GlobalContext.SystemConfig.BaseCertificate;
                //生成二维码
                byte[] code = GetPTQRCode("https://www.gzpm010/zs/" + ce.IDCode, 20);
                entity.BaseCertificate = "\\upload\\Certificate\\" + ce.IDCode + ".png";

                 
                // 获取绝对路径
                string imagePath = GlobalContext.HostingEnvironment.ContentRootPath + ConfigBaseCertificate;
                string saveImagePath = GlobalContext.HostingEnvironment.ContentRootPath + entity.BaseCertificate;


                using (Bitmap baseImage = new Bitmap(imagePath))
                {
                    // 创建绘图对象
                    using (Graphics graphics = Graphics.FromImage(baseImage))
                    {
                        // 加载叠加图片
                        using (Bitmap overlayImage = new Bitmap(ByteArrayToBitmap(code)))
                        {
                            // 将叠加图片绘制到基础图片上
                            graphics.DrawImage(overlayImage, 1137, 1855, 205, 205);
                        }

                        // 设置绘图质量
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                        // 绘制文字
                        {
                            //编号
                            Font font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel); // 文字字体和大小
                            PointF textPosition = new PointF(1684, 915); // 文字位置 
                            graphics.DrawString(ce.IDCode, font, brush, textPosition);
                        }
                        {
                            //描述文本
                            Font font = new Font(fontFamily, 42, FontStyle.Bold, GraphicsUnit.Pixel); // 文字字体和大小
                            PointF textPosition = new PointF(548, 1220); // 文字位置 
                            graphics.DrawString(entity.Details, font, brush, textPosition);
                        }
                        {
                            //鉴定机构
                            Font font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel); // 文字字体和大小
                            PointF textPosition = new PointF(755, 1850); // 文字位置 
                            graphics.DrawString(entity.AppraisalInstitution, font, brush, textPosition);
                        }

                        {
                            //备案名称
                            Font font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel); // 文字字体和大小
                            PointF textPosition = new PointF(755, 1935); // 文字位置 
                            graphics.DrawString(entity.RecordName, font, brush, textPosition);
                        }
                        {
                            //鉴定结果
                            Font font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel); // 文字字体和大小
                            PointF textPosition = new PointF(755, 2018); // 文字位置 
                            graphics.DrawString(entity.IdentificationResult, font, brush, textPosition);
                        }
                        {
                            //日期
                            Font font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel); // 文字字体和大小
                            PointF textPosition = new PointF(1648, 2514); // 文字位置 
                                                                          //由于是修改没有创建数据 所以通过数据库重新查询
                            DateTime dt = ce.BaseCreateTime.Value;
                            graphics.DrawString(dt.ToString("yyyy年MM月dd日"), font, brush, textPosition);
                        }


                        {
                            //签名
                            Font font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel); // 文字字体和大小
                            PointF textPosition = new PointF(505, 2514); // 文字位置 
                            string Appraisal = entity.Appraisal1 + "      " + entity.Appraisal2;
                            graphics.DrawString(Appraisal, font, brush, textPosition);
                        }
                    }

                    // 保存合成后的图片
                    baseImage.Save(saveImagePath, ImageFormat.Jpeg);
                }

                await this.BaseRepository().Update(entity);
            }
        }

        public async Task SaveForms(List<CertificateEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            //将所有的id 对应的 BaseIsDelete 全部都修改成1
            foreach (var id in idArr)
            {
                var entity = await this.BaseRepository().FindEntity<CertificateEntity>(id);
                entity.BaseIsDelete = 1;
                await entity.Modify();
                await this.BaseRepository().Update(entity);
            }
        }
        public async Task<int> Update(Expression<Func<CertificateEntity, bool>> whereLambda, Expression<Func<CertificateEntity, CertificateEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">存储内容</param>
        /// <param name="pixel">像素大小</param>
        /// <returns></returns>
        public static byte[] GetPTQRCode(string url, int pixel)
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData codeData = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M, true);
            QRCoder.PngByteQRCode qrcode = new QRCoder.PngByteQRCode(codeData);
            byte[] qrImage = qrcode.GetGraphic(pixel, Color.Black, Color.White, true);
            return qrImage;
        }
        private Expression<Func<CertificateEntity, bool>> ListFilter(CertificateListParam param)
        {
            var expression = LinqExtensions.True<CertificateEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.IDCode))
                {
                    expression = expression.And(d => d.IDCode.Contains(param.IDCode));
                }
            }
            expression = expression.And(d => d.BaseIsDelete == 0);
            return expression;
        }
        #endregion
    }
}
