namespace Domain.Models
{
    /// <summary>
    /// Product model
    /// </summary>
    public class Product
    {
        /// <summary>Product id </summary>
        public int Id { get; set; }
        /// <summary>Product Name </summary>
        public string Name { get; set; }
        /// <summary>Product Description </summary>
        public string Description { get; set; }
        /// <summary>Product Available Quantity </summary>
        public long AvailableQuantity { get; set; }
        /// <summary>Product Price </summary>
        public Decimal Price { get; set; }

        /// <summary>Product model constructor </summary>
        public Product(string name, string description, long availableQuantity, decimal price)
        {
            Name = name;
            Description = description;
            AvailableQuantity = availableQuantity;
            Price = price;
        }
    }
}
