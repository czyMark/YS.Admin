using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Admin.Util.Model
{
    /// <summary>
    /// 数据传输对象
    /// </summary>
    public class TData
    {
        /// <summary>
        /// 操作结果，Tag为1代表成功，0代表失败，其他的验证返回结果，可根据需要设置
        /// </summary>
        public int Tag { get; set; }

		/// <summary>
		/// 提示信息或异常信息
		/// </summary>
		public string Message { get; set; } = string.Empty;

       

    }

    public class TData<T> : TData
    {
        /// <summary>
        /// 列表的记录数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }

	public class TDataF<T> : TData
	{
		/// <summary>
		/// 列表的记录数
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		/// 数据
		/// </summary>
		public T Data { get; set; }




		/// <summary>
		/// 扩展Message
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 是否分片
		/// </summary>
		public bool chunked { get; set; } = false;
		/// <summary>
		/// 上传路径
		/// </summary>
		public string uploadpath { get; set; }
		/// <summary>
		/// 扩展名
		/// </summary>
		public string f_ext { get; set; }
		/// <summary>
		/// 文件路径
		/// </summary>
		public string filepath { get; set; }
		/// <summary>
		/// 文件路径
		/// </summary>
		public string filename { get; set; }
		/// <summary>
		/// 文件路径
		/// </summary>
		public int fileModule { get; set; } = 0;
	}
}
