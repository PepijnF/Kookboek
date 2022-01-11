using System;
using System.Collections.Generic;
using System.Linq;
using AbstractionLayer;

namespace LogicLayer
{
    public class CookingBook
    {
        public string Id { get; }
        public string OwnerId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public List<Recipe> Recipes { get; init; }

        private readonly ICookingBookDal _cookingBookDal;

        public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
            _cookingBookDal.Save(new CookingBookDto()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description
            }, Recipes.Select(r => r.Id).ToList());
        }

        public CookingBook(ICookingBookDal cookingBookDal)
        {
            Id = Guid.NewGuid().ToString();
            _cookingBookDal = cookingBookDal;
            Recipes = new List<Recipe>();
        }

        public CookingBook(CookingBookDto cookingBookDto, List<Recipe> recipes)
        {
            Id = cookingBookDto.Id;
            OwnerId = cookingBookDto.OwnerId;
            Name = cookingBookDto.Name;
            Description = cookingBookDto.Description;
            Recipes = recipes;
        }

        public void Save()
        {
            var cookingBookDto = new CookingBookDto()
            {
                Id = this.Id,
                OwnerId = this.OwnerId,
                Description = this.Description,
                Name = this.Name
            };

            _cookingBookDal.Save(cookingBookDto, Recipes.Select(r => r.Id).ToList());
        }
    }
}