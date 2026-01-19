using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarWorldModel;

public partial class CarModel
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("make_id")]
    public int MakeId { get; set; }

    [Column("model")]
    [StringLength(50)]
    [Unicode(false)]
    public string Model { get; set; } = null!;

    [Column("mpg")]
    public double Mpg { get; set; }

    [Column("cylinders")]
    public int Cylinders { get; set; }

    [Column("displacement")]
    public double Displacement { get; set; }

    [Column("horsepower")]
    public int Horsepower { get; set; }

    [Column("weight")]
    public int Weight { get; set; }

    [Column("acceleration")]
    public double Acceleration { get; set; }

    [Column("model_year")]
    public int ModelYear { get; set; }

    [ForeignKey("MakeId")]
    [InverseProperty("CarModels")]
    public virtual CarMake Make { get; set; } = null!;
}
