namespace Models.DTOs;

public class CreatePhoneNumberDTO
{
    public string Operator { get; set; }
    public string MobileNumber { get; set; }


    public ClientInputDTO Client { get; set; }
}