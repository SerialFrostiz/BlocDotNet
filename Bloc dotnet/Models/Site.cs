using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bloc_dotnet.Models
{
    public class Site
    {
        [Key]
        public int IdSite { get; set; }
        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Nom manquant.")]
        public string Nom { get; set; } = "";
        [Display(Name = "Ville")]
        [Required(ErrorMessage = "Ville manquante")]
        public string Ville  { get; set; } = "";
    }   
}
