using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeWork.Interface;

namespace HomeWork.Entities;

public class Passport : IEntity
{
    public int Id { get; set; }

    [Required]
    public int ClientId { get; set; }

    [MaxLength(11)]
    [Required]
    public required string Number { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? UpdatedAt { get; set; }

    public Client? Client { get; set; }

    public override string ToString()
    {
        return $"Id={Id} | ClientId={ClientId} | Number={Number}";
    }
}
