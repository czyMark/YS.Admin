using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YS.Admin.Util.Model;

namespace YS.Admin.Business.AutoJob
{
    public interface IJobTask
    {
        Task<TData> Start();
    }
}
