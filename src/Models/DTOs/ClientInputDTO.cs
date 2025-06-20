using System.ComponentModel.DataAnnotations;

namespace Models.DTOs;

public class ClientInputDTO
{
    public string FullName { get; set; }

    [Required] 
    public string Email { get; set; }

    public string City { get; set; }
}