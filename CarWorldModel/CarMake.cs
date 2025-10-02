using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarWorldModel;

public partial class CarMake
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("make")]
    [StringLength(30)]
    [Unicode(false)]
    public string Make { get; set; } = null!;

    [Column("origin")]
    [StringLength(50)]
    [Unicode(false)]
    public string Origin { get; set; } = null!;

    [InverseProperty("Make")]
    public virtual ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
}
