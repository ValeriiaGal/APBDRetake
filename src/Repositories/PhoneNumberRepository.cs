using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;

namespace Repositories;

public class PhoneNumberRepository(string connectionString) : IPhoneNumberRepository
{
    public async Task<IEnumerable<PhoneNumber>> GetAllPhoneNumbersAsync()
    {
        var result = new List<PhoneNumber>();

        await using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var sql = @"
                SELECT 
                    pn.Id AS PhoneId,
                    pn.Number,
                    o.Id AS OperatorId,
                    o.Name AS OperatorName,
                    c.Id AS ClientId,
                    c.Fullname,
                    c.Email,
                    c.City
                FROM PhoneNumber pn
                INNER JOIN Operator o ON pn.Operator_Id = o.Id
                INNER JOIN Client c ON pn.Client_Id = c.Id";

            await using (SqlCommand command = new SqlCommand(sql, connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var phone = new PhoneNumber
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("PhoneId")),
                            Number = reader.GetString(reader.GetOrdinal("Number")),
                            Operator = new Operator
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("OperatorId")),
                                Name = reader.GetString(reader.GetOrdinal("OperatorName"))
                            },
                            Client = new Client
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ClientId")),
                                Fullname = reader.GetString(reader.GetOrdinal("Fullname")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                City = reader.IsDBNull(reader.GetOrdinal("City"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("City"))
                            }
                        };

                        result.Add(phone);
                    }
                }
            }
        }

        return result;
    }
}