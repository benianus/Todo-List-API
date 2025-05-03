using Microsoft.Extensions.Configuration;

namespace ToDoListDataLayer;

public class DataSettings
{
    public static readonly string? ConnectionString = "Server=.; Database=TodoListDb; User id=sa; Password=123456;Encrypt=False; " +
                                                        "TrustServerCertificate=True; Connection Timeout=30;";
}
