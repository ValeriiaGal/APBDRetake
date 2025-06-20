using Application.Exceptions;
using Application.Interfaces;
using Models;
using Models.DTOs;
using Repositories.Interfaces;

namespace Application;

public class OperatorService(
    IPhoneNumberRepository phoneNumberRepository,
    IClientRepository clientRepository
) : IOperatorService
{
    public async Task<List<GetPhoneNumberDTO>> GetAllPhoneNumbersAsync()
    {
        var query = await phoneNumberRepository.GetAllPhoneNumbersAsync();

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
        if (!dto.MobileNumber.StartsWith("+48"))
        {
            throw new ClientInputException("Invalid mobile number. It must start with +48 and be 12 digits long.");
        }

        Task<Client> client = null;
        if (dto.Client != null && dto.Client.City == null && dto.Client.FullName == null && dto.Client.Email != null )
        {
            client = clientRepository.GetClientByEmailAsync(dto.Client.Email);

            dto.Client = new ClientInputDTO
            {
                FullName = client.Result.Fullname,
                Email = client.Result.Email,
                City = client.Result.City
            };
        }

        if (dto.Client.Email == null ||
            clientRepository.GetClientByEmailAsync(dto.Client.Email) == null)
        {
            throw new ClientInputException("No client found.");
        }

        var id = phoneNumberRepository.CreatePhoneNumberAsync(dto);

        return id;
    }
}