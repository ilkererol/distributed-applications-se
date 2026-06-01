using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShelter.Api.DTOs.RequestDTOs;
using PetShelter.Api.DTOs.ResponseDTOs;
using PetShelter.Api.Services.Interfaces;

namespace PetShelter.Api.Controllers
{
    /// <summary>
    /// Provides endpoints for managing adoption applications.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        /// <summary>
        /// Initializes a new instance of the ApplicationsController class.
        /// </summary>
        /// <param name="applicationService">Service responsible for application operations.</param>
        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Retrieves a paginated list of adoption applications with optional filtering and sorting.
        /// </summary>
        /// <param name="species">Optional animal species filter.</param>
        /// <param name="applicationDate">Optional application date filter.</param>
        /// <param name="sortBy">Field used for sorting.</param>
        /// <param name="ascending">Determines whether sorting is ascending.</param>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Number of records per page.</param>
        /// <returns>A paginated collection of applications.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? species,
            [FromQuery] DateTime? applicationDate,
            [FromQuery] string? sortBy,
            [FromQuery] bool ascending,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var (items, totalCount) = await _applicationService.GetAllAsync(species, applicationDate, sortBy, ascending, page, pageSize);

            return Ok(new
            {
                Items = items,
                TotalCount = totalCount
            });
        }

        /// <summary>
        /// Retrieves an adoption application by its unique identifier.
        /// </summary>
        /// <param name="id">Application identifier.</param>
        /// <returns>The requested application.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationOutput>> GetById(int id)
        {
            var application = await _applicationService.GetByIdAsync(id);
            return Ok(application);
        }

        /// <summary>
        /// Creates a new adoption application.
        /// </summary>
        /// <param name="input">Application information.</param>
        /// <returns>The newly created application.</returns>
        [HttpPost]
        public async Task<ActionResult<ApplicationOutput>> Create([FromBody] ApplicationInput input)
        {
            var output = await _applicationService.CreateAsync(input);

            return CreatedAtAction(nameof(GetById), new { id = output.Id }, output);
        }

        /// <summary>
        /// Updates an existing adoption application.
        /// </summary>
        /// <param name="id">Application identifier.</param>
        /// <param name="input">Updated application information.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ApplicationInput input)
        {
            await _applicationService.UpdateAsync(id, input);
            return NoContent(); 
        }

        /// <summary>
        /// Deletes an adoption application.
        /// </summary>
        /// <param name="id">Application identifier.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _applicationService.DeleteAsync(id);
            return NoContent();
        }
    }
}