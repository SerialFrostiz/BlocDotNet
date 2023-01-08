using Bloc_dotnet.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.Text;

namespace Bloc_dotnet.Datas
{
    public class SiteService : ISiteService
    {
        private readonly ApplicationDbContext _context;
        public SiteService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Site AddSite(Site site)
        {
            _context.Sites.Add(site);
            if (_context.SaveChanges() > 0)
            {
                return site;
            }
            return new Site();
        }

        public Site GetSiteById(int idSite)
        {
            return _context.Sites.FirstOrDefault(site => site.IdSite == idSite);
        }

        public List<Site> GetSites()
        {
            return _context.Sites.ToList();
        }

        public bool RemoveSite(int idSite)
        {
            Site? site = _context.Sites.FirstOrDefault(x => x.IdSite == idSite);
            if (site == null)
            {
                return false;
            }else
            {
                _context.Sites.Remove(site);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
            
        }

        public Site UpdateSite(Site updatedSite)
        {
            Site? site = _context.Sites.FirstOrDefault(x => x.IdSite == updatedSite.IdSite);
            if (site == null)
            {
                return new Site();
                
            }
            else
            {
                site.Nom = updatedSite.Nom;
                site.Ville = updatedSite.Ville;
                _context.Sites.Update(site);
                if(_context.SaveChanges() > 0)
                {
                    return site;
                }
                return new Site();
            }
        }
    }
}
