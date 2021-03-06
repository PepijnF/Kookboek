using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbstractionLayer;

namespace LogicLayer
{
    public class User
    {
        public string Id { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public List<Recipe> Recipes { get; init; }
        public List<CookingBook> CookingBooks { get; set; }

        public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
        }

        public void Save(IUserDal userDal, IRecipeDal recipeDal, IFoodImageDal foodImageDal)
        {
            userDal.Save(new UserDto()
            {
                Id = Id,
                Password = Password,
                Username = Username
            });

            foreach (var recipe in Recipes)
            {
                recipe.Save(recipeDal, foodImageDal);
            }
        }

        public User(UserDto userDto)
        {
            Id = userDto.Id;
            Username = userDto.Username;
            Password = userDto.Password;
        }

        public User()
        {
            Id = System.Guid.NewGuid().ToString();
            Recipes = new List<Recipe>();
            CookingBooks = new List<CookingBook>();
        }

        public User(string username, IUserDal userDal, IRecipeDal recipeDal, IFoodImageDal foodImageDal)
        {
            UserDto userDto = userDal.FindByUsername(username).Result;
            Id = userDto.Id;
            Username = userDto.Username;
            Password = userDto.Password;

            List<RecipeDto> recipeDtos = recipeDal.FindAllByUserId(Id).Result;
            Recipes = new List<Recipe>();
            foreach (var recipeDto in recipeDtos)
            {
                Recipes.Add(new Recipe(recipeDto, foodImageDal));
            }
        }
    }
}