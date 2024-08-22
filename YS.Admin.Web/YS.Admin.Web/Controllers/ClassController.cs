using Microsoft.AspNetCore.Mvc;

namespace YS.Admin.Web.Controllers
{
	public class ClassController : Controller
	{
		public IActionResult Apply(long? id)
		{
			return Content(id.ToString());

		}
	}
}
