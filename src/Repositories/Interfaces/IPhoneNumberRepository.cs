using Models;

namespace Repositories.Interfaces;

public interface IPhoneNumberRepository
{
    public Task<IEnumerable<PhoneNumber>> GetAllPhoneNumbersAsync();
}