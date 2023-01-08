using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bloc_dotnet.Models
{
    public class Service
    {
        [Key]
        public int IdService { get; set; }
        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Nom manquant.")]
        public string Nom { get; set; } = "";        
    }   
}
