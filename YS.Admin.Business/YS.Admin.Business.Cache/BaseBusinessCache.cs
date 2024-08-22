using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YS.Admin.Cache.Factory;

namespace YS.Admin.Business.Cache
{
    public abstract class BaseBusinessCache<T>
    {
        public abstract string CacheKey { get; }

        public virtual bool Remove()
        {
            return CacheFactory.Cache.RemoveCache(CacheKey);
        }

        public virtual Task<List<T>> GetList()
        {
            throw new Exception("请在子类实现");
        }
		public virtual Task<T> GetDefault()
		{
			throw new Exception("请在子类实现");
		}
	}
}
