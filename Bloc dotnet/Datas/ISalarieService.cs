using Bloc_dotnet.Models;

namespace Bloc_dotnet.Datas
{
    public interface ISalarieService
    {
        public Salarie AddSalarie(Salarie salarie);
        public Salarie GetSalarieById(int idSalarie);
        public List<Salarie> GetSalaries();
        public Salarie UpdateSalarie(Salarie salarie);
        public bool RemoveSalarie(int idSalarie);
    }
}
