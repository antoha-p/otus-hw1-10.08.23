using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeWork.Interface;

namespace HomeWork.Entities;

public class Account : IEntity
{
    public int Id { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Precision(15,2)]
    [Required]
    public required decimal Amount { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? UpdatedAt { get; set; }

    public Client Client { get; set; } = null!;

    public override string ToString()
    {
        return $"Id={Id} | ClientId={ClientId} | Amount={Amount}";
    }
}
