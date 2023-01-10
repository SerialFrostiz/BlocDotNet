using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bloc_dotnet.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Login manquant.")]
        public string Login { get; set; } = "";
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Mot de passe manquant.")]
        public string Password  { get; set; } = "";
    }   
}
