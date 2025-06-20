using Models;
using Repositories.Interfaces;

namespace Repositories;

public class ClientRepository(string connectionString) : IClientRepository
{
    public Task<Client> GetClientByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}