using Bloc_dotnet.Models;

namespace Bloc_dotnet.Datas
{
    public interface ISiteService
    {
        public Site AddSite(Site site);
        public Site GetSiteById(int idSite);
        public List<Site> GetSites();
        public Site UpdateSite(Site site);
        public bool RemoveSite(int idSite);
    }
}
