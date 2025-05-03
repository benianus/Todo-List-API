using System.Data;
using ToDoListDataLayer.Dtos;
using Microsoft.Data.SqlClient;

namespace ToDoListDataLayer;

public class UserData
{
    public static async Task<int> SingUp(UserDto NewUser)
    {
        int userId = 0;

        try
        {
            using (var connection = new SqlConnection(DataSettings.ConnectionString))
            {
                using (var command = new SqlCommand("Sp_AddNewUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", NewUser.Name);
                    command.Parameters.AddWithValue("@Email", NewUser.Email);
                    command.Parameters.AddWithValue("@Password", NewUser.Password);

                    connection.Open();

                    object? result = await command.ExecuteScalarAsync();

                    if (result != null & int.TryParse(result?.ToString(), out int insertedId))
                    {
                        userId = insertedId;
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return userId;
    }
    public static async Task<LoginDto> Login(LoginDto user)
    {
        try
        {
            using (var connection = new SqlConnection(DataSettings.ConnectionString))
            {
                using (var command = new SqlCommand("Sp_IsUserExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);

                    connection.Open();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new LoginDto(
                                reader.GetString(reader.GetOrdinal("Email")),
                                reader.GetString(reader.GetOrdinal("Password"))
                            );
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return null;
    }
}
