using System.Linq;
using System.Collections.Generic;
using YS.Admin.Cache.Factory;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Service.SystemManage;
using System.Threading.Tasks;
using YS.Admin.Util;

namespace YS.Admin.Business.Cache
{
    public class IpBlockCache : BaseBusinessCache<IpBlockEntity>
    {
        private IpBlockService service = new IpBlockService();

        public override string CacheKey => GlobalContext.SystemConfig.ProCode + this.GetType().Name;

        public override async Task<List<IpBlockEntity>> GetList()
        {
            var cacheList = CacheFactory.Cache.GetCache<List<IpBlockEntity>>(CacheKey);
            if (cacheList == null)
            {
                var result = await service.GetList(null);
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
