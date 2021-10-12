using System.Collections.Generic;
using System.Threading.Tasks;
using AbstractionLayer;
using Npgsql;

namespace DataLayer
{
    public class UserDal: IUserDal
    {
        public async Task<UserDto> FindByUsername(string username)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();
            UserDto user;

            await using (NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT * FROM users WHERE username=@username"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("username", username);
                await using (NpgsqlDataReader npgsqlDataReader = await cmd.ExecuteReaderAsync())
                {
                    npgsqlDataReader.Read();
                    user = new UserDto()
                    {
                        Id = npgsqlDataReader.GetString(0),
                        Username = npgsqlDataReader.GetString(1),
                        Password = npgsqlDataReader.GetString(2),

                    };
                }
            }

            return user;
        }

        public async Task<UserDto> FindById(string userId)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();
            UserDto user;

            await using (NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT * FROM users WHERE id=@userId"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("userId", userId);
                await using (NpgsqlDataReader npgsqlDataReader = await cmd.ExecuteReaderAsync())
                {
                    npgsqlDataReader.Read();
                    user = new UserDto()
                    {
                        Id = npgsqlDataReader.GetString(0),
                        Username = npgsqlDataReader.GetString(1),
                        Password = npgsqlDataReader.GetString(2),

                    };
                }
            }

            return user;
        }

        
        /// <summary>
        /// CREATE PROCEDURE SaveUser(id varchar(255), username varchar(255), password varchar(255))
        /// LANGUAGE plpgsql
        /// AS $$
        /// BEGIN
        ///     IF EXISTS(SELECT Users.id FROM users WHERE Users.id = SaveUser.id) THEN
        ///     UPDATE users SET username = SaveUser.username, password = SaveUser.password WHERE id = SaveUser.id;
        /// ELSE
        ///     INSERT INTO users (id, username, password) VALUES (SaveUser.id, SaveUser.username, SaveUser.Password);
        /// end if;
        /// end $$;
        /// </summary>
        /// <param name="userDto"></param>
        public async Task Save(UserDto userDto)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();

            await using (NpgsqlCommand cmd = new NpgsqlCommand("CALL SaveUser(@id, @username, @password)"))
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("id", userDto.Id);
                cmd.Parameters.AddWithValue("username", userDto.Username);
                cmd.Parameters.AddWithValue("password", userDto.Password);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<UserDto>> GetAll()
        {
            NpgsqlConnection conn = new NpgsqlConnection(Connection.connString);
            await conn.OpenAsync();

            List<UserDto> userDtos = new List<UserDto>();
            await using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM users"))
            {
                cmd.Connection = conn;
                await using (NpgsqlDataReader npgsqlDataReader = await cmd.ExecuteReaderAsync())
                {
                    while (npgsqlDataReader.Read())
                    {
                        userDtos.Add(new UserDto()
                        {
                            Id = npgsqlDataReader.GetString(0),
                            Username = npgsqlDataReader.GetString(1),
                            Password = npgsqlDataReader.GetString(2)
                        });
                    }
                }
            }

            await conn.CloseAsync();
            return userDtos;
        }
    }
}