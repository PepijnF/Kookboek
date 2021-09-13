using System;
using System.Threading.Tasks;
using AbstractionLayer;
using Npgsql;

namespace DataLayer
{
    public class RecipeDal: IRecipeDal
    {
        private static string connString = "Host=localhost;Username=appuser;Password=1234;Database=kookboek";

        public async Task Insert(Recipe recipe)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            // TODO add image saving
            await using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO recipes (id,title,ingredients,preparation) VALUES (@id,@title,@ingredients,@prep)"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("id", Guid.NewGuid());
                cmd.Parameters.AddWithValue("title", recipe.Title);
                cmd.Parameters.AddWithValue("ingredients", recipe.Ingredients);
                cmd.Parameters.AddWithValue("prep", recipe.Preparation);
                await cmd.ExecuteNonQueryAsync();
            } ;
        }
    }
}


