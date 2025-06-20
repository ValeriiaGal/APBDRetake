using Application.Exceptions;
using Application.Interfaces;
using Models;
using Models.DTOs;
using Repositories.Interfaces;

namespace Application;

public class OperatorsesService(
    IPhoneNumberRepository phoneNumberRepository,
    IClientRepository clientRepository
) : IOperatorsService
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

    public async Task<int> CreatePhoneNumberAsync(CreatePhoneNumberDTO dto)
    {
        if (!dto.MobileNumber.StartsWith("+48"))
        {
            throw new ClientInputException("Invalid mobile number. It must start with +48 and be 12 digits long.");
        }

        if (dto.Client == null || string.IsNullOrWhiteSpace(dto.Client.Email))
        {
            throw new ClientInputException("Client email is required.");
        }

        var existingClient = await clientRepository.GetClientByEmailAsync(dto.Client.Email);

        if (existingClient != null)
        {
            dto.Client = new ClientInputDTO
            {
                FullName = existingClient.Fullname,
                Email = existingClient.Email,
                City = existingClient.City
            };
        }
        else if (string.IsNullOrWhiteSpace(dto.Client.FullName) || string.IsNullOrWhiteSpace(dto.Client.City))
        {
            throw new ClientInputException("Client does not exist and not enough data provided to create a new one.");
        }

        return await phoneNumberRepository.CreatePhoneNumberAsync(dto);
    }

}