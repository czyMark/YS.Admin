using DinkToPdf;
using DinkToPdf.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using ColorMode = DinkToPdf.ColorMode;
using PaperKind = DinkToPdf.PaperKind;

namespace YS.Admin.Util
{

	public class PDFHelper 
	{
		#region 生成PDF
		//private IConverter _converter=new ;
		//public PDFHelper(IConverter converter)
		//{
		//	_converter = converter;
		//}

		public byte[] ConvertHtmlToPdfByDinkToPdf(string htmlContent, string fileName = "PDF")
		{
			try
			{
				// 创建 PDF 转换选项
				var globalSettings = new GlobalSettings
				{
					ColorMode = ColorMode.Color,
					Orientation = Orientation.Portrait,
					PaperSize = PaperKind.A4,
					Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 }
				};

				var objectSettings = new ObjectSettings
				{
					PagesCount = true,
					HtmlContent = htmlContent,
					WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") }
				};

				var doc = new HtmlToPdfDocument()
				{
					GlobalSettings = globalSettings,
					Objects = { objectSettings }
				};


				// 创建 PDF 转换器
				var converter = new SynchronizedConverter(new PdfTools());
				byte[] pdfBytes = converter.Convert(doc);

				return pdfBytes;
			}
			catch (Exception)
			{
				throw;
			}
		}

		
		#endregion
	}
}
