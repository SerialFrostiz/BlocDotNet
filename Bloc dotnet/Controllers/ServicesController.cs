using Bloc_dotnet.Datas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bloc_dotnet.Models;

namespace Bloc_dotnet.Controllers
{
    public class ServicesController : Controller
    {

        private IServiceService _ServiceService;
        public ServicesController(IServiceService ServiceService)
        {
            _ServiceService = ServiceService;
        }


        // GET: ServiceController
        public  IActionResult Index()
        {
            List<Service> listServices = _ServiceService.GetServices();
            return View(listServices);
        }

        // GET: ServiceController/Details/5
        public IActionResult Details(int id)
        {
            Service Service = _ServiceService.GetServiceById(id);
            if (Service == null) return View("NotFound");
            return View(Service);
        }

        // GET: ServiceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service Service)
        {
            try
            {
                Service newService = _ServiceService.AddService(Service);
                if (newService != null) return View("Details", newService);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceController/Edit/5
        public ActionResult Edit(int id)
        {
            Service Service = _ServiceService.GetServiceById(id);
            if (Service != null) return View(Service);
            return View("NotFound");
        }

        // POST: ServiceController/Edit/5
        [HttpPost]
        public IActionResult Edit(Service Service)
        {
            dynamic updatedService = _ServiceService.UpdateService(Service);
            if (updatedService.IdService != null ) return View("Details", updatedService);
            return View("NotFound");

        }

        // GET: ServiceController/Delete/5
        public ActionResult Delete(int id)
        {
            if (_ServiceService.RemoveService(id)) return RedirectToAction("Index");
            return View();
        }
    }
}
