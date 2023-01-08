using Bloc_dotnet.Datas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bloc_dotnet.Models;

namespace Bloc_dotnet.Controllers
{
    public class SitesController : Controller
    {

        private ISiteService _siteService;
        public SitesController(ISiteService siteService)
        {
            _siteService = siteService;
        }


        // GET: SiteController
        public  IActionResult Index()
        {
            List<Site> listSites = _siteService.GetSites();
            return View(listSites);
        }

        // GET: SiteController/Details/5
        public IActionResult Details(int id)
        {
            Site site = _siteService.GetSiteById(id);
            if (site == null) return View("NotFound");
            return View(site);
        }

        // GET: SiteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Site site)
        {
            try
            {
                Site newSite = _siteService.AddSite(site);
                if (newSite != null) return View("Details", newSite);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SiteController/Edit/5
        public ActionResult Edit(int id)
        {
            Site site = _siteService.GetSiteById(id);
            if (site != null) return View(site);
            return View("NotFound");
        }

        // POST: SiteController/Edit/5
        [HttpPost]
        public IActionResult Edit(Site site)
        {
            dynamic updatedSite = _siteService.UpdateSite(site);
            if (updatedSite.IdSite != null ) return View("Details", updatedSite);
            return View("NotFound");

        }

        // GET: SiteController/Delete/5
        public ActionResult Delete(int id)
        {
            if (_siteService.RemoveSite(id)) return RedirectToAction("Index");
            return View();
        }
    }
}
