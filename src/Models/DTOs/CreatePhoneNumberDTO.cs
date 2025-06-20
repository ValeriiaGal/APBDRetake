using System.ComponentModel.DataAnnotations;

namespace Models.DTOs;

public class CreatePhoneNumberDTO
{
    [Required]
    public string MobileNumber { get; set; }

    [Required(ErrorMessage = "Operator is required.")]
    public string Operator { get; set; }

    [Required]
    public ClientInputDTO Client { get; set; }
}