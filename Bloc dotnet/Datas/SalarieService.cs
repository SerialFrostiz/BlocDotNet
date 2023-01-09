using Bloc_dotnet.Migrations;
using Bloc_dotnet.Models;
using Bloc_dotnet.Models.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace Bloc_dotnet.Datas
{
    public class SalarieService : ISalarieService
    {
        private readonly ApplicationDbContext _context;
        public SalarieService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Salarie AddSalarie(Salarie salarie)
        {
            _context.Salaries.Add(salarie);
            if (_context.SaveChanges() > 0)
            {
                return salarie;
            }
            return new Salarie();
        }

        public Salarie GetSalarieById(int idSalarie)
        {
            return _context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).FirstOrDefault(site => site.IdSalarie == idSalarie);
        }

        public List<Salarie> GetSalaries()
        {
            return _context.Salaries.ToList();
        }

        public bool RemoveSalarie(int idSalarie)
        {
            Salarie? salarie = _context.Salaries.FirstOrDefault(x => x.IdSalarie == idSalarie);
            if (salarie == null)
            {
                return false;
            }else
            {
                _context.Salaries.Remove(salarie);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
            
        }

        public Salarie UpdateSalarie(Salarie updatedSalarie)
        {
            Salarie? salarie = _context.Salaries.FirstOrDefault(x => x.IdSalarie == updatedSalarie.IdSalarie);
            if (salarie == null)
            {
                return new Salarie();
                
            }
            else
            {
                salarie.Nom = updatedSalarie.Nom;
                salarie.Prenom = updatedSalarie.Prenom;
                salarie.Fixe = updatedSalarie.Fixe;
                salarie.Portable = updatedSalarie.Portable;
                salarie.Email = updatedSalarie.Email;
                salarie.Service = updatedSalarie.Service;
                salarie.Site = updatedSalarie.Site;
                _context.Salaries.Update(salarie);
                if(_context.SaveChanges() > 0)
                {
                    return salarie;
                }
                return new Salarie();
            }
        }

        public List<Salarie> Search(SearchVM search)
        {
            List<Salarie> result = new List<Salarie>();
            if((search.Recherche == "" || search.Recherche == null) && search.IdService == 0 && search.IdSite == 0) 
            {
                return _context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).ToList();
            }
            else if((search.Recherche != "" && search.Recherche != null) && search.IdService == 0 && search.IdSite == 0)
            {
                List<Salarie> nom = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Nom.Contains(search.Recherche)).ToList());
                List<Salarie> prenom = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Prenom.Contains(search.Recherche)).ToList());
                List<Salarie> email = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Email.Contains(search.Recherche)).ToList());
                List<Salarie> fixe = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Fixe.Contains(search.Recherche)).ToList());
                List<Salarie> portable = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Portable.Contains(search.Recherche)).ToList());               
                result = nom.Union(prenom).Union(email).Union(fixe).Union(portable).ToList();
                return result;
            }
            else if ((search.Recherche == "" || search.Recherche == null) && search.IdService != 0 && search.IdSite == 0)
            {
                result = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Service.IdService == search.IdService).ToList());
                return result;
            }
            else if ((search.Recherche == "" || search.Recherche == null) && search.IdService == 0 && search.IdSite != 0)
            {
                result = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Site.IdSite == search.IdSite).ToList());
                return result;
            }
            else if ((search.Recherche == "" || search.Recherche == null) && search.IdService != 0 && search.IdSite != 0)
            {
                result = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Service.IdService == search.IdService).Where(s => s.Site.IdSite == search.IdSite).ToList());
                return result;
            }
            else if ((search.Recherche != "" && search.Recherche != null) && search.IdService != 0 && search.IdSite == 0)
            {
                List<Salarie> nom = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Nom.Contains(search.Recherche)).Where(s => s.Service.IdService == search.IdService).ToList());
                List<Salarie> prenom = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Prenom.Contains(search.Recherche)).ToList());
                List<Salarie> email = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Email.Contains(search.Recherche)).ToList());
                List<Salarie> fixe = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Fixe.Contains(search.Recherche)).ToList());
                List<Salarie> portable = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Portable.Contains(search.Recherche)).ToList());
                result = nom.Union(prenom).Union(email).Union(fixe).Union(portable).ToList();
                return result;
            }
            else if ((search.Recherche != "" && search.Recherche != null) && search.IdService == 0 && search.IdSite != 0)
            {
                List<Salarie> nom = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Nom.Contains(search.Recherche)).Where(s => s.Site.IdSite == search.IdSite).ToList());
                List<Salarie> prenom = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Prenom.Contains(search.Recherche)).ToList());
                List<Salarie> email = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Email.Contains(search.Recherche)).ToList());
                List<Salarie> fixe = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Fixe.Contains(search.Recherche)).ToList());
                List<Salarie> portable = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Portable.Contains(search.Recherche)).ToList());
                result = nom.Union(prenom).Union(email).Union(fixe).Union(portable).ToList();
                return result;
            }
            else if ((search.Recherche != "" && search.Recherche != null) && search.IdService != 0 && search.IdSite != 0)
            {
                List<Salarie> nom = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Nom.Contains(search.Recherche)).Where(s => s.Site.IdSite == search.IdSite).ToList());
                List<Salarie> prenom = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Prenom.Contains(search.Recherche)).ToList());
                List<Salarie> email = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Email.Contains(search.Recherche)).ToList());
                List<Salarie> fixe = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Fixe.Contains(search.Recherche)).ToList());
                List<Salarie> portable = (_context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).Where(s => s.Portable.Contains(search.Recherche)).ToList());
                result = nom.Union(prenom).Union(email).Union(fixe).Union(portable).ToList();
                return result;
            }
            else
            {
                return _context.Salaries.Include(salarie => salarie.Service).Include(salarie => salarie.Site).ToList();
            }
        }
    }
}
