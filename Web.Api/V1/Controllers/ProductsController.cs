using Api.Dtos;
using Asp.Versioning;
using Domain.Models;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Services.Interfaces;
using System.Net;

namespace Api.V1.Controllers
{
    /// <summary>
    /// Products controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Products controller constructor
        /// </summary>
        /// <param name="productService"></param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Find all products
        /// </summary>
        /// <remarks> Find all products created </remarks>
        /// <param name="pageNumber">The page number to return</param>
        /// <param name="pageSize">The page size to return</param>
        /// <param name="nameToFind">The name to find</param>
        /// <param name="propertyToOrderBy">The property to order by</param>
        /// <returns>A list of product according to filters</returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int pageNumber, int pageSize, string? nameToFind, string? propertyToOrderBy)
        {
            var result = await _productService.FindAll(new FindProductsDto()
            {
                NameToFind = nameToFind,
                PropertyToOrderBy = propertyToOrderBy,
                Pagination = new PaginationDto()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                }
            });

            if (result.IsFailed)
                return BadRequest(new ErrorResponseDto(HttpStatusCode.BadRequest, result.Errors));

            return Ok(result.Value);
        }

        /// <summary>
        /// Find a product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product of given id</returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService.GetById(id);
            if (result.IsFailed)
                return BadRequest(new ErrorResponseDto(HttpStatusCode.BadRequest, result.Errors));

            return Ok(result.Value);
        }

        /// <summary>
        ///  Create a Product
        /// </summary>
        /// <param name="productToCreate"></param>
        /// <returns>Created product</returns>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateProductDto productToCreate)
        {
            var result = await _productService.Create(productToCreate);
            if (result.IsFailed)
                return BadRequest(new ErrorResponseDto(HttpStatusCode.BadRequest, result.Errors));

            return CreatedAtAction(nameof(Post), new { id = result.Value.Id }, result.Value);
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productToUpdate"></param>
        /// <returns>Updated product</returns>
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProductDto productToUpdate)
        {
            var result = await _productService.Update(id, productToUpdate);
            if (result.IsFailed)
                return BadRequest(new ErrorResponseDto(HttpStatusCode.BadRequest, result.Errors));

            return Ok(result.Value);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.Delete(id);
            if (result.IsFailed)
                return BadRequest(new ErrorResponseDto(HttpStatusCode.BadRequest, result.Errors));

            return NoContent();
        }
    }
}
