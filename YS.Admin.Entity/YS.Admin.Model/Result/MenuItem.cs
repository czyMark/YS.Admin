using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Admin.Model.Result
{
	/// <summary>
	/// 用于门户网站的导航菜单
	/// </summary>
	public class MenuItem
	{
		public long? id { get; set; }
		public long? tid { get; set; }
        public string title { get; set; }
		public string href { get; set; }
		public string icon { get; set; }
		public string selectIcon { get; set; }

		public List<MenuItem> children { get; set; }
	}
}
