using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;

namespace Repositories;

public class ClientRepository(string connectionString) : IClientRepository
{
    public async Task<Client> GetClientByEmailAsync(string email)
    {
        Client client = null;
        await using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var sql = @"
        SELECT Id, Fullname, Email, City 
        FROM Client 
        WHERE Email = @Email";

            await using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())

                    client = new Client
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Fullname = reader.GetString(reader.GetOrdinal("Fullname")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        City = reader.IsDBNull(reader.GetOrdinal("City"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("City"))
                    };
            }
        }

        return client;
    }
}