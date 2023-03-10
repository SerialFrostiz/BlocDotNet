using Bloc_dotnet.Datas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bloc_dotnet.Models;
using Bloc_dotnet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloc_dotnet.Controllers
{
    public class SalariesController : Controller
    {
        private IServiceService _serviceService;
        private ISiteService _siteService;
        private ISalarieService _salarieService;
        private IUserService _userService;
        public SalariesController(ISalarieService salarieService,IServiceService serviceService, ISiteService siteService, IUserService userService)
        {
            _salarieService = salarieService;
            _serviceService = serviceService;
            _siteService = siteService;
            _userService = userService;
        }


        // GET: SalarieController
        public  IActionResult Index()
        {
            if (_userService.IsUser() == true)
            {
                return RedirectToAction("IndexAdmin", "Salaries");
            }
            if (_userService.IsUser()) return RedirectToAction("IndexAdmin", "Salaries");
            ViewBag.Services = new SelectList(_serviceService.GetServices().ToList(), "IdService", "Nom");
            ViewBag.Sites = new SelectList(_siteService.GetSites().ToList(), "IdSite", "Nom");
            List<Salarie> listSalaries = _salarieService.GetSalaries();
            return View(listSalaries);
        }

        // GET: SalarieController/IndexAdmin
        public IActionResult IndexAdmin()
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            ViewBag.Services = new SelectList(_serviceService.GetServices().ToList(), "IdService", "Nom");
            ViewBag.Sites = new SelectList(_siteService.GetSites().ToList(), "IdSite", "Nom");
            List<Salarie> listSalaries = _salarieService.GetSalaries();
            return View(listSalaries);
        }

        // GET: SalarieController/Details/5
        public IActionResult Details(int id)
        {
            if (_userService.IsUser() == true)
            {
                return RedirectToAction("IndexAdmin", "Salaries");
            }
            Salarie salarie = _salarieService.GetSalarieById(id);
            if (salarie == null) return View("NotFound");
            return View(salarie);
        }

        // GET: SalarieController/DetailsAdmin/5
        public IActionResult DetailsAdmin(int id)
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            Salarie salarie = _salarieService.GetSalarieById(id);
            if (salarie == null) return View("NotFound");
            return View(salarie);
        }

        // GET: SalarieController/Create
        public IActionResult Create()
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            ViewBag.Services = new SelectList(_serviceService.GetServices().ToList(), "IdService", "Nom");
            ViewBag.Sites = new SelectList(_siteService.GetSites().ToList(), "IdSite", "Nom");
            return View();
        }

        // POST: SalarieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SalarieVM salarie)
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            try
            {
                Salarie newSalarie = new Salarie()
                {
                    Nom = salarie.Nom,
                    Prenom = salarie.Prenom,
                    Fixe = salarie.Fixe,
                    Portable = salarie.Portable,
                    Email = salarie.Email,
                    Service = _serviceService.GetServiceById(salarie.IdService),
                    Site = _siteService.GetSiteById(salarie.IdSite)
                };
                Salarie createdSalarie = _salarieService.AddSalarie(newSalarie);
                if (createdSalarie != null) return View("DetailsAdmin", createdSalarie);
                return RedirectToAction("IndexAdmin");
            }
            catch
            {          
                return View();
            }
        }

        // GET: SalarieController/Edit/5
        public IActionResult Edit(int id)
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            ViewBag.Services = new SelectList(_serviceService.GetServices().ToList(), "IdService", "Nom");
            ViewBag.Sites = new SelectList(_siteService.GetSites().ToList(), "IdSite", "Nom");
            Salarie salarie = _salarieService.GetSalarieById(id);
            SalarieVM salarieVm = new SalarieVM()
            {
                IdSalarie= salarie.IdSalarie,
                Nom= salarie.Nom,
                Prenom= salarie.Prenom,
                Fixe= salarie.Fixe,
                Portable= salarie.Portable,
                Email = salarie.Email,
                IdService = salarie.Service.IdService,
                IdSite = salarie.Site.IdSite
            };
            if (salarie != null) return View(salarieVm);
            return View("NotFound");
        }

        // POST: SalarieController/Edit/5
        [HttpPost]
        public IActionResult Edit(SalarieVM salarie)
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            Salarie editedSalarie = new Salarie()
            {
                IdSalarie = salarie.IdSalarie,
                Nom = salarie.Nom,
                Prenom = salarie.Prenom,
                Fixe = salarie.Fixe,
                Portable = salarie.Portable,
                Email = salarie.Email,
                Service = _serviceService.GetServiceById(salarie.IdService),
                Site = _siteService.GetSiteById(salarie.IdSite)
            };
            Salarie updatedSalarie = _salarieService.UpdateSalarie(editedSalarie);
            if (updatedSalarie.IdSalarie != 0 ) return View("DetailsAdmin", updatedSalarie);
            return View("NotFound");

        }

        // GET: SalarieController/Delete/5
        public IActionResult Delete(int id)
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            if (_salarieService.RemoveSalarie(id)) return RedirectToAction("IndexAdmin");
            return View();
        }

        // GET: SalarieController/Search
        public IActionResult Search()
        {
            if (_userService.IsUser() == true)
            {
                return RedirectToAction("IndexAdmin", "Salaries");
            }
            List<Service> listService = new List<Service> { new Service() { IdService = 0, Nom = "Tous les services" } };
            foreach (Service service in _serviceService.GetServices().ToList())
            {
                listService.Add(service);
            }
            ViewBag.Services = new SelectList(listService, "IdService", "Nom");
            List<Site> listSites = new List<Site> { new Site() { IdSite = 0, Nom = "Tous les sites", Ville = "" } };
            foreach (Site site in _siteService.GetSites().ToList())
            {
                listSites.Add(site);
            }
            ViewBag.Sites = new SelectList(listSites, "IdSite", "Nom");
            return View();
        }

        // GET: SalarieController/SearchAdmin
        public IActionResult SearchAdmin()
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            List<Service> listService = new List<Service> { new Service() { IdService = 0, Nom = "Tous les services" } };
            foreach (Service service in _serviceService.GetServices().ToList())
            {
                listService.Add(service);
            }
            ViewBag.Services = new SelectList(listService, "IdService", "Nom");
            List<Site> listSites = new List<Site> { new Site() { IdSite = 0, Nom = "Tous les sites", Ville = "" } };
            foreach (Site site in _siteService.GetSites().ToList())
            {
                listSites.Add(site);
            }
            ViewBag.Sites = new SelectList(listSites, "IdSite", "Nom");
            return View();
        }

        // POST: SalarieController/Index
        [HttpPost]
        public IActionResult Index(SearchVM search)
        {
            if (_userService.IsUser() == true)
            {
                return RedirectToAction("IndexAdmin", "Salaries");
            }
            List<Salarie> listSalaries = _salarieService.Search(search);
            return View(listSalaries);
        }

        // POST: SalarieController/IndexAdmin
        [HttpPost]
        public IActionResult IndexAdmin(SearchVM search)
        {
            if (_userService.IsUser() == false)
            {
                return RedirectToAction("Index", "Salaries");
            }
            List<Salarie> listSalaries = _salarieService.Search(search);
            return View(listSalaries);
        }

    }
}
