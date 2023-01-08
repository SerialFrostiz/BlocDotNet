using Bloc_dotnet.Models;

namespace Bloc_dotnet.Datas
{
    public interface IServiceService
    {
        public Service AddService(Service service);
        public Service GetServiceById(int idService);
        public List<Service> GetServices();
        public Service UpdateService(Service service);
        public bool RemoveService(int idService);
    }
}
