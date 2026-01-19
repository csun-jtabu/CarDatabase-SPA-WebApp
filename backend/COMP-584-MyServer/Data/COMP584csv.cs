namespace COMP_584_MyServer.Data
{
    public class COMP584csv
    {
        public required string make { get; set; }
        public required string model { get; set; }
        public decimal mpg { get; set; }
        public decimal cylinders { get; set; }
        public decimal displacement { get; set; }
        public decimal? horsepower { get; set; }
        public int weight { get; set; }
        public decimal acceleration { get; set; }
        public int model_year { get; set; }
        public required string origin { get; set; }

    }
}
