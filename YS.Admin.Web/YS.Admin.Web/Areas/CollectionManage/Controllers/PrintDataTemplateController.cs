using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using YS.Admin.Util;
using YS.Admin.Util.Model;
using YS.Admin.Entity;
using YS.Admin.Model;
using YS.Admin.Web.Controllers;
using YS.Admin.Entity.CollectionManage;
using YS.Admin.Business.CollectionManage;
using YS.Admin.Model.Param.CollectionManage;

namespace YS.Admin.Web.Areas.CollectionManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:45
    /// 描 述：标签类型控制器类
    /// </summary>
    [Area("CollectionManage")]
    public class PrintDataTemplateController : BaseController
    {
        private PrintdataBaseBLL printdataBaseBLL = new PrintdataBaseBLL();
        private PrintstylePropBLL printstylePropBLL = new PrintstylePropBLL();

        #region 视图功能
        /// <summary>
        /// 纸币190*27MM 
        /// </summary>
        /// <returns></returns>
        public ActionResult BanknotesSizeType1()
        {
            return View();
        }
        /// <summary>
        /// 纸币160*27Mm
        /// </summary>
        /// <returns></returns>
        public ActionResult BanknotesSizeType2()
        {
            return View();
        }
        /// <summary>
        /// 硬币46x24mm
        /// </summary>
        /// <returns></returns>
        public ActionResult BanknotesSizeType3()
        {
            return View();
        }
        /// <summary>
        /// 硬币46x24mm OS
        /// </summary>
        /// <returns></returns>
        public ActionResult BanknotesSizeType4()
        {
            return View();
        }
        /// <summary>
        /// 邮票46x24mm
        /// </summary>
        /// <returns></returns>
        public ActionResult BanknotesSizeType5()
        {
            return View();
        }
        /// <summary>
        /// 邮票46x24mmOS
        /// </summary>
        /// <returns></returns>
        public ActionResult BanknotesSizeType6()
        {
            return View();
        }

        /// <summary>
        /// 硬币78x28MM
        /// </summary>
        /// <returns></returns>
        public ActionResult BanknotesSizeType7()
        {
            return View();
        }
        /// <summary>
        /// 硬币邮票78x28MMOS
        /// </summary>
        /// <returns></returns>
        public ActionResult BanknotesSizeType8()
        {
            return View();
        }
        /// <summary>
        /// 邮票190*27Mm
        /// </summary>
        /// <returns></returns>
        public ActionResult BanknotesSizeType9()
        {
            return View();
        }
        /// <summary>
        /// 邮票160*27Mm
        /// </summary>
        /// <returns></returns>
        public ActionResult BanknotesSizeType10()
        {
            return View();
        }
        /// <summary>
        /// 预留
        /// </summary>
        /// <returns></returns>
        public ActionResult BanknotesSizeType11()
        {
            return View();
        }
        #endregion


        #region 获取数据 

        [HttpGet]
        public async Task<ActionResult> GetPrintTempPageListJson(PrintdataTempListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = await printdataBaseBLL.GetPrintTempPageListJson(param, pagination);
            return Json(obj);
        }


        [HttpGet] 
        public async Task<ActionResult> GetPrintStylePropListJson(PrintstylePropListParam param)
        { 
            TData<List<PrintstylePropEntity>> obj = await printstylePropBLL.GetAllList(param);
            return Json(obj);
        }

        #endregion
    }
}
