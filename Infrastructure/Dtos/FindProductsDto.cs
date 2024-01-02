namespace Infrastructure.Dtos
{
    /// <summary>
    /// DTO to produt search
    /// </summary>
    public class FindProductsDto
    {
        /// <summary>Full Name or Partial Name to find</summary>
        public string? NameToFind {get; set; }
        /// <summary>Property to order the result</summary>
        public string? PropertyToOrderBy { get; set; }
        /// <summary>Pagination of result</summary>
        public PaginationDto Pagination { get; set;}

        /// <summary>Constructor</summary>
        public FindProductsDto() {
            Pagination = new PaginationDto();
        }
    }
}
