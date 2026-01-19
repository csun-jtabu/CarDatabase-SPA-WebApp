namespace COMP_584_MyServer.DTOs
{
    public class CarModelCreate
    {
        public int MakeId { get; set; }
        public required string Model { get; set; }
        public double Mpg { get; set; }
        public int Cylinders { get; set; }
        public double Displacement { get; set; }
        public int Horsepower { get; set; }
        public int Weight { get; set; }
        public double Acceleration { get; set; }
        public int ModelYear { get; set; }
    }
}
