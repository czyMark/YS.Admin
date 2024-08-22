using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using YS.Admin.Util;

namespace YS.Admin.Model.Result
{
	public class OSSCallbackModel
	{
		public string Filename { get; set; }
		public string Size { get; set; }
		public string MimeType { get; set; }
		public string Height { get; set; }
		public string Width { get; set; }
		public long CID { get; set; }
		public int FileType { get; set; }
	}
}
