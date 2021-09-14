using System;
using System.Threading.Tasks;
using AbstractionLayer;
using Npgsql;

namespace DataLayer
{
    public class RecipeDal: IRecipeDal
    {
        // TODO hide connString
        private static string connString = "Host=localhost;Username=appuser;Password=1234;Database=kookboek";

        public async Task Insert(Recipe recipe)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            string stringImageId;
            await using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO recipeImages (id, image) VALUES (@id, @image)"))
            {
                cmd.Connection = conn;
                stringImageId = Guid.NewGuid().ToString();
                cmd.Parameters.AddWithValue("id", stringImageId);
                // TODO add image
                cmd.Parameters.AddWithValue("image", recipe.Image);
                await cmd.ExecuteNonQueryAsync();
            };

            await using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO recipes (id,imageId,title,ingredients,preparation) VALUES (@id,@imageId,@title,@ingredients,@prep)"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("id", Guid.NewGuid());
                cmd.Parameters.AddWithValue("imageId", stringImageId);
                cmd.Parameters.AddWithValue("title", recipe.Title);
                cmd.Parameters.AddWithValue("ingredients", recipe.Ingredients);
                cmd.Parameters.AddWithValue("prep", recipe.Preparation);
                await cmd.ExecuteNonQueryAsync();
            };
            conn.Close();
        }

        public async Task<Recipe> Get()
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            Recipe recipe;

            await using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT Title, Ingredients, Preparation, image FROM recipes INNER JOIN recipeImages rI on rI.id = recipes.ImageId;"))
            {
                cmd.Connection = conn;
                await using (NpgsqlDataReader npgsqlDataReader = await cmd.ExecuteReaderAsync())
                {
                    npgsqlDataReader.Read();
                    recipe = new Recipe()
                    {
                        Image = (byte[])npgsqlDataReader.GetValue(3),
                        Title = npgsqlDataReader.GetString(0),
                        Ingredients = npgsqlDataReader.GetString(1),
                        Preparation = npgsqlDataReader.GetString(2)
                    };
                }

                return recipe;
            }
            
        }
    }
}


