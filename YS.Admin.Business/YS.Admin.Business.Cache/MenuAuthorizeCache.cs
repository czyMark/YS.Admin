using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Cache.Factory;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Service.SystemManage;
using YS.Admin.Util;

namespace YS.Admin.Business.Cache
{
    public class MenuAuthorizeCache : BaseBusinessCache<MenuAuthorizeEntity>
    {
        private MenuAuthorizeService menuAuthorizeService = new MenuAuthorizeService();

        public override string CacheKey => GlobalContext.SystemConfig.ProCode + this.GetType().Name;

        public override async Task<List<MenuAuthorizeEntity>> GetList()
        {
            var cacheList = CacheFactory.Cache.GetCache<List<MenuAuthorizeEntity>>(CacheKey);
            if (cacheList == null)
            {
                var list = await menuAuthorizeService.GetList(null);
                CacheFactory.Cache.SetCache(CacheKey, list);
                return list;
            }
            else
            {
                return cacheList;
            }
        }
    }
}
