using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShelter.Api.DTOs.RequestDTOs;
using PetShelter.Api.DTOs.ResponseDTOs;
using PetShelter.Api.Services.Interfaces;

namespace PetShelter.Api.Controllers
{
    /// <summary>
    /// Provides endpoints for managing adopters.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AdoptersController : ControllerBase
    {
        private readonly IAdopterService _adopterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoptersController"/> class.
        /// </summary>
        /// <param name="adopterService">Service responsible for adopter operations.</param>
        public AdoptersController(IAdopterService adopterService)
        {
            _adopterService = adopterService;
        }

        /// <summary>
        /// Retrieves a paginated list of adopters with optional filtering and sorting.
        /// </summary>
        /// <param name="lastName">Optional last name filter.</param>
        /// <param name="phone">Optional phone number filter.</param>
        /// <param name="sortBy">Field used for sorting.</param>
        /// <param name="ascending">Determines whether sorting is ascending.</param>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Number of records per page.</param>
        /// <returns>A paginated collection of adopters.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? lastName,
            [FromQuery] string? phone,
            [FromQuery] string? sortBy,
            [FromQuery] bool ascending,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var (items, totalCount) = await _adopterService.GetAllAsync(
                lastName,
                phone,
                sortBy,
                ascending,
                page,
                pageSize);

            return Ok(new
            {
                Items = items,
                TotalCount = totalCount
            });
        }

        /// <summary>
        /// Retrieves an adopter by its unique identifier.
        /// </summary>
        /// <param name="id">Adopter identifier.</param>
        /// <returns>The requested adopter.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AdopterOutput>> GetById(int id)
        {
            var adopter = await _adopterService.GetByIdAsync(id);
            return Ok(adopter);
        }

        /// <summary>
        /// Creates a new adopter.
        /// </summary>
        /// <param name="input">Adopter information.</param>
        /// <returns>The newly created adopter.</returns>
        [HttpPost]
        public async Task<ActionResult<AdopterOutput>> Create([FromBody] AdopterInput input)
        {
            var output = await _adopterService.CreateAsync(input);

            return CreatedAtAction(
                nameof(GetById),
                new { id = output.Id },
                output);
        }

        /// <summary>
        /// Updates an existing adopter.
        /// </summary>
        /// <param name="id">Adopter identifier.</param>
        /// <param name="input">Updated adopter information.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AdopterInput input)
        {
            await _adopterService.UpdateAsync(id, input);
            return NoContent();
        }

        /// <summary>
        /// Deletes an adopter.
        /// </summary>
        /// <param name="id">Adopter identifier.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _adopterService.DeleteAsync(id);
            return NoContent();
        }
    }
}