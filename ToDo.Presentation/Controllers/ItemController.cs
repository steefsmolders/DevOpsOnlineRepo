using System.Web.Mvc;
using ToDo.Interfaces.Business;
using ToDo.Models;

namespace ToDo.Presentation.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService itemService;

        public ItemController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        public ActionResult Index(int page = 1)
        {
            return View(itemService.GetItems(page));
        }

        public ActionResult Search(string query, int page = 1)
        {
            ViewBag.CurrentQuery = query;
            return View("Index", itemService.SearchItem(query, page));
        }

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Item/Create
        public ActionResult Add()
        {
            return View(new Item());
        }

        // POST: Item/Create
        [HttpPost]
        public ActionResult Add(Item item)
        {
            if (ModelState.IsValid)
            {
                itemService.AddItem(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Item/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Item/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}