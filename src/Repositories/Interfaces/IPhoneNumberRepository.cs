using Models;
using Models.DTOs;

namespace Repositories.Interfaces;

public interface IPhoneNumberRepository
{
    public Task<IEnumerable<PhoneNumber>> GetAllPhoneNumbersAsync();
    public Task<int> CreatePhoneNumberAsync(CreatePhoneNumberDTO phoneNumber);
}