using System.ComponentModel.DataAnnotations;

namespace Kookboek.Models
{
    public class RecipeModel
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Ingredients { get; set; }
        
        [Required]
        public string Preparation { get; set; }
    }
}