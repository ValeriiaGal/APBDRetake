using Models;
using Repositories.Interfaces;

namespace Repositories;

public class PhoneNumberRepository(string connectionString) : IPhoneNumberRepository
{
    public Task<IEnumerable<PhoneNumber>> GetAllPhoneNumbersAsync()
    {
        throw new NotImplementedException();
    }
}