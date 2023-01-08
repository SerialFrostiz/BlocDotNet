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
        public SalariesController(ISalarieService salarieService,IServiceService serviceService, ISiteService siteService)
        {
            _salarieService = salarieService;
            _serviceService = serviceService;
            _siteService = siteService;
        }


        // GET: SalarieController
        public  IActionResult Index()
        {
            ViewBag.Services = new SelectList(_serviceService.GetServices().ToList(), "IdService", "Nom");
            ViewBag.Sites = new SelectList(_siteService.GetSites().ToList(), "IdSite", "Nom");
            List<Salarie> listSalaries = _salarieService.GetSalaries();
            return View(listSalaries);
        }

        // GET: SalarieController/Details/5
        public IActionResult Details(int id)
        {
            Salarie salarie = _salarieService.GetSalarieById(id);
            if (salarie == null) return View("NotFound");
            return View(salarie);
        }

        // GET: SalarieController/Create
        public IActionResult Create()
        {
            ViewBag.Services = new SelectList(_serviceService.GetServices().ToList(), "IdService", "Nom");
            ViewBag.Sites = new SelectList(_siteService.GetSites().ToList(), "IdSite", "Nom");
            return View();
        }

        // POST: SalarieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SalarieVM salarie)
        {
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
                if (createdSalarie != null) return View("Details", createdSalarie);
                return RedirectToAction("Index");
            }
            catch
            {          
                return View();
            }
        }

        // GET: SalarieController/Edit/5
        public IActionResult Edit(int id)
        {
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
            if (updatedSalarie.IdSalarie != 0 ) return View("Details", updatedSalarie);
            return View("NotFound");

        }

        // GET: SalarieController/Delete/5
        public IActionResult Delete(int id)
        {
            if (_salarieService.RemoveSalarie(id)) return RedirectToAction("Index");
            return View();
        }

        // GET: SalarieController/Search
        public IActionResult Search()
        {
            List<Service> listService = new List<Service> { new Service() { IdService = 0, Nom = "Tous les services" } };
            foreach (Service service in _serviceService.GetServices().ToList())
            {
                listService.Add(service);
            }
            ViewBag.Services = new SelectList(listService, "IdService", "Nom");
            ViewBag.Sites = new SelectList(_siteService.GetSites().ToList(), "IdSite", "Nom");
            return View();
        }
        // POST: SalarieController
        [HttpPost]
        public IActionResult Index(string recherche, int IdService, int IdSite)
        {
            
            
            
            SearchVM search = new SearchVM()
            {
                //Recherche = recherche,
                //Service = _serviceService.GetServiceById(salarie.IdService),
                //Site = _siteService.GetSiteById(salarie.IdSite)
            };
            List<Salarie> listSalaries = _salarieService.Search(search);
            return View(listSalaries);
        }
    }
}
