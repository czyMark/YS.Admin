using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;

namespace YS.Admin.Util
{
	public static class PdfPigHelper
	{
		public static string PdfToText(string FilePath)
		{
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				using (PdfDocument pdfDocument = PdfDocument.Open(FilePath))
				{
					// 创建RTF文档

					// 遍历PDF的每一页
					foreach (var page in pdfDocument.GetPages())
					{
						// 获取页面内容
						var pageText = page.GetWords();
						//stringBuilder.Append("<p>");
						stringBuilder.Append(page.Text);
						//stringBuilder.Append("</p>");
						// 添加内容到RTF文档
					}
				}
			}
			catch
			{

			}


			return stringBuilder.ToString();
		}


	}
}
