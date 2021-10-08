namespace AbstractionLayer
{
    public class FoodImageDto
    {
        public string Id { get; set; }
        public byte[] Image { get; set; }
        public string RecipeId { get; set; }

        public FoodImageDto(){}
    }
}