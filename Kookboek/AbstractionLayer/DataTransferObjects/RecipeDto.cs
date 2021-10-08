using System.Collections;

namespace AbstractionLayer
{
    public class RecipeDto
    {
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        
    }
}