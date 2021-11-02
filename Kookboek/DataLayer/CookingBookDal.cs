using System.Collections.Generic;
using System.Threading.Tasks;
using AbstractionLayer;
using Npgsql;

namespace DataLayer
{
    public class CookingBookDal : ICookingBookDal
    {
        public async Task<List<CookingBookDto>> GetAllByUserId(string userId)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();
            List<CookingBookDto> cookingBookDtos = new List<CookingBookDto>();
            
            await using (NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT id, name, description FROM CookingBooks
                                                                     INNER JOIN users_cookingbooks uc on CookingBooks.id = uc.cookingbook_id
                                                                     WHERE uc.user_id = @userid;"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("userid", userId);
                
                await using (NpgsqlDataReader npgsqlDataReader = cmd.ExecuteReader())
                {
                    while (npgsqlDataReader.Read())
                    {
                        cookingBookDtos.Add(new CookingBookDto()
                        {
                            Id = npgsqlDataReader.GetString(0),
                            Name = npgsqlDataReader.GetString(1),
                            Description = npgsqlDataReader.GetString(2)
                        });
                    }
                }
            }
            await conn.CloseAsync();
            return cookingBookDtos;
        }

        /// <summary>
        /// CREATE PROCEDURE SaveCookingBook(id varchar(255), name varchar(255), description text)
        /// LANGUAGE plpgsql
        /// AS $$
        /// BEGIN
        ///     IF EXISTS(SELECT CookingBooks.id FROM cookingbooks WHERE CookingBooks.id = SaveCookingBook.id) THEN
        ///     UPDATE cookingbooks SET cookingbooks.name = SaveCookingBook.name, cookingbooks.description = SaveCookingBook.description WHERE cookingbooks.id = SaveCookingBook.id;
        /// ELSE
        ///     INSERT INTO CookingBooks (id, name, description) VALUES (SaveCookingBook.id, SaveCookingBook.name, SaveCookingBook.description);
        /// end if;
        /// END$$;
        /// </summary>
        /// <param name="cookingBookDto"></param>
        /// <param name="recipeIds"></param>
        public async Task Save(CookingBookDto cookingBookDto, List<string> recipeIds)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();
            
            await using (NpgsqlCommand cmd = new NpgsqlCommand("CALL SaveCookingBook(@id, @name, @description)"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("id", cookingBookDto.Id);
                cmd.Parameters.AddWithValue("name", cookingBookDto.Name);
                cmd.Parameters.AddWithValue("description", cookingBookDto.Description);
                await cmd.ExecuteNonQueryAsync();
            }

            foreach (var recipeId in recipeIds)
            {
                await using (NpgsqlCommand cmd = new NpgsqlCommand("CALL UpdateRecipeReferences(@recipeId, @cookingBookId)"))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("recipeId", recipeId);
                    cmd.Parameters.AddWithValue("cookingBookId", cookingBookDto.Id);
                    await cmd.ExecuteNonQueryAsync();
                }    
            }
            
            await conn.CloseAsync();
        }
    }
}