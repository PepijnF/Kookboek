using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AbstractionLayer;
using Microsoft.AspNetCore.Http;

namespace Kookboek.Models
{
    public class RecipeModel
    {
        public string Id { get; set; }
        [DisplayName("Image")]
        public IFormFile Image { get; set; }
        
        public string ImageBase64 { get; set; }
        
        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }
        
        [Required]
        [DisplayName("Ingredients")]
        public string Ingredients { get; set; }
        
        [Required]
        [DisplayName("Preparation")]
        public string Preparation { get; set; }
    }
}