namespace Service.Dtos
{
    /// <summary>
    /// Update Product Dto
    /// </summary>
    public class UpdateProductDto
    {
        /// <summary> Product Name </summary>
        public string? Name { get; set; }
        /// <summary> Product Description </summary>
        public string? Description { get; set; }
        /// <summary> Product Available Quantity </summary>
        public long? AvailableQuantity { get; set; }
        /// <summary> Product Price </summary>
        public decimal? Price { get; set; }
    }
}
