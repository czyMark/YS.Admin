using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YS.Admin.Cache.Factory;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Service.SystemManage;
using YS.Admin.Util;

namespace YS.Admin.Business.Cache
{
    public class DataDictDetailCache : BaseBusinessCache<DataDictDetailEntity>
    {
        private DataDictDetailService dataDictDetailService = new DataDictDetailService();

        public override string CacheKey => GlobalContext.SystemConfig.ProCode + this.GetType().Name;

        public override async Task<List<DataDictDetailEntity>> GetList()
        {
            var cacheList = CacheFactory.Cache.GetCache<List<DataDictDetailEntity>>(CacheKey);
            if (cacheList == null)
            {
                var list = await dataDictDetailService.GetList(null);
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
