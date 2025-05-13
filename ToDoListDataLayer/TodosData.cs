using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ToDoListDataLayer.Dtos;

namespace ToDoListDataLayer;

public class TodosData
{
    public async static Task<TodosDto> GetAllTasks(int page, int limite)
    {
        var task = new List<TaskDto>();

        using (var connection = new SqlConnection(DataSettings.ConnectionString))
        {
            using (var command = new SqlCommand("Sp_GetAllTasks", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Page", page);
                command.Parameters.AddWithValue("@Limite", limite);

                connection.Open();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        task.Add(new TaskDto(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("Title")),
                            reader.GetString(reader.GetOrdinal("Description")),
                            reader.GetInt32(reader.GetOrdinal("UserId"))
                        ));
                    }
                }
                connection.Close();
            }
        }

        return new TodosDto(task, page, limite);
    }
    public static async Task<TaskDto> GetTask(int id)
    {
        using (var connection = new SqlConnection(DataSettings.ConnectionString))
        {
            using (var command = new SqlCommand("Sp_GetTask", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);
                
                connection.Open();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return new TaskDto(
                            id,
                            reader.GetString(reader.GetOrdinal("Title")),
                            reader.GetString(reader.GetOrdinal("Description")),
                            reader.GetInt32(reader.GetOrdinal("UserId"))
                        );
                    }
                }
                connection.Close();
            }
        }

        return null;
    }
    public static async Task<int> CreateTask(TaskDto newTask)
    {
        int newTaskId = 0;

        try
        {
            using (var connection = new SqlConnection(DataSettings.ConnectionString))
            {
                using (var command = new SqlCommand("Sp_CreateTask", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Title", newTask.Title);
                    command.Parameters.AddWithValue("@Description", newTask.Description);
                    command.Parameters.AddWithValue("@UserId", newTask.UserId);

                    connection.Open();

                    object? result = await command.ExecuteScalarAsync();

                    if (result != null && int.TryParse(result.ToString(), out int insertedId))
                    {
                        newTaskId = insertedId;
                    }
                }
            }
        }
        catch (Exception)
        {

            throw;
        }

        return newTaskId;
    }
    public static async Task<bool> UpdateTask(int id, TaskDto newTask)
    {
        int rowsAffected = 0;

        try
        {
            using (var connection = new SqlConnection(DataSettings.ConnectionString))
            {
                using (var command = new SqlCommand("Sp_UpdateTask", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Title", newTask.Title);
                    command.Parameters.AddWithValue("@Description", newTask.Description);

                    connection.Open();

                    rowsAffected = await command.ExecuteNonQueryAsync();
                }

                connection.Close();
            }
        }
        catch (Exception)
        {

            throw;
        }

        return rowsAffected > 0;    
    }
    public static async Task<bool> DeleteTask(int id)
    {
        int rowsAffected = 0;

        try
        {
            using (var connection = new SqlConnection(DataSettings.ConnectionString))
            {
                using (var command = new SqlCommand("Sp_DeleteTask", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();

                    rowsAffected = await command.ExecuteNonQueryAsync();
                }

                connection.Close();
            }
        }
        catch (Exception)
        {

            throw;
        }

        return rowsAffected > 0;
    }

    public static async Task<TodosDto> FilterTasks(string filter, int page, int limite)
    {
        var tasks = new List<TaskDto>();

        using (var connection = new SqlConnection(DataSettings.ConnectionString))
        {
            using (var command = new SqlCommand("Sp_FilterTasks", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@filter", filter); 
                command.Parameters.AddWithValue("@page", page); 
                command.Parameters.AddWithValue("@limite", limite); 

                connection.Open();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        tasks.Add(
                            new TaskDto(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Title")),
                                reader.GetString(reader.GetOrdinal("Description")),
                                reader.GetInt32(reader.GetOrdinal("UserId"))
                            )   
                        );
                    }
                }
            }
        }

        return new TodosDto(tasks, page, limite);
    }
}
