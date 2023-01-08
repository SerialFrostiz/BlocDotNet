using Bloc_dotnet.Models;
using System.ComponentModel;
using System.Text;

namespace Bloc_dotnet.Datas
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;
        public ServiceService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Service AddService(Service service)
        {
            _context.Services.Add(service);
            if (_context.SaveChanges() > 0)
            {
                return service;
            }
            return new Service();
        }

        public Service GetServiceById(int idService)
        {
            return _context.Services.FirstOrDefault(site => site.IdService == idService);
        }

        public List<Service> GetServices()
        {
            return _context.Services.ToList();
        }

        public bool RemoveService(int idService)
        {
            Service? service = _context.Services.FirstOrDefault(x => x.IdService == idService);
            if (service == null)
            {
                return false;
            }else
            {
                _context.Services.Remove(service);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
            
        }

        public Service UpdateService(Service updatedService)
        {
            Service? service = _context.Services.FirstOrDefault(x => x.IdService == updatedService.IdService);
            if (service == null)
            {
                return new Service();
                
            }
            else
            {
                service.Nom = updatedService.Nom;
                _context.Services.Update(service);
                if(_context.SaveChanges() > 0)
                {
                    return service;
                }
                return new Service();
            }
        }
    }
}
