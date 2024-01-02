using FluentResults;
using System.Net;

namespace Api.Dtos
{
    /// <summary>
    /// Errors response dto
    /// </summary>
    public class ErrorResponseDto
    {
        /// <summary>
        /// Error http status code
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// Error title
        /// </summary>
        public string title { get; set; }
        /// <summary>Errors</summary>
        public List<ErrorDto> Errors { get; set; }

        /// <summary>
        /// ErrrorResponseDto constructor
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="errors"></param>
        public ErrorResponseDto(HttpStatusCode statusCode, List<IError> errors)
        {
            status = (int)statusCode;
            title = statusCode.ToString();
            Errors = errors.Select(e => new ErrorDto(e.Message, e.Reasons.Select(r => r.Message).ToList())).ToList();
        }


    }
}
