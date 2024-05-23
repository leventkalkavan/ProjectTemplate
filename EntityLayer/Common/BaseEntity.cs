using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Common;

public class BaseEntity
{
    [Key] 
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? UpdatedDate { get; set; }
}