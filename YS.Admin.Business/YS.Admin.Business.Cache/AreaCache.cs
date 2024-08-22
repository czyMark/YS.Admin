using System.Linq;
using System.Collections.Generic;
using YS.Admin.Cache.Factory;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Service.SystemManage;
using System.Threading.Tasks;
using YS.Admin.Util;

namespace YS.Admin.Business.Cache
{
    public class AreaCache : BaseBusinessCache<AreaEntity>
    {
        private AreaService areaService = new AreaService();

        public override string CacheKey => GlobalContext.SystemConfig.ProCode + this.GetType().Name;

        public override async Task<List<AreaEntity>> GetList()
        {
            var cacheList = CacheFactory.Cache.GetCache<List<AreaEntity>>(CacheKey);
            if (cacheList == null)
            {
                var result = await areaService.GetList(null);
                CacheFactory.Cache.SetCache(CacheKey, result);
                return result;
            }
            else
            {
                return cacheList;
            }
        }
    }
}
