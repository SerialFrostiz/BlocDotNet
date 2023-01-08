using System.ComponentModel.DataAnnotations;

namespace Bloc_dotnet.Models.ViewModels
{
    public class SalarieVM
    {
        public int IdSalarie { get; set; }

        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Display(Name = "Prenom")]
        public string Prenom { get; set; }

        [Display(Name = "Fixe")]
        public string Fixe { get; set; }

        [Display(Name = "Portable")]
        public string Portable { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Service")]
        public int IdService { get; set; }

        [Display(Name = "Site")]
        public int IdSite { get; set; }
    }
}
