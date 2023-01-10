using Bloc_dotnet.Datas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bloc_dotnet.Models;

namespace Bloc_dotnet.Controllers
{
    public class SitesController : Controller
    {

        private ISiteService _siteService;
        private IUserService _userService;
        public SitesController(ISiteService siteService, IUserService userService)
        {
            _siteService = siteService;
            _userService = userService;
        }


        // GET: SiteController
        public  IActionResult Index()
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            List<Site> listSites = _siteService.GetSites();
            return View(listSites);
        }

        // GET: SiteController/Details/5
        public IActionResult Details(int id)
        {
            if (_userService.IsUser() == true)
            {
                return RedirectToAction("IndexAdmin", "Salaries");
            }
            Site site = _siteService.GetSiteById(id);
            if (site == null) return View("NotFound");
            return View(site);
        }

        // GET: SiteController/DetailsAdmin/5
        public IActionResult DetailsAdmin(int id)
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            Site site = _siteService.GetSiteById(id);
            if (site == null) return View("NotFound");
            return View(site);
        }

        // GET: SiteController/Create
        public ActionResult Create()
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            return View();
        }

        // POST: SiteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Site site)
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            try
            {
                Site newSite = _siteService.AddSite(site);
                if (newSite != null) return View("DetailsAdmin", newSite);
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
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            Site site = _siteService.GetSiteById(id);
            if (site != null) return View(site);
            return View("NotFound");
        }

        // POST: SiteController/Edit/5
        [HttpPost]
        public IActionResult Edit(Site site)
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            dynamic updatedSite = _siteService.UpdateSite(site);
            if (updatedSite.IdSite != null ) return View("DetailsAdmin", updatedSite);
            return View("NotFound");

        }

        // GET: SiteController/Delete/5
        public ActionResult Delete(int id)
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            if (_siteService.RemoveSite(id)) return RedirectToAction("Index");
            return View();
        }
    }
}
