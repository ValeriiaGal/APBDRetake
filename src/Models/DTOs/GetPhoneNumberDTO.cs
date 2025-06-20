using System.ComponentModel.DataAnnotations;

namespace Models.DTOs;

public class GetPhoneNumberDTO
{
    public int Id { get; set; }
    [Required]
    public string Number { get; set; }

    [Required]
    public string Operator { get; set; }

    [Required]
    public ClientDetailsDTO Client { get; set; }
}