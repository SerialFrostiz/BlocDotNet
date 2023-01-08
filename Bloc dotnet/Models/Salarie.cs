using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bloc_dotnet.Models
{
    public class Salarie
    {
        [Key]
        public int IdSalarie { get; set; }
        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Nom manquant.")]
        public string Nom { get; set; } = "";
        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Prénom manquant.")]
        public string Prenom { get; set; } = "";
        [Display(Name = "Téléphone fixe")]
        [Required(ErrorMessage = "Téléphone fixe manquant.")]
        public string Fixe { get; set; } = "";
        [Display(Name = "Téléphone portable")]
        [Required(ErrorMessage = "Téléphone portable manquant.")]
        public string Portable { get; set; } = "";
        [Display(Name = "Adresse email")]
        [Required(ErrorMessage = "Adresse email manquante.")]
        public string Email { get; set; } = "";
        [Display(Name = "Service")]
        [Required(ErrorMessage = "Service manquant.")]
        public virtual Service Service { get; set; } = new Service();
        [Display(Name = "Site")]
        [Required(ErrorMessage = "Site manquant.")]
        public virtual Site Site { get; set; } = new Site();
    }   
}
