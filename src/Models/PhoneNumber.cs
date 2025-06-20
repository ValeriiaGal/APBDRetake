namespace Models;

public class PhoneNumber
{
    public int Id { get; set; }
    public int Operator_Id { get; set; }
    public int Client_Id { get; set; }
    public string Number { get; set; }
    
    public Operator Operator { get; set; }
    public Client Client { get; set; }
}