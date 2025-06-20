using Models;

namespace Repositories.Interfaces;

public interface IClientRepository
{
    public Task<Client?> GetClientByEmailAsync(string email);
}