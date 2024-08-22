using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YS.Admin.Business.CollectionManage;
using YS.Admin.Business.Portal;
using YS.Admin.Business.SiteManage;
using YS.Admin.Entity.Portal;
using YS.Admin.Entity.SiteManage;
using YS.Admin.Model.Param.Portal;
using YS.Admin.Model.Param.SiteManage;
using YS.Admin.Model.Result;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Web.Controllers;

namespace YS.Admin.Web.Areas.CollectionManage.Controllers
{


    [Area("Portal")]
    public class ViewInfoController : BaseController
    {
        private BigdataEditionBLL bigdataEditionBLL = new BigdataEditionBLL();
        private BigdataResultBLL bigdataResultBLL = new BigdataResultBLL();
        private BigdataRatingBLL bigdataRatingBLL = new BigdataRatingBLL();
        /// <summary>
        /// 站点信息
        /// </summary>
        private SiteConfigBLL siteConfigBLL = new SiteConfigBLL();

        private BigdataCategoryBLL bigdataCategoryBLL = new BigdataCategoryBLL();
        private BigdataTagBLL bigdataTagBLL = new BigdataTagBLL();

        /// <summary>
        /// 栏目信息
        /// </summary>
        private ArticleCategoryBLL articleCategoryBLL = new ArticleCategoryBLL();

        /// <summary>
        /// 栏目内容信息
        /// </summary>
        private ArticlesBLL articlesBLL = new ArticlesBLL();

        /// <summary>
        /// 栏目附加数据
        /// </summary>
        private ArticlesDescriptiondataBLL articlesDescriptiondataBLL = new ArticlesDescriptiondataBLL();

        /// <summary>
        /// 模版信息
        /// </summary>
        private ArticlesTemplateBLL articlesTemplateBLL = new ArticlesTemplateBLL();

        /// <summary>
        /// 首页banner图
        /// </summary>
        private AdvertBannerBLL advertBannerBLL = new AdvertBannerBLL();

        /// <summary>
        /// 友情链接
        /// </summary>
        private LinkBLL linkBLL = new LinkBLL();

        #region 视图功能 
        public async Task<ActionResult> Index()
        {
            await GetGlobalInfo();


            //查询首页banner图
            TData<List<AdvertBannerEntity>> bannerlist = await advertBannerBLL.GetPageList(new Model.Param.SiteManage.AdvertBannerListParam() { Aid = 718451339591421952 }, new Pagination() { Sort = "SortId", PageIndex = 1, SortType = "  asc ", PageSize = 999 });
            ViewBag.swiperList = bannerlist.Data.ToJson();

            //查询最热门的超级链接数据
            TData<List<LinkEntity>> linklist = await linkBLL.GetPageList(new Model.Param.SiteManage.LinkListParam { }, new Pagination() { PageIndex = 1, PageSize = 6 });
            ViewBag.hrefList = linklist.Data.ToJson();

            //查询最新公告标题
            ViewBag.title = @"最新消息";

            //查询最新公告数据
            TData<ArticlesEntity> articles = await articlesBLL.GetNewsEntity(new Model.Param.SiteManage.ArticlesListParam() { CategoryId = 740515357835399168 });
            if (articles.Tag == 1)
                ViewBag.lable = articles.Data.Explanatory;
            else
                ViewBag.lable = "";

            return View();
        }


        public async Task<ActionResult> Menu()
        {
            List<MenuItem> Data = await GetGlobalInfo();
            MenuItem tmenu = Data.Find(d => d.tid > 0);
            if (tmenu != null)
                Data.Remove(tmenu);
            ViewBag.bodyMenuList = Data.ToJson();

            return View();
        }


        public async Task<ActionResult> href()
        {
            await GetGlobalInfo();
            TData<List<LinkEntity>> data = await linkBLL.GetList(new Model.Param.SiteManage.LinkListParam() { });
            if (data.Tag == 1)
            {

                ViewBag.guanfangList = data.Data.Where(d => d.LinkType == 0).ToJson(); ;
                ViewBag.gongsiList = data.Data.Where(d => d.LinkType == 1).ToJson(); ;
            }
            else
            {
                ViewBag.guanfangList = @"[]";
                ViewBag.gongsiList = @"[]";
            }

            return View("~/Areas/Portal/Views/Template/href.cshtml");
        }


        /// <summary>
        /// 当前栏目id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> SiteInfo(string id)
        {
            await GetGlobalInfo();
            //查询栏目信息
            var CategoryInfo = await ShowCategoryInfo(id);

            //查询栏目内容信息
            string TemplateAddr = await ShowNewsData(id, CategoryInfo.TemplateId.ToString(), CategoryInfo.LinkUrl);

            ViewBag.CategoryId = id;
            return View(TemplateAddr);
        }


        public async Task<ActionResult> showImage(string id)
        {
            await GetGlobalInfo();
            //查询对应的图片
            var data = await articlesDescriptiondataBLL.GetEntity(id.ParseToLong());
            ViewBag.Message = data.Message;
            ViewBag.Tag = data.Tag;
            if (data.Tag == 1)
                ViewBag.Goods = data.Data.C4;
            return View();
        }

        public IActionResult GetCertificateImage(string IDCode)
        {
            //读取图片流

            string BaseCertificate = "\\upload\\Certificate\\" + IDCode + ".png";
            string BaseCertificateAllPath = GlobalContext.HostingEnvironment.ContentRootPath + BaseCertificate;

            byte[] bytes ;
            using (FileStream fs = new FileStream(BaseCertificateAllPath, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
            }
            return File(bytes, @"image/jpeg");
        }

        public async Task<ActionResult> showCertificate(string IDCode)
        {
            await GetGlobalInfo();

            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";


            //查询对应的图片
            //根据IDCode 
            var data = await certificateBLL.IDCodeGetEntity(IDCode);
            ViewBag.Message = data.Message;
            ViewBag.Tag = data.Tag;
            
            if (data.Tag == 1)
                ViewBag.Goods = "./GetCertificateImage?IDCode=" + IDCode;
            return View("~/Areas/Portal/Views/ViewInfo/showImage.cshtml");
        }

        public async Task<ActionResult> AnnouncementDetails(string id, string pid)
        {
            await GetGlobalInfo();


            TData<List<ArticlesEntity>> temp = await articlesBLL.GetList(new Model.Param.SiteManage.ArticlesListParam() { Status = 1, CategoryId = pid.ParseToLong() });

            List<ArticlesEntity> data = temp.Data.OrderBy(d => d.SortId).ToList();




            //查询当前数据信息，在查询上下一条数据的标题
            //多条数据分页显示
            ArticlesEntity ae = data.FirstOrDefault(u => u.Id == id.ParseToLong());
            if (ae == null)
            {
                //查询当前编辑的新闻信息中的标题
                ViewBag.title = "";
                //查询当前编辑的新闻信息中的正文
                ViewBag.details = "";
                ViewBag.time = "";
                ViewBag.CategoryId = "";
                ViewBag.prevTitle = "";
                ViewBag.prevSrc = "";
                ViewBag.nextTitle = "";
                ViewBag.nextSrc = "";
                return View();
            }
            //查询当前编辑的新闻信息中的标题
            ViewBag.title = ae.Title;
            //查询当前编辑的新闻信息中的正文
            ViewBag.details = ae.Contents;
            ViewBag.time = ae.Remark;
            ViewBag.CategoryId = ae.CategoryId;


            int index = data.IndexOf(ae);
            // 获取上一条数据
            ArticlesEntity prev = data.ElementAtOrDefault(index - 1);
            if (prev != null)
            {
                ViewBag.prevTitle = prev.Title;
                ViewBag.prevSrc = "./AnnouncementDetails?id=" + prev.Id + "&pid=" + pid;
            }
            else
            {

                ViewBag.prevTitle = "";
                ViewBag.prevSrc = "";
            }
            // 获取下一条数据
            ArticlesEntity next = data.ElementAtOrDefault(index + 1);
            if (next != null)
            {
                ViewBag.nextTitle = next.Title;
                ViewBag.nextSrc = "./AnnouncementDetails?id=" + next.Id + "&pid=" + pid;
            }
            else
            {

                ViewBag.nextTitle = "";
                ViewBag.nextSrc = "";
            }


            return View();
        }


        public async Task<ActionResult> ratingCertificate(string id)
        {
            await GetGlobalInfo();

            List<List<string>> goods = new List<List<string>>();
            List<List<string>> swiperList = new List<List<string>>();
            string ratingdate = string.Empty;
            string seal = "/images/goods/zhang.png";
            string certificateHeader = "/images/pingji/header-pc.png";
            string BigDataTag = string.Empty;
            //查询藏品对应信息-内容表对应的附加数据
            var data = await articlesDescriptiondataBLL.GetList(new ArticlesDescriptiondataListParam() { pid = id });
            foreach (var item in data.Data)
            {
                if (item.C5.Contains("藏品图片"))
                {
                    var t = item.C4.Split(";");
                    if (t.Length > 0)
                    {
                        swiperList.Add(t.ToList());
                    }
                }
                else if (item.C5.Contains("证书顶部图片"))
                {
                    certificateHeader = item.C4;
                }
                else if (item.C5 == "大数据标识")
                {
                    BigDataTag += item.C2;
                }
                else if (item.C5 == "大数据标识界面显示")
                {
                    BigDataTag += item.C2;
                    var t = new List<string>();
                    t.Add(item.C1);
                    t.Add(item.C2);
                    goods.Add(t);
                }
                else if (item.C5 == "印章图片")
                {
                    seal = item.C4;
                }
                else if (item.C5 == "评级日期")
                {
                    ratingdate = item.C2;
                }
                else
                {
                    var t = new List<string>();
                    t.Add(item.C1);
                    t.Add(item.C2);
                    goods.Add(t);
                }
            }
            //藏品对应的图片
            ViewBag.swiperList = swiperList;
            //藏品属性数据
            ViewBag.goodsInfo = goods;
            //评级日期
            ViewBag.ratingdate = ratingdate;
            //印章图片 后续在讨论需求
            ViewBag.seal = seal;

            ViewBag.certificateHeader = certificateHeader;



            //依据鉴定编号 IDCode 查询 统计数据


            //todo:从大数据统计表中查询 bigdataTag 对应的数据
            var bigdataRating = await bigdataRatingBLL.GetPageList(new Model.Param.Portal.BigdataRatingListParam() { BigDataTag = BigDataTag },
                new Pagination() { PageIndex = 1, PageSize = int.MaxValue, Sort = "Rating", SortType = "desc" });
            //var bigdataResult = await bigdataResultBLL.GetPageList(new Model.Param.Portal.BigdataResultListParam() { BigDataTag = BigDataTag },
            //    new Pagination() { PageIndex = 1, PageSize = int.MaxValue, Sort = "Id", SortType = "desc" });
            var bigdataEdition = await bigdataEditionBLL.GetPageList(new Model.Param.Portal.BigdataEditionListParam() { BigDataTag = BigDataTag },
                new Pagination() { PageIndex = 1, PageSize = int.MaxValue, Sort = "TotalTag", SortType = "desc" });
            //评分统计
            ViewBag.tableData = bigdataRating.Data;


            //版别统计列
            List<string> cateList = new List<string>();
            cateList.Add("名称");
            cateList.AddRange(bigdataEdition.Data.GroupBy(n => n.TotalTag).Select(d => d.Key).ToList());
            ViewBag.cateList = cateList;

            List<List<string>> cateTotalList = new List<List<string>>();
            var groupedData = bigdataEdition.Data.GroupBy(n => n.CollectionName);
            foreach (var cate in groupedData)
            {
                List<string> strArr = new List<string>();
                strArr.Add(cate.Key);
                var temp = bigdataEdition.Data.Where(d => d.CollectionName == cate.Key).OrderByDescending(d => d.TotalTag).ToList();
                foreach (var d in temp)
                {
                    strArr.Add(d.Result);
                }
                cateTotalList.Add(strArr);
            }

            ViewBag.cateTotalList = cateTotalList;

            //if (bigdataResult.Tag == 1)
            //{
            //    var t = bigdataResult.Data.FirstOrDefault();
            //    ViewBag.feedback = t == null ? "" : t.BigDataResult;
            //}
            //else
            //{
            //    ViewBag.feedback = "";
            //}
            return View();
        }


        private CertificateBLL certificateBLL = new CertificateBLL();
        private PrintdataBaseBLL printdataBaseBLL = new PrintdataBaseBLL();
        public async Task<ActionResult> ratingCertificateIDCode(string IDCode)
        {
            await GetGlobalInfo();


            List<List<string>> goods = new List<List<string>>();
            List<List<string>> swiperList = new List<List<string>>();
            string ratingdate = string.Empty;
            string seal = "/images/goods/zhang.png";
            string BigDataTag = string.Empty;
            //查询藏品对应信息-藏品数据表
            var list = await printdataBaseBLL.GetDatabaseTypeDataList(
              new Model.Param.CollectionManage.PrintdataBaseListParam() { IDCode = IDCode },
              new Pagination()
              );
            var printdata = list.Data.FirstOrDefault();
            if (printdata == null)
            {
                //藏品对应的图片
                ViewBag.swiperList = new List<string>();
                //藏品属性数据
                ViewBag.goodsInfo = new List<string>();
                //评级日期
                ViewBag.ratingdate = ratingdate;
                //印章图片 后续在讨论需求
                ViewBag.seal = seal;

                ViewBag.cateList = new List<string>();

                ViewBag.cateTotalList = new List<string>();

                ViewBag.feedback = "";
                return View("~/Areas/Portal/Views/ViewInfo/ratingCertificateIDCode.cshtml");
            }
            if (printdata.CollectionImage == null)
                printdata.CollectionImage = "";
            var t = printdata.CollectionImage.Split(";");
            if (t.Length > 0)
            {
                swiperList.Add(t.ToList());
            }
            if (printdata.DataTag == 1)
            {
                BigDataTag = printdata.CollectionYear + printdata.CollectionValue;
            }
            else
            {
                BigDataTag = printdata.BigDataTag;
            }
            DateTime dt = printdata.BaseCreateTime.Value;
            ratingdate = dt.ToString("yyyy年MM月dd日");


            if (printdata.DataTag == 1)
            {
                #region 纸币

                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("鉴定编号");
                    goodsitem.Add(printdata.IDCode);
                    goods.Add(goodsitem);
                }

                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("藏品名称");
                    goodsitem.Add(printdata.CollectionName);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("年份");
                    goodsitem.Add(printdata.CollectionYear);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("面值");
                    goodsitem.Add(printdata.CollectionValue);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("印刷工艺");
                    goodsitem.Add(printdata.PrintArt);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("冠字编号");
                    goodsitem.Add(printdata.SerialCode);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("评分");
                    goodsitem.Add(printdata.Rating);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("HQP");
                    goodsitem.Add(printdata.HQP);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("三星");
                    goodsitem.Add(printdata.StarTag);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("版别/个性化");
                    goodsitem.Add(printdata.EditionPersonalization);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("估值保价");
                    goodsitem.Add(printdata.EstimatedValue);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("珍稀度");
                    goodsitem.Add(printdata.Rarity);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("发行单位");
                    goodsitem.Add(printdata.IssuingUnit);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("鉴定师");
                    goodsitem.Add(printdata.AppraiserName);
                    goods.Add(goodsitem);
                }
                #endregion
            }
            else if (printdata.DataTag == 2)
            {

                #region 邮票

                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("鉴定编号");
                    goodsitem.Add(printdata.IDCode);
                    goods.Add(goodsitem);
                }

                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("藏品名称");
                    goodsitem.Add(printdata.CollectionName);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("年份");
                    goodsitem.Add(printdata.CollectionYear);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("面值");
                    goodsitem.Add(printdata.CollectionValue);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("评分");
                    goodsitem.Add(printdata.Rating);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("OS");
                    goodsitem.Add(printdata.OS);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("描述");
                    goodsitem.Add(printdata.Description);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("版别");
                    goodsitem.Add(printdata.Edition);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("志号");
                    goodsitem.Add(printdata.NumberCode);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("原胶");
                    goodsitem.Add(printdata.VirginRubber);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("个性化");
                    goodsitem.Add(printdata.Personalization);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("估值");
                    goodsitem.Add(printdata.EstimatedValue);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("珍稀度");
                    goodsitem.Add(printdata.Rarity);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("发行单位");
                    goodsitem.Add(printdata.IssuingUnit);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("鉴定师");
                    goodsitem.Add(printdata.AppraiserName);
                    goods.Add(goodsitem);
                }
                #endregion
            }

            else if (printdata.DataTag == 3)
            {
                #region 硬币

                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("鉴定编号");
                    goodsitem.Add(printdata.IDCode);
                    goods.Add(goodsitem);
                }

                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("藏品名称");
                    goodsitem.Add(printdata.CollectionName);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("年份");
                    goodsitem.Add(printdata.CollectionYear);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("面值");
                    goodsitem.Add(printdata.CollectionValue);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("评分");
                    goodsitem.Add(printdata.PrintArt);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("OS");
                    goodsitem.Add(printdata.OS);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("版别");
                    goodsitem.Add(printdata.Edition);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("材质");
                    goodsitem.Add(printdata.Material);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("重量");
                    goodsitem.Add(printdata.Weight);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("尺寸");
                    goodsitem.Add(printdata.Size);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("估值保价");
                    goodsitem.Add(printdata.EstimatedValue);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("珍稀度");
                    goodsitem.Add(printdata.Rarity);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("发行单位");
                    goodsitem.Add(printdata.IssuingUnit);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("鉴定师");
                    goodsitem.Add(printdata.AppraiserName);
                    goods.Add(goodsitem);
                }
                #endregion
            }


            //藏品对应的图片
            ViewBag.swiperList = swiperList;
            //藏品属性数据
            ViewBag.goodsInfo = goods;
            //评级日期
            ViewBag.ratingdate = ratingdate;
            //印章图片 后续在讨论需求
            ViewBag.seal = seal;

            //依据鉴定编号 IDCode 查询 统计数据



            //todo:从大数据统计表中查询 bigdataTag 对应的数据
            var bigdataRating = await bigdataRatingBLL.GetPageList(new Model.Param.Portal.BigdataRatingListParam() { BigDataTag = BigDataTag },
                new Pagination() { PageIndex = 1, PageSize = int.MaxValue, Sort = "Rating", SortType = "desc" });
            //var bigdataResult = await bigdataResultBLL.GetPageList(new Model.Param.Portal.BigdataResultListParam() { BigDataTag = BigDataTag },
            //    new Pagination() { PageIndex = 1, PageSize = int.MaxValue, Sort = "Id", SortType = "desc" });
            var bigdataEdition = await bigdataEditionBLL.GetPageList(new Model.Param.Portal.BigdataEditionListParam() { BigDataTag = BigDataTag },
                new Pagination() { PageIndex = 1, PageSize = int.MaxValue, Sort = "TotalTag", SortType = "desc" });
            //评分统计
            ViewBag.tableData = bigdataRating.Data;


            //版别统计列
            List<string> cateList = new List<string>();
            cateList.Add("名称");
            cateList.AddRange(bigdataEdition.Data.GroupBy(n => n.TotalTag).Select(d => d.Key).ToList());
            ViewBag.cateList = cateList;

            List<List<string>> cateTotalList = new List<List<string>>();
            var groupedData = bigdataEdition.Data.GroupBy(n => n.CollectionName);
            foreach (var cate in groupedData)
            {
                List<string> strArr = new List<string>();
                strArr.Add(cate.Key);
                var temp = bigdataEdition.Data.Where(d => d.CollectionName == cate.Key).OrderByDescending(d => d.TotalTag).ToList();
                foreach (var d in temp)
                {
                    strArr.Add(d.Result);
                }
                cateTotalList.Add(strArr);
            }

            ViewBag.cateTotalList = cateTotalList;

            //if (bigdataResult.Tag == 1)
            //{
            //    var feedback = bigdataResult.Data.FirstOrDefault();
            //    ViewBag.feedback = feedback == null ? "" : feedback.BigDataResult;
            //}
            //else
            //{
            //    ViewBag.feedback = "";
            //}




            return View("~/Areas/Portal/Views/ViewInfo/ratingCertificateIDCode.cshtml");
        }
        public async Task<ActionResult> ratingCertificateIDCodePhone(string IDCode)
        {


            List<List<string>> goods = new List<List<string>>();
            List<List<string>> swiperList = new List<List<string>>();
            string ratingdate = string.Empty;
            string seal = "/images/goods/zhang.png";
            string BigDataTag = string.Empty;
            //查询藏品对应信息-藏品数据表
            var list = await printdataBaseBLL.GetDatabaseTypeDataList(
              new Model.Param.CollectionManage.PrintdataBaseListParam() { IDCode = IDCode },
              new Pagination()
              );
            var printdata = list.Data.FirstOrDefault();
            if (printdata == null)
            {
                //藏品对应的图片
                ViewBag.swiperList = new List<string>();
                //藏品属性数据
                ViewBag.goodsInfo = new List<string>();
                //评级日期
                ViewBag.ratingdate = ratingdate;
                //印章图片 后续在讨论需求
                ViewBag.seal = seal;

                ViewBag.cateList = new List<string>();

                ViewBag.cateTotalList = new List<string>();

                ViewBag.feedback = "";
                return View("~/Areas/Portal/Views/ViewInfo/ratingCertificateIDCodePhone.cshtml");
            }
            if (printdata.CollectionImage == null)
                printdata.CollectionImage = "";
            var t = printdata.CollectionImage.Split(";");
            if (t.Length > 0)
            {
                swiperList.Add(t.ToList());
            }
            if (printdata.DataTag == 1)
            {
                BigDataTag = printdata.CollectionYear + printdata.CollectionValue;
            }
            else
            {
                BigDataTag = printdata.BigDataTag;
            }
            DateTime dt = printdata.BaseCreateTime.Value;
            ratingdate = dt.ToString("yyyy年MM月dd日");


            if (printdata.DataTag == 1)
            {
                #region 纸币

                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("鉴定编号");
                    goodsitem.Add(printdata.IDCode);
                    goods.Add(goodsitem);
                }

                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("藏品名称");
                    goodsitem.Add(printdata.CollectionName);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("年份");
                    goodsitem.Add(printdata.CollectionYear);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("面值");
                    goodsitem.Add(printdata.CollectionValue);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("印刷工艺");
                    goodsitem.Add(printdata.PrintArt);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("冠字编号");
                    goodsitem.Add(printdata.SerialCode);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("评分");
                    goodsitem.Add(printdata.Rating);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("HQP");
                    goodsitem.Add(printdata.HQP);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("三星");
                    goodsitem.Add(printdata.StarTag);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("版别/个性化");
                    goodsitem.Add(printdata.EditionPersonalization);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("估值保价");
                    goodsitem.Add(printdata.EstimatedValue);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("珍稀度");
                    goodsitem.Add(printdata.Rarity);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("发行单位");
                    goodsitem.Add(printdata.IssuingUnit);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("鉴定师");
                    goodsitem.Add(printdata.AppraiserName);
                    goods.Add(goodsitem);
                }
                #endregion
            }
            else if (printdata.DataTag == 2)
            {

                #region 邮票

                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("鉴定编号");
                    goodsitem.Add(printdata.IDCode);
                    goods.Add(goodsitem);
                }

                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("藏品名称");
                    goodsitem.Add(printdata.CollectionName);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("年份");
                    goodsitem.Add(printdata.CollectionYear);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("面值");
                    goodsitem.Add(printdata.CollectionValue);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("评分");
                    goodsitem.Add(printdata.Rating);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("OS");
                    goodsitem.Add(printdata.OS);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("描述");
                    goodsitem.Add(printdata.Description);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("版别");
                    goodsitem.Add(printdata.Edition);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("志号");
                    goodsitem.Add(printdata.NumberCode);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("原胶");
                    goodsitem.Add(printdata.VirginRubber);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("个性化");
                    goodsitem.Add(printdata.Personalization);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("估值");
                    goodsitem.Add(printdata.EstimatedValue);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("珍稀度");
                    goodsitem.Add(printdata.Rarity);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("发行单位");
                    goodsitem.Add(printdata.IssuingUnit);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("鉴定师");
                    goodsitem.Add(printdata.AppraiserName);
                    goods.Add(goodsitem);
                }
                #endregion
            }

            else if (printdata.DataTag == 3)
            {
                #region 硬币

                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("鉴定编号");
                    goodsitem.Add(printdata.IDCode);
                    goods.Add(goodsitem);
                }

                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("藏品名称");
                    goodsitem.Add(printdata.CollectionName);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("年份");
                    goodsitem.Add(printdata.CollectionYear);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("面值");
                    goodsitem.Add(printdata.CollectionValue);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("评分");
                    goodsitem.Add(printdata.PrintArt);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("OS");
                    goodsitem.Add(printdata.OS);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("版别");
                    goodsitem.Add(printdata.Edition);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("材质");
                    goodsitem.Add(printdata.Material);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("重量");
                    goodsitem.Add(printdata.Weight);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("尺寸");
                    goodsitem.Add(printdata.Size);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("估值保价");
                    goodsitem.Add(printdata.EstimatedValue);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("珍稀度");
                    goodsitem.Add(printdata.Rarity);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("发行单位");
                    goodsitem.Add(printdata.IssuingUnit);
                    goods.Add(goodsitem);
                }
                {
                    List<string> goodsitem = new List<string>();
                    goodsitem.Add("鉴定师");
                    goodsitem.Add(printdata.AppraiserName);
                    goods.Add(goodsitem);
                }
                #endregion
            }


            //藏品对应的图片
            ViewBag.swiperList = swiperList;
            //藏品属性数据
            ViewBag.goodsInfo = goods;
            //评级日期
            ViewBag.ratingdate = ratingdate;
            //印章图片 后续在讨论需求
            ViewBag.seal = seal;


            return View("~/Areas/Portal/Views/ViewInfo/ratingCertificateIDCodePhone.cshtml");
        }

        public async Task<ActionResult> bigdataNav()
        {
            await GetGlobalInfo();

            TData<List<BigdataCategoryEntity>> category = await bigdataCategoryBLL.GetList(new BigdataCategoryListParam());
            if (category.Tag == 1)
            {
                foreach (var item in category.Data)
                {
                    TData<List<BigdataTagEntity>> tag = await bigdataTagBLL.GetList(new BigdataTagListParam() { CategoryId = item.Id.ToString() });
                    if (tag.Tag == 1)
                        item.TagList = tag.Data.ToList();
                }
            }
            ViewBag.tableList = category.Data;
            return View();
        }

        public async Task<ActionResult> bigdata(string TagName)
        {
            await GetGlobalInfo();


            if (string.IsNullOrEmpty(TagName))
            {
                ViewBag.cateList = new List<string>();
                ViewBag.cateTotalList = new List<List<string>>();
                return View();
            }
            var bigdataEdition = await bigdataEditionBLL.GetPageList(new Model.Param.Portal.BigdataEditionListParam() { BigDataTag = TagName },
               new Pagination() { PageIndex = 1, PageSize = int.MaxValue, Sort = "TotalTag", SortType = "desc" });

            //版别统计列
            List<string> cateList = new List<string>();
            cateList.Add("名称");
            cateList.AddRange(bigdataEdition.Data.GroupBy(n => n.TotalTag).Select(d => d.Key).ToList());
            ViewBag.cateList = cateList;

            List<List<string>> cateTotalList = new List<List<string>>();
            var groupedData = bigdataEdition.Data.GroupBy(n => n.CollectionName);
            foreach (var cate in groupedData)
            {
                List<string> strArr = new List<string>();
                strArr.Add(cate.Key);
                var temp = bigdataEdition.Data.Where(d => d.CollectionName == cate.Key).OrderByDescending(d => d.TotalTag).ToList();
                foreach (var d in temp)
                {
                    strArr.Add(d.Result);
                }
                cateTotalList.Add(strArr);
            }

            ViewBag.cateTotalList = cateTotalList;

            return View();
        }


        #endregion

        #region 获取数据

        /// <summary>
        /// 验证大数据项是否存在
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> VerifyBigdataTag(BigdataTagListParam param)
        {
            if (param == null || string.IsNullOrEmpty(param.TagName))
            {
                TData<List<BigdataTagEntity>> temp = new TData<List<BigdataTagEntity>>();
                temp.Message = "必须输入大数据统计项";
                temp.Tag = 0;
                return Json(temp);
            }

            TData<List<BigdataTagEntity>> obj = await bigdataTagBLL.VerifyDataTag(param);
            return Json(obj);
        }
        /// <summary>
        /// 内容分页
        /// </summary>
        /// <param name="param"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetArticlesPageListJson(ArticlesListParam param, Pagination pagination)
        {
            param.Status = 1;
            pagination.Sort = "SortId,BaseCreateTime"; // 默认按Id排序
            pagination.SortType = "asc";
            TData<List<ArticlesEntity>> obj = await articlesBLL.GetPageList(param, pagination);
            obj.Data = obj.Data.OrderBy(d => d.SortId).OrderBy(d => d.BaseCreateTime).ToList();
            return Json(obj);
        }
        #endregion

        #region 私有方法 
        public async Task<List<MenuItem>> GetGlobalInfo()
        {
            //todo:可以优化去缓存中读
            //查询网站全局信息
            TData<SiteConfigEntity> temp = await siteConfigBLL.GetDefaultList();
            //备案号
            ViewBag.WebCord = temp.Data.WebCord;
            //版权所有
            ViewBag.AllRight = temp.Data.AllRight;
            //服务联系方式
            ViewBag.ServiceTel = temp.Data.ServiceTel;
            //地址
            ViewBag.Addr = temp.Data.Addr;
            //工作时间
            ViewBag.WorkTime = temp.Data.WorkTime;
            var articleCategory = articleCategoryBLL.GetList(new Model.Param.SiteManage.ArticleCategoryListParam() { }).Result.Data;
            var menu = ArticleCategoryToMenu(articleCategory);
            //string str = "[{    id: '1',    title: '关于',    href: './menu',       children: [        {            id: '31',            title: '公司简介',            href: './companyProfile'        },        {            id: '31',            title: '消息公告',            href: './Announcement'        },        {            id: '31',            title: '专家+仪器',            href: './expertsAndInstruments'        }    ]},{    id: '2',    title: '鉴定与评估',    href: './menu',    children: [        {            id: '31',            title: ' 纸币鉴定标准',            href: './charges'        },        {            id: '31',            title: '邮票鉴定标准',            href: './charges'        },        {            id: '31',            title: '金银币鉴定标准',            href: './charges'        },        {            id: '31',            title: '古钱币制币',            href: './charges'        },        {            id: '31',            title: '卡牌',            href: './charges'        },        {            id: '31',            title: '评估标准',            href: './evaluationCriteria'        }    ]},{    id: '3',    href: './menu',    title: '收费标准',    children: [        {            id: '31',            title: ' 纸币鉴定标准',            href: './charge'        },        {            id: '31',            title: '邮票鉴定标准',            href: './charge'        },        {            id: '31',            title: '金银币鉴定标准',            href: './charge'        },        {            id: '31',            title: '古钱币制币',            href: './charge'        },        {            id: '31',            title: '卡牌',            href: './charge'        },        {            id: '31',            title: '评估标准',            href: './evaluationCriteria'        }    ]},{    id: '4',    href: './menu',    title: '藏品展示',    children: [        {            id: '41',            title: '纸币',            href: './goods'        },        {            id: '52',            title: '纸币套装',            href: './goods'        },        {            id: '41',            title: '纸币刀货',            href: './goods'        },        {            id: '52',            title: '纸币2-3张套装',            href: './goods'        },        {            id: '41',            title: '外国纸币',            href: './goods'        },        {            id: '52',            title: '币钞套装',            href: './goods'        },        {            id: '41',            title: '民国纸币',            href: './goods'        },        {            id: '52',            title: '纸币双力货',            href: './goods'        }    ]},{    id: '998',    title: '版别标注',    href: './banBieBiaoZhu',},{    id: '999',    title: '提交',    href: './menu',    children: [        {            id: '31',            title: '评级流程',            href: './pingJiLiuCheng'        },        {            id: '31',            title: '送评须知',            href: './originalText'        },        {            id: '31',            title: '联系我们',            href: './contactUs'        }    ]}]";
            ViewBag.muendata = menu.ToJson();

            return menu;
        }

        public async Task<ArticleCategoryEntity> ShowCategoryInfo(string id)
        {
            //查询栏目内容第一条数据
            TData<ArticleCategoryEntity> ac = await articleCategoryBLL.GetEntity(id.ParseToLong());

            //广告图片
            ViewBag.banner = ac.Data.ImgUrl3;
            //点击广告跳转图片
            ViewBag.bannerUrl = ac.Data.BannerLinkUrl;
            return ac.Data;
        }
        public List<MenuItem> ArticleCategoryToMenu(List<ArticleCategoryEntity> menu)
        {

            var mainmenu = menu.Where(d => d.ParentId == 0).ToList();
            //菜单
            List<MenuItem> menuList = new List<MenuItem>();
            foreach (var item in mainmenu)
            {
                menuList.Add(new MenuItem()
                {
                    id = item.Id,
                    tid = item.TemplateId,
                    title = item.Title,
                    href = item.TemplateId == 0 ? "./menu" : "./SiteInfo?id=" + item.Id,
                    children =
                    item.TemplateId > 0 ? new List<MenuItem>() : GetMenuChildren(menu.Where(d => d.ParentId == item.Id).ToList())
                });

            }
            return menuList;
        }
        private List<MenuItem> GetMenuChildren(List<ArticleCategoryEntity> menu)
        {
            List<MenuItem> menuList = new List<MenuItem>();
            foreach (var item in menu)
            {
                List<MenuItem> data = GetMenuChildren(menu.Where(d => d.ParentId == item.Id).ToList());
                menuList.Add(new MenuItem() { icon = item.ImgUrl, selectIcon = item.ImgUrl2, id = item.Id, title = item.Title, href = "./SiteInfo?id=" + item.Id, children = data });
            }
            return menuList;

        }
        public async Task<string> ShowNewsData(string id, string TemplateId, string LinkUrl)
        {

            //根据模版id查找对应的网页
            TData<ArticlesTemplateEntity> temp = await articlesTemplateBLL.GetEntity(TemplateId.ParseToLong());

            //0-外链
            //1-单网页数据
            //2-单条数据附加文字数据一次显示
            //3-多条数据分页显示
            //4-多条数据附加文字数据一次显示
            //5-单条数据附加图片数据一次显示
            switch (temp.Data.TemplateDataStatus)
            {
                case 0:
                    if (string.IsNullOrEmpty(temp.Data.TemplateAddr))
                        temp.Data.TemplateAddr = LinkUrl;
                    break;
                case 1:
                    {
                        //1-单网页数据
                        TData<List<ArticlesEntity>> ae = await articlesBLL.GetList(new Model.Param.SiteManage.ArticlesListParam() { Status = 1, CategoryId = id.ParseToLong() });
                        ArticlesEntity data = ae.Data.FirstOrDefault();
                        if (data == null) { break; }
                        //查询当前编辑的新闻信息中的图片
                        ViewBag.ImgUrl = data.ImgUrl;
                        //查询当前编辑的新闻信息中的标题
                        ViewBag.Title = data.Title;
                        //查询当前编辑的新闻信息中的子标题
                        ViewBag.SubTitle = data.SubTitle;
                        //查询当前编辑的新闻信息中的摘要
                        ViewBag.Explanatory = data.Explanatory;
                        //查询当前编辑的新闻信息中的正文
                        ViewBag.details = data.Contents;
                    }
                    break;
                case 2:
                    {
                        //单条数据多附加数据一次显示
                        TData<List<ArticlesEntity>> ae = await articlesBLL.GetList(new Model.Param.SiteManage.ArticlesListParam() { Status = 1, CategoryId = id.ParseToLong() });
                        {
                            ArticlesEntity data = ae.Data.FirstOrDefault();
                            if (data == null) { break; }
                            //查询当前编辑的新闻信息中的图片
                            ViewBag.ImgUrl = data.ImgUrl;
                            //查询当前编辑的新闻信息中的标题
                            ViewBag.Title = data.Title;
                            //查询当前编辑的新闻信息中的子标题
                            ViewBag.SubTitle = data.SubTitle;
                            //查询当前编辑的新闻信息中的摘要
                            ViewBag.Explanatory = data.Explanatory;
                            //查询当前编辑的新闻信息中的正文
                            ViewBag.details = data.Contents;
                            ViewBag.ShowDetails = !string.IsNullOrEmpty(data.Contents);

                            {
                                //查询附加数据
                                TData<List<ArticlesDescriptiondataEntity>> tempdata = await articlesDescriptiondataBLL.GetPageList(
                                    new Model.Param.SiteManage.ArticlesDescriptiondataListParam() { pid = data.Id.ToString() },
                                    new Pagination() { PageIndex = 1, PageSize = int.MaxValue });
                                if (tempdata.Tag == 1)
                                    ViewBag.tableList = tempdata.Data.OrderBy(d => d.SortId);
                                else
                                    ViewBag.tableList = @"[]";
                            }
                        }
                    }
                    break;

                case 3:
                    {
                        //多条数据分页显示  新闻公告
                        TData<List<ArticlesEntity>> ae = await articlesBLL.GetPageList(
                            new Model.Param.SiteManage.ArticlesListParam() { Status = 1, CategoryId = id.ParseToLong() },
                            new Pagination() { PageIndex = 1, PageSize = 10, Sort = "SortId,BaseCreateTime", SortType = "asc" }
                            );

                        ArticlesEntity data = ae.Data.FirstOrDefault();
                        if (data == null) { ViewBag.total = 0; break; }
                        //查询当前编辑的新闻信息中的图片
                        ViewBag.ImgUrl = data.ImgUrl;
                        //查询当前编辑的新闻信息中的标题
                        ViewBag.Title = data.Title;
                        //查询当前编辑的新闻信息中的子标题
                        ViewBag.SubTitle = data.SubTitle;
                        //查询当前编辑的新闻信息中的摘要
                        ViewBag.Explanatory = data.Explanatory;
                        //查询当前编辑的新闻信息中的正文
                        ViewBag.details = data.Contents;

                        ViewBag.tableList = ae.Data;
                        ViewBag.total = ae.Total;
                    }
                    break;

                case 4:
                    {
                        //单条数据多附加数据一次显示  
                        TData<List<ArticlesEntity>> ae = await articlesBLL.GetList(new Model.Param.SiteManage.ArticlesListParam() { Status = 1, CategoryId = id.ParseToLong() });
                        ArticlesEntity data = ae.Data.FirstOrDefault();
                        if (data == null)
                        {
                            ViewBag.total = 0;
                            break;
                        }
                        //查询当前编辑的新闻信息中的图片
                        ViewBag.ImgUrl = data.ImgUrl;
                        //查询当前编辑的新闻信息中的标题
                        ViewBag.Title = data.Title;
                        //查询当前编辑的新闻信息中的子标题
                        ViewBag.SubTitle = data.SubTitle;
                        //查询当前编辑的新闻信息中的摘要
                        ViewBag.Explanatory = data.Explanatory;
                        //查询当前编辑的新闻信息中的正文
                        ViewBag.details = data.Contents;
                        {
                            //查询附加数据
                            foreach (var item in ae.Data)
                            {
                                TData<List<ArticlesDescriptiondataEntity>> tempdata = await articlesDescriptiondataBLL.GetPageList(
                                    new Model.Param.SiteManage.ArticlesDescriptiondataListParam() { pid = item.Id.ToString() },
                                    new Pagination() { PageIndex = 1, PageSize = int.MaxValue });
                                if (tempdata.Tag == 1)
                                    item.ArticlesDescriptiondata = tempdata.Data.OrderBy(d => d.SortId).ToList();
                            }
                            ViewBag.tableList = ae.Data;
                            ViewBag.total = ae.Total;
                        }
                    }
                    break;
                case 5:
                    {
                        //查询同级别栏目分类  多条数据  分页显示 藏品
                        TData<List<ArticleCategoryEntity>> ace = await articleCategoryBLL.GetPeersList(id.ParseToLong());
                        List<ArticleCategoryEntity> list = ace.Data;
                        ViewBag.selectList = list;
                        //当前数据在实体中在第几项；
                        ViewBag.select = list.IndexOf(list.FirstOrDefault(d => d.Id == id.ParseToLong()));
                        TData<List<ArticlesEntity>> ae = await articlesBLL.GetPageList(
                            new Model.Param.SiteManage.ArticlesListParam() { Status = 1, CategoryId = id.ParseToLong() },
                            new Pagination() { PageIndex = 1, PageSize = 9, Sort = "SortId,BaseCreateTime", SortType = "asc" }
                            );
                        ArticlesEntity data = ae.Data.FirstOrDefault();
                        if (data == null)
                        {
                            ViewBag.total = 0;
                            break;
                        }
                        //查询当前编辑的新闻信息中的图片
                        ViewBag.ImgUrl = data.ImgUrl;
                        //查询当前编辑的新闻信息中的标题
                        ViewBag.Title = data.Title;
                        //查询当前编辑的新闻信息中的子标题
                        ViewBag.SubTitle = data.SubTitle;
                        //查询当前编辑的新闻信息中的摘要
                        ViewBag.Explanatory = data.Explanatory;
                        //查询当前编辑的新闻信息中的正文
                        ViewBag.details = data.Contents;
                        {

                            ViewBag.tableList = ae.Data;
                            ViewBag.total = ae.Total;
                        }
                    }
                    break;
                case 6:
                    {
                        //查询低一个级别栏目分类 及   多条数据 及 多附加数据一次显示
                        TData<List<ArticleCategoryEntity>> ace = await articleCategoryBLL.GetSubordinateLevelList(id.ParseToLong());
                        {
                            foreach (var aceitem in ace.Data)
                            {
                                //查询多条数据
                                TData<List<ArticlesEntity>> ae = await articlesBLL.GetList(new Model.Param.SiteManage.ArticlesListParam() { Status = 1, CategoryId = aceitem.Id });
                                //查询附加数据
                                foreach (var item in ae.Data)
                                {
                                    TData<List<ArticlesDescriptiondataEntity>> tempdata = await articlesDescriptiondataBLL.GetPageList(
                                        new Model.Param.SiteManage.ArticlesDescriptiondataListParam() { pid = item.Id.ToString() },
                                        new Pagination() { PageIndex = 1, PageSize = int.MaxValue });
                                    if (tempdata.Tag == 1)
                                        item.ArticlesDescriptiondata = tempdata.Data.OrderBy(d => d.SortId).ToList();
                                }
                                aceitem.Articles = ae.Data;
                            }
                        }
                        ViewBag.tableList = ace.Data;
                    }
                    break;
                default:
                    break;
            }
            return temp.Data.TemplateAddr;
        }


        #endregion
    }
}
