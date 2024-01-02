namespace Infrastructure.Dtos
{
    /// <summary>
    /// DTO to pagination
    /// </summary>
    public class PaginationDto
    {
        /// <summary>Current page to retrieve</summary>
        public int PageNumber { get; set; }
        /// <summary>Number of items per page</summary>
        public int PageSize { get; set; }
    }
}
