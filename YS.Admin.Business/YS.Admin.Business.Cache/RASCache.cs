using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using YS.Admin.Cache.Factory;
using YS.Admin.Entity.SiteManage;
using YS.Admin.Model.Result;
using YS.Admin.Service.SiteManage;
using YS.Admin.Util;

namespace YS.Admin.Business.Cache
{
	public class RASCache : BaseBusinessCache<RASModel>
	{
		public override string CacheKey => GlobalContext.SystemConfig.ProCode + this.GetType().Name;

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override async Task<RASModel> GetDefault()
		{
			var cacheRAS = CacheFactory.Cache.GetCache<RASModel>(CacheKey);
			if (cacheRAS == null)
			{
				RASModel model = new RASModel();
				string PrivateKey = string.Empty;
				string PublicKey = string.Empty;
				RSAHelper.Generator(out PrivateKey, out PublicKey, 1024);
				model.PrivateKey = PrivateKey;
				model.PublicKey = PublicKey;
				DateTime expireTime = DateTime.Now.AddDays(1);
				model.ExpireTime = DateTimeHelper.GetUnixTimeStamp(expireTime);
				CacheFactory.Cache.SetCache(CacheKey, model, expireTime);
				return model;
			}
			else
			{
				return cacheRAS;
			}
		}
		public override bool Remove()
		{
			bool b = CacheFactory.Cache.RemoveCache(CacheKey);
			return b;
		}
	}
}
