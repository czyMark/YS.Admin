using System;
using System.Collections.Generic;
using System.Text;

namespace YS.Admin.Model.Result
{
    public class WebTreeList
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public int ModelId { get; set; }
        public string ClassList { get; set; }
        public int ClassLayer { get; set; }
        public long Cid { get; set; }
        public string ModelUrl { get; set; }
        public List<WebTreeList> WebChildList { get; set; }

    }
}
