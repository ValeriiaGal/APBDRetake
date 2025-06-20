using Models.DTOs;

namespace Application.Interfaces;

public interface IOperatorsService
{
    public Task<List<GetPhoneNumberDTO>> GetAllPhoneNumbersAsync();
    public Task<int> CreatePhoneNumberAsync(CreatePhoneNumberDTO dto);
}