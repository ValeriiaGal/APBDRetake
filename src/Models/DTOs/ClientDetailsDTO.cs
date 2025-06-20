using System.ComponentModel.DataAnnotations;

namespace Models.DTOs;

public class ClientDetailsDTO
{
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    public string Email { get; set; }

    public string? City { get; set; }
}