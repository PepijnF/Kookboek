using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbstractionLayer;
using Npgsql;

namespace DataLayer
{
    public class RecipeDal: IRecipeDal
    {
        
        private static string connString = "Host=localhost;Username=appuser;Password=1234;Database=kookboek";

        /// <summary>
        /// CREATE PROCEDURE SaveRecipe(recipeId varchar(255), imageId varchar(255), title varchar(255), ingredients text, preparation text, ownerId varchar(255))
        ///LANGUAGE plpgsql
        ///AS $$
        ///BEGIN
        ///    IF EXISTS(SELECT id FROM recipes WHERE id = recipeId) THEN
        ///    UPDATE recipes SET ImageId = imageid, Title = title, Ingredients = ingredients, Preparation = preparation WHERE id = recipeId;
        ///ELSE
        ///    INSERT INTO recipes (id,imageId,title,ingredients,preparation,ownerid) VALUES (recipeId,imageId,title,ingredients,preparation,ownerId);
        ///end if;
        ///end $$;
        /// </summary>
        /// <param name="recipeDto"></param>
        ///
        // TODO FINISH EVERYTHING
        public async Task Save(RecipeDto recipeDto)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();

            await using (NpgsqlCommand cmd = new NpgsqlCommand("CALL SaveRecipe(@recipeId, @title, @ingredients, @preparation, @ownerId)"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("recipeId", recipeDto.Id);
                cmd.Parameters.AddWithValue("title", recipeDto.Title);
                cmd.Parameters.AddWithValue("ingredients", recipeDto.Ingredients);
                cmd.Parameters.AddWithValue("preparation", recipeDto.Preparation);
                cmd.Parameters.AddWithValue("ownerId", recipeDto.OwnerId);
                await cmd.ExecuteNonQueryAsync();
            }
            await conn.CloseAsync();
        }

        public async Task<RecipeDto> FindById(string recipeId)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();
            RecipeDto recipeDto = new RecipeDto();

            await using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM recipes WHERE id = @recipeId"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("recipeId", recipeId);
                await cmd.ExecuteReaderAsync();
                
                
                await using (NpgsqlDataReader npgsqlDataReader = cmd.ExecuteReader())
                {
                    while (npgsqlDataReader.Read())
                    {
                        recipeDto = new RecipeDto()
                        {
                            Id = npgsqlDataReader.GetString(0),
                            Title = npgsqlDataReader.GetString(1),
                            Ingredients = npgsqlDataReader.GetString(2),
                            Preparation = npgsqlDataReader.GetString(3),
                            OwnerId = npgsqlDataReader.GetString(4)
                        };
                    }
                }
            }

            return recipeDto;
        }

        public async Task<List<RecipeDto>> FindAllByUserId(string userId)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();
            List<RecipeDto> recipeDtos = new List<RecipeDto>();

            await using (NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT recipes.id,recipes.title, recipes.Preparation, recipes.Ingredients, recipes.ownerid FROM users 
                                                                            INNER JOIN recipes ON users.id = recipes.OwnerId
                                                                            INNER JOIN recipeImages ON recipes.id = recipeImages.recipeId
                                                                            WHERE Users.id = @userid;"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("userid", userId);
                
                await using (NpgsqlDataReader npgsqlDataReader = cmd.ExecuteReader())
                {
                    while (npgsqlDataReader.Read())
                    {
                        recipeDtos.Add(new RecipeDto()
                        {
                            Id = npgsqlDataReader.GetString(0),
                            Title = npgsqlDataReader.GetString(1),
                            Ingredients = npgsqlDataReader.GetString(3),
                            Preparation = npgsqlDataReader.GetString(2),
                            OwnerId = npgsqlDataReader.GetString(4)
                        });
                    }
                }
            }
            await conn.CloseAsync();
            return recipeDtos;
        }

        public async Task<List<RecipeDto>> GetAll()
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();

            List<RecipeDto> recipeDtos = new List<RecipeDto>();
            await using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM recipes"))
            {
                cmd.Connection = conn;

                await using (NpgsqlDataReader npgsqlDataReader = await cmd.ExecuteReaderAsync())
                {
                    while (npgsqlDataReader.Read())
                    {
                        recipeDtos.Add(new RecipeDto()
                        {
                            Id = npgsqlDataReader.GetString(0),
                            Title = npgsqlDataReader.GetString(1),
                            Ingredients = npgsqlDataReader.GetString(3),
                            Preparation = npgsqlDataReader.GetString(2),
                            OwnerId = npgsqlDataReader.GetString(4)    
                        });
                    }
                }
            }

            conn.CloseAsync();
            return recipeDtos;
        }

        public async Task<List<RecipeDto>> GetAllByCookingBookId(string cookingBookId)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();
            List<RecipeDto> recipeDtos = new List<RecipeDto>();
            
            await using (NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT id, Title, Ingredients, Preparation, OwnerId FROM recipes
                                                                     INNER JOIN recipes_cookingbooks rc on recipes.id = rc.recipe_id
                                                                     WHERE rc.cookingbook_id = @cookingBookId;"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("cookingBookId", cookingBookId);
                
                await using (NpgsqlDataReader npgsqlDataReader = cmd.ExecuteReader())
                {
                    while (npgsqlDataReader.Read())
                    {
                        recipeDtos.Add(new RecipeDto()
                        {
                            Id = npgsqlDataReader.GetString(0),
                            Title = npgsqlDataReader.GetString(1),
                            Ingredients = npgsqlDataReader.GetString(3),
                            Preparation = npgsqlDataReader.GetString(2),
                            OwnerId = npgsqlDataReader.GetString(4)
                        });
                    }
                }
            }
            await conn.CloseAsync();
            return recipeDtos;
        }
    }
}


