using Bloc_dotnet.Models;
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
                _context.Salaries.Update(salarie);
                if(_context.SaveChanges() > 0)
                {
                    return salarie;
                }
                return new Salarie();
            }
        }
    }
}
