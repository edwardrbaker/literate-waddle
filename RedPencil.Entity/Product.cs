namespace RedPencil.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public bool IsRedPencil { get; set; }
    }
}
