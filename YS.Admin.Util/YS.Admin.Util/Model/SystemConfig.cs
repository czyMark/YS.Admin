namespace YS.Admin.Util.Model
{
    public class SystemConfig
    {
        public SystemConfig()
        {
            DBSlowSqlLogTime = 5;
        }

        /// <summary>
        /// 是否是Demo模式
        /// </summary>
        public bool Demo { get; set; }

        /// <summary>
        /// 是否是调试模式
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// 允许一个用户在多个电脑同时登录
        /// </summary>
        public bool LoginMultiple { get; set; }

        public string LoginProvider { get; set; }

        /// <summary>
        /// Snow Flake Worker Id
        /// </summary>
        public int SnowFlakeWorkerId { get; set; }

        /// <summary>
        /// api地址
        /// </summary>
        public string ApiSite { get; set; }
		/// <summary>
		/// ApiToken
		/// </summary>
		public string ApiToken { get; set; }

        /// <summary>
        /// 主站点地址
        /// </summary>
        public string SiteWeb { get; set; }
        /// <summary>
        /// 前端
        /// </summary>
        public string SiteWebApp { get; set; }
  

        /// <summary>
        /// 允许跨域的站点
        /// </summary>
        public string AllowCorsSite { get; set; }

        /// <summary>
        /// 网站虚拟目录
        /// </summary>
        public string VirtualDirectory { get; set; }

        public string DBProvider { get; set; }

        public string DBConnectionString { get; set; }

        /// <summary>
        ///  数据库超时间（秒）
        /// </summary>
        public int DBCommandTimeout { get; set; }

        /// <summary>
        /// 慢查询记录Sql(秒),保存到文件以便分析
        /// </summary>
        public int DBSlowSqlLogTime { get; set; }

        /// <summary>
        /// 数据库备份路径
        /// </summary>
        public string DBBackup { get; set; }

        public string CacheProvider { get; set; }

        public string RedisConnectionString { get; set; }


		/// <summary>
		/// OSS配置URl
		/// </summary>
		public string OSSUrl { get; set; }
		/// <summary>
		/// OSSAccessKeyId
		/// </summary>
		public string OSSAccessKeyId { get; set; }
		/// <summary>
		/// OSSAccessKeySecret
		/// </summary>
		public string OSSAccessKeySecret { get; set; }
		/// <summary>
		/// OSSEndpoint
		/// </summary>
		public string OSSEndpoint { get; set; }
		/// <summary>
		/// OSSBucketName
		/// </summary>
		public string OSSBucketName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string WeChatAppId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string WeChatAppSecret { get; set; }

        /// <summary>
        /// 项目简称
        /// </summary>
        public string ProCode { get;set; }

        /// <summary>
        /// 创建档案时最大条目
        /// </summary>
        public int ProfileMaxCount { get; set; }


        /// <summary>
        /// 基础证书地址
        /// </summary>
        public string BaseCertificate { get; set; }

    }
}