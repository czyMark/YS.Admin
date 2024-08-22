using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YS.Admin.Cache.Factory;
using YS.Admin.Entity.SiteManage;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Service.SiteManage;
using YS.Admin.Service.SystemManage;
using YS.Admin.Util;

namespace YS.Admin.Business.Cache
{
	public class ArticleCategoryCache : BaseBusinessCache<ArticleCategoryEntity>
	{
		private ArticleCategoryService articleCategoryService = new ArticleCategoryService();

		public override string CacheKey => GlobalContext.SystemConfig.ProCode + this.GetType().Name;

		public override async Task<List<ArticleCategoryEntity>> GetList()
		{
			var cacheList = CacheFactory.Cache.GetCache<List<ArticleCategoryEntity>>(CacheKey);
			if (cacheList == null)
			{
				var result = await articleCategoryService.GetList(null);
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
