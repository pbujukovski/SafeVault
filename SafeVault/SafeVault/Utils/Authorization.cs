using System.Data;
using MySql.Data.MySqlClient;

namespace SafeVault.Utils
{
    public static class Authorization
    {
        public static bool UserHasRole(int userId, string roleName, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = @"SELECT COUNT(*) FROM UserRoles ur
                             JOIN Roles r ON ur.RoleID = r.RoleID
                             WHERE ur.UserID = @UserID AND r.RoleName = @RoleName";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.AddWithValue("@RoleName", roleName);

            var count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
    }
}