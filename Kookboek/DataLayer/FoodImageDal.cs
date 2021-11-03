using System.Threading.Tasks;
using AbstractionLayer;
using Npgsql;

namespace DataLayer
{
    public class FoodImageDal: IFoodImageDal
    {
        
        /// <summary>
        /// CREATE PROCEDURE SaveImage(imageId varchar(255), image bytea)
        /// LANGUAGE plpgsql
        /// AS $$
        /// BEGIN
        ///     IF EXISTS(SELECT id FROM recipeImages WHERE id = imageId) THEN
        ///     UPDATE recipeImages SET image = SaveImage.image WHERE id = imageId;
        /// ELSE
        ///     INSERT INTO recipeImages (id, image) VALUES (imageId, SaveImage.image);
        /// end if;
        /// end $$;
        /// </summary>
        /// <param name="foodImageDto"></param>
        public async Task Save(FoodImageDto foodImageDto)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();

            await using (NpgsqlCommand cmd = new NpgsqlCommand("CALL SaveImage(@id, @image, @recipeId)"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("id", foodImageDto.Id);
                cmd.Parameters.AddWithValue("image", foodImageDto.Image);
                cmd.Parameters.AddWithValue("recipeId", foodImageDto.RecipeId);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<FoodImageDto> FindById(string foodImageId)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();
            FoodImageDto foodImageDto;

            await using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM recipeImages WHERE id = @foodImageId"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("recipeId", foodImageId);
                await cmd.ExecuteReaderAsync();
                
                
                await using (NpgsqlDataReader npgsqlDataReader = cmd.ExecuteReader())
                {
                    npgsqlDataReader.Read();
                    foodImageDto = new FoodImageDto()
                    {
                        Id = npgsqlDataReader.GetString(0),
                        Image = (byte[])npgsqlDataReader.GetValue(1)
                    };
                }
            }

            return foodImageDto;
        }

        public async Task<FoodImageDto> FindByRecipeId(string recipeId)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();
            FoodImageDto foodImageDto;

            await using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM recipeImages WHERE recipeId = @recipeId"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("recipeId", recipeId);

                await using (NpgsqlDataReader npgsqlDataReader = await cmd.ExecuteReaderAsync())
                {
                    npgsqlDataReader.Read();
                    if (npgsqlDataReader.HasRows)
                    {
                        foodImageDto = new FoodImageDto()
                        {
                            Id = npgsqlDataReader.GetString(0),
                            Image = (byte[])npgsqlDataReader.GetValue(1)
                        };    
                    }
                    else
                    {
                        foodImageDto = new FoodImageDto();
                    }
                    
                }
            }

            return foodImageDto;
        }

        public async Task RemoveByRecipeId(string recipeId)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();

            await using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM recipeimages WHERE recipeid = @recipeId"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("recipeId", recipeId);
                await cmd.ExecuteNonQueryAsync();
            }

            await conn.CloseAsync();
        }
    }
}