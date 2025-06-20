using Application.Interfaces;
using Models;
using Models.DTOs;
using Repositories.Interfaces;

namespace Application;

public class OperatorService(
    IPhoneNumberRepository repository
) : IOperatorService
{
    public async Task<List<GetPhoneNumberDTO>> GetAllPhoneNumbersAsync()
    {
        var query = await repository.GetAllPhoneNumbersAsync();

        return query.Select(q => new GetPhoneNumberDTO
        {
            Id = q.Id,
            Number = q.Number,
            Operator = q.Operator.Name,
            Client = new ClientDetailsDTO
            {
                Id = q.Client.Id,
                FullName = q.Client.Fullname,
                Email = q.Client.Email,
                City = q.Client.City
            }
        }).ToList();
    }

    public Task<int> CreatePhoneNumberAsync(CreatePhoneNumberDTO dto)
    {
        throw new NotImplementedException();
    }
}