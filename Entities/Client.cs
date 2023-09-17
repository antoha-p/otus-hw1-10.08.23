using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeWork.Interface;

namespace HomeWork.Entities;

public class Client : IEntity
{
    public int Id { get; set; }

    [MaxLength(255)]
    [Required]
    public required string FirstName { get; set; }

    [MaxLength(255)]
    [Required]
    public required string LastName { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? UpdatedAt { get; set; }

    #region relations
    public virtual ICollection<Passport> Passports { get; set; } = new List<Passport>();
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    #endregion

    public override string ToString()
    {
        return $"Id={Id} | FirstName={FirstName} | LastName={LastName}";
    }
}
