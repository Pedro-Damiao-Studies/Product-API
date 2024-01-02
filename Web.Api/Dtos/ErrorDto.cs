namespace Api.Dtos
{
    /// <summary>
    /// Error DTO
    /// </summary>
    public class ErrorDto
    {
        /// <summary>Error message</summary>
        public string Message { get; set; }
        /// <summary>Error reasons</summary>
        public List<string> Reasons { get; set; }

        /// <summary>
        /// ErrorDto constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="reasons"></param>
        public ErrorDto(string message, List<string> reasons)
        {
            Message = message;
            Reasons = reasons;
        }
    }
}
