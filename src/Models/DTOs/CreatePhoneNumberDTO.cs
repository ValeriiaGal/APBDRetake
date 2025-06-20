using System.ComponentModel.DataAnnotations;

namespace Models.DTOs;

public class CreatePhoneNumberDTO
{
    public string MobileNumber { get; set; }
    
    public string Operator { get; set; }
    
    public ClientInputDTO Client { get; set; }
}