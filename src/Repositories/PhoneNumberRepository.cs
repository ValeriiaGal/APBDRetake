using Microsoft.Data.SqlClient;
using Models;
using Models.DTOs;
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

    public async Task<int> CreatePhoneNumberAsync(CreatePhoneNumberDTO phoneNumber)
    {
        using var connection = new SqlConnection(connectionString);
    await connection.OpenAsync();

    using var transaction = await connection.BeginTransactionAsync();

    try
    {
        var getOperatorCmd = new SqlCommand(
            "SELECT Id FROM Operator WHERE Name = @Name",
            connection, (SqlTransaction)transaction);

        getOperatorCmd.Parameters.AddWithValue("@Name", phoneNumber.Operator);

        var operatorIdObj = await getOperatorCmd.ExecuteScalarAsync();
        if (operatorIdObj == null)
            throw new ArgumentException("Operator not found.");

        int operatorId = (int)operatorIdObj;
        
        var getClientCmd = new SqlCommand(@"
            SELECT Id FROM Client WHERE Email = @Email",
            connection, (SqlTransaction)transaction);

        getClientCmd.Parameters.AddWithValue("@Email", phoneNumber.Client.Email);

        object? clientIdObj = await getClientCmd.ExecuteScalarAsync();

        int clientId;

        if (clientIdObj != null)
        {
            clientId = (int)clientIdObj;
        }
        else
        {
            var insertClientCmd = new SqlCommand(@"
                INSERT INTO Client (Fullname, Email, City)
                VALUES (@Fullname, @Email, @City);
                SELECT SCOPE_IDENTITY();",
                connection, (SqlTransaction)transaction);

            insertClientCmd.Parameters.AddWithValue("@Fullname", (object?)phoneNumber.Client.FullName ?? DBNull.Value);
            insertClientCmd.Parameters.AddWithValue("@Email", phoneNumber.Client.Email);
            insertClientCmd.Parameters.AddWithValue("@City", (object?)phoneNumber.Client.City ?? DBNull.Value);

            var insertedClientId = await insertClientCmd.ExecuteScalarAsync();
            clientId = Convert.ToInt32(insertedClientId);
        }
        
        var insertPhoneCmd = new SqlCommand(@"
            INSERT INTO PhoneNumber (Number, Operator_Id, Client_Id)
            VALUES (@Number, @OperatorId, @ClientId);
            SELECT SCOPE_IDENTITY();",
            connection, (SqlTransaction)transaction);

        insertPhoneCmd.Parameters.AddWithValue("@Number", phoneNumber.MobileNumber);
        insertPhoneCmd.Parameters.AddWithValue("@OperatorId", operatorId);
        insertPhoneCmd.Parameters.AddWithValue("@ClientId", clientId);

        var phoneIdObj = await insertPhoneCmd.ExecuteScalarAsync();
        int phoneNumberId = Convert.ToInt32(phoneIdObj);

        await transaction.CommitAsync();
        return phoneNumberId;
    }
    catch
    {
        await transaction.RollbackAsync();
        throw;
    }
    }
}