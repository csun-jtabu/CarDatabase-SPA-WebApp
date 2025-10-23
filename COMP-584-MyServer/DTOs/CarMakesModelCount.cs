namespace COMP_584_MyServer.DTOs
{
    public class CarMakesModelCount
    {
        public int Id { get; set; }
        public required string Make { get; set; }
        public required string Origin { get; set; }
        public int ModelCount { get; set; }
    }
}
