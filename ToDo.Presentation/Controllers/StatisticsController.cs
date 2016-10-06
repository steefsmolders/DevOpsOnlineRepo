using System.Threading.Tasks;
using System.Web.Mvc;
using ToDo.Interfaces.Business;

namespace ToDo.Presentation.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IItemService itemService;

        public StatisticsController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        // GET: Statistics
        public ActionResult Index()
        {
            return View(itemService.GetStatistics());
        }
    }
}