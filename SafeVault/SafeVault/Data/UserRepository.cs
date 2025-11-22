namespace SafeVault.Data;
// Data/UserRepository.cs
using System.Data;
using MySql.Data.MySqlClient;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void AddUser(string username, string email)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "INSERT INTO Users (Username, Email) VALUES (@Username, @Email)";
        using var cmd = new MySqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@Username", username);
        cmd.Parameters.AddWithValue("@Email", email);

        cmd.ExecuteNonQuery();
    }

    public DataTable GetUserByUsername(string username)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT * FROM Users WHERE Username = @Username";
        using var cmd = new MySqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@Username", username);

        using var adapter = new MySqlDataAdapter(cmd);
        var result = new DataTable();
        adapter.Fill(result);

        return result;
    }
}
