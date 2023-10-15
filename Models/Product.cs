namespace CrudwithDrapper.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string name { get; set; } = " ";

        public string Brand { get; set; } = " ";

        public string Category { get; set; } = " ";

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public DateTime Createdat { get; set; }
 



    }
}
