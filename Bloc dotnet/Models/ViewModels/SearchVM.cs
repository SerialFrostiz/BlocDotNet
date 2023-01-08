using System.ComponentModel.DataAnnotations;

namespace Bloc_dotnet.Models.ViewModels
{
    public class SearchVM
    {
        [Display(Name = "Recherche")]
        public string Recherche { get; set; }

        [Display(Name = "Service")]
        public int IdService { get; set; }

        [Display(Name = "Site")]
        public int IdSite { get; set; }
    }
}
