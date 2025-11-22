using System.Data;
using MySql.Data.MySqlClient;
using SafeVault.Utils;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void RegisterUser(string username, string email, string password)
    {
        var hashedPassword = Authentication.HashPassword(password);

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "INSERT INTO Users (Username, Email, PasswordHash) VALUES (@Username, @Email, @PasswordHash)";
        using var cmd = new MySqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@Username", username);
        cmd.Parameters.AddWithValue("@Email", email);
        cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

        cmd.ExecuteNonQuery();
    }

    public bool AuthenticateUser(string username, string password)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT PasswordHash FROM Users WHERE Username = @Username";
        using var cmd = new MySqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@Username", username);

        var hash = cmd.ExecuteScalar() as string;
        if (hash == null) return false;

        return Authentication.VerifyPassword(password, hash);
    }
}