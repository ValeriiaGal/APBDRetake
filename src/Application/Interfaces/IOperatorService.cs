using Models.DTOs;

namespace Application.Interfaces;

public interface IOperatorService
{
    public Task<List<GetPhoneNumberDTO>> GetAllPhoneNumbersAsync();
}