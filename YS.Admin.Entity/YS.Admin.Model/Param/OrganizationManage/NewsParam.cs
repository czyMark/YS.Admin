using System;
using System.Collections.Generic;
using YS.Admin.Model.Param.SystemManage;

namespace YS.Admin.Model.Param.OrganizationManage
{
    public class NewsListParam : BaseAreaParam
    {
        public string NewsTitle { get; set; }
        public int? NewsType { get; set; }
        public string NewsTag { get; set; }
    }
}
