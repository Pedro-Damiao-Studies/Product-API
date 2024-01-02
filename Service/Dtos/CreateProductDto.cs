namespace Service.Dtos
{
    /// <summary>
    /// Create Product Dto
    /// </summary>
    public class CreateProductDto
    {
        /// <summary> Product Name </summary>
        public string Name { get; set; }
        /// <summary> Product Description </summary>
        public string Description { get; set; }
        /// <summary> Product Available Quantity </summary>
        public long AvailableQuantity { get; set; }
        /// <summary> Product Price </summary>
        public decimal Price { get; set; }

        /// <summary> Create Product Dto constructor </summary>
        public CreateProductDto(string name, string description, long availableQuantity, decimal price) {
            Name = name;
            Description = description;
            AvailableQuantity = availableQuantity;
            Price = price;
        }
    }
}
