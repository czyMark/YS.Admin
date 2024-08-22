using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using ThoughtWorks.QRCode.Codec;

namespace YS.Admin.Util
{
	public class QRCodeHelper
	{
		public static Image create_two(string url)
		{
			//Bitmap bmp = new Bitmap(300,300);
			string enCodeString = url;//
			int size = 300;
			int border = 1;
			string content = enCodeString;
			System.Drawing.Image image = CreateQRCode(content,
				  QRCodeEncoder.ENCODE_MODE.BYTE,
				  QRCodeEncoder.ERROR_CORRECTION.M,
				  0,
				  5,
				  size,
				  border);
			System.IO.MemoryStream stream = new System.IO.MemoryStream();

			image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
			return image;
			//byte[] b = stream.GetBuffer();
			//return "data:image/jpg;base64," + Convert.ToBase64String(b);
		}
		public static string create_two2(string url)
		{
			//Bitmap bmp = new Bitmap(300,300);
			string enCodeString = url;//
			int size = 300;
			int border = 1;
			string content = enCodeString;
			System.Drawing.Image image = CreateQRCode(content,
				  QRCodeEncoder.ENCODE_MODE.BYTE,
				  QRCodeEncoder.ERROR_CORRECTION.M,
				  0,
				  5,
				  size,
				  border);
			System.IO.MemoryStream stream = new System.IO.MemoryStream();
			image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

			byte[] b = stream.GetBuffer();
			return "data:image/jpg;base64," + Convert.ToBase64String(b);
		}
		public static System.Drawing.Image CreateQRCode(string Content, QRCodeEncoder.ENCODE_MODE QRCodeEncodeMode, QRCodeEncoder.ERROR_CORRECTION QRCodeErrorCorrect, int QRCodeVersion, int QRCodeScale, int size, int border)
		{
			QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
			qrCodeEncoder.QRCodeEncodeMode = QRCodeEncodeMode;
			qrCodeEncoder.QRCodeErrorCorrect = QRCodeErrorCorrect;
			qrCodeEncoder.QRCodeScale = QRCodeScale;
			qrCodeEncoder.QRCodeVersion = QRCodeVersion;
			System.Drawing.Image image = qrCodeEncoder.Encode(Content);

			#region 根据设定的目标图片尺寸调整二维码QRCodeScale设置，并添加边框
			if (size > 0)
			{
				//当设定目标图片尺寸大于生成的尺寸时，逐步增大方格尺寸
				#region 当设定目标图片尺寸大于生成的尺寸时，逐步增大方格尺寸
				while (image.Width < size)
				{
					qrCodeEncoder.QRCodeScale++;
					System.Drawing.Image imageNew = qrCodeEncoder.Encode(Content);
					if (imageNew.Width < size)
					{
						image = new System.Drawing.Bitmap(imageNew);
						imageNew.Dispose();
						imageNew = null;
					}
					else
					{
						qrCodeEncoder.QRCodeScale--; //新尺寸未采用，恢复最终使用的尺寸
						imageNew.Dispose();
						imageNew = null;
						break;
					}
				}
				#endregion

				//当设定目标图片尺寸小于生成的尺寸时，逐步减小方格尺寸
				#region 当设定目标图片尺寸小于生成的尺寸时，逐步减小方格尺寸
				System.Drawing.Image imageNew2;
				while (image.Width > size && qrCodeEncoder.QRCodeScale > 1)
				{
					qrCodeEncoder.QRCodeScale--;
					imageNew2 = qrCodeEncoder.Encode(Content);
					image = new System.Drawing.Bitmap(imageNew2);
					imageNew2.Dispose();
					imageNew2 = null;
					if (image.Width < size)
					{
						break;
					}
				}
				#endregion

				//如果目标尺寸大于生成的图片尺寸，则为图片增加白边
				#region 如果目标尺寸大于生成的图片尺寸，则为图片增加白边
				if (image.Width <= size)
				{
					//根据参数设置二维码图片白边的最小宽度
					#region 根据参数设置二维码图片白边的最小宽度
					if (border > 0)
					{
						while (image.Width <= size && size - image.Width < border * 2 && qrCodeEncoder.QRCodeScale > 1)
						{
							qrCodeEncoder.QRCodeScale--;
							System.Drawing.Image imageNew = qrCodeEncoder.Encode(Content);
							image = new System.Drawing.Bitmap(imageNew);
							imageNew.Dispose();
							imageNew = null;
						}
					}
					#endregion
					//当目标图片尺寸大于二维码尺寸时，将二维码绘制在目标尺寸白色画布的中心位置
					if (image.Width < size)
					{
						//新建空白绘图
						System.Drawing.Bitmap panel = new System.Drawing.Bitmap(size, size);
						System.Drawing.Graphics graphic0 = System.Drawing.Graphics.FromImage(panel);
						int p_left = 0;
						int p_top = 0;
						if (image.Width <= size) //如果原图比目标形状宽
						{
							p_left = (size - image.Width) / 2;
						}
						if (image.Height <= size)
						{
							p_top = (size - image.Height) / 2;
						}

						//将生成的二维码图像粘贴至绘图的中心位置
						graphic0.DrawImage(image, p_left, p_top, image.Width, image.Height);
						image = new System.Drawing.Bitmap(panel);
						panel.Dispose();
						panel = null;
						graphic0.Dispose();
						graphic0 = null;
					}
				}
				#endregion
			}
			#endregion
			return image;
		}

	}
}
