using System.Linq;
using System.Collections.Generic;
using YS.Admin.Cache.Factory;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Service.SystemManage;
using System.Threading.Tasks;
using YS.Admin.Entity.SiteManage;
using YS.Admin.Service.SiteManage;
using YS.Admin.Util;

namespace YS.Admin.Business.Cache
{
	public class SiteConfigCache : BaseBusinessCache<SiteConfigEntity>
	{
		private SiteConfigService siteConfigService = new SiteConfigService();

		public override string CacheKey => GlobalContext.SystemConfig.ProCode + this.GetType().Name;

		public override async Task<SiteConfigEntity> GetDefault()
		{
			var cacheList = CacheFactory.Cache.GetCache<SiteConfigEntity>(CacheKey);
			if (cacheList == null)
			{
				var result = await siteConfigService.GetDefault();
				CacheFactory.Cache.SetCache(CacheKey, result);
				return result;
			}
			else
			{
				return cacheList;
			}
		}

		public override bool Remove()
		{
			bool b = CacheFactory.Cache.RemoveCache(CacheKey);
			return b;
		}
	}
}
