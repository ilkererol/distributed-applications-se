using Microsoft.AspNetCore.Mvc;
using PetShelter.Api.DTOs.RequestDTOs;
using PetShelter.Api.DTOs.ResponseDTOs;
using PetShelter.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PetShelter.Api.Controllers
{
    /// <summary>
    /// Provides endpoints for managing shelter animals.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        /// <summary>
        /// Initializes a new instance of the AnimalsController class.
        /// </summary>
        /// <param name="animalService">Service used for animal operations.</param>
        public AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        /// <summary>
        /// Retrieves a paginated list of animals with optional filtering and sorting.
        /// </summary>
        /// <param name="species">Optional animal species filter.</param>
        /// <param name="isVaccinated">Optional vaccination status filter.</param>
        /// <param name="sortBy">Property used for sorting.</param>
        /// <param name="ascending">Sort direction.</param>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Number of records per page.</param>
        /// <returns>A paginated collection of animals.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? species,
            [FromQuery] bool? isVaccinated,
            [FromQuery] string? sortBy,
            [FromQuery] bool ascending = true,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var (items, totalCount) = await _animalService.GetAllAsync(species, isVaccinated, sortBy, ascending, page, pageSize);
            return Ok(new
            {
                Items = items,
                TotalCount = totalCount
            });
        }

        /// <summary>
        /// Retrieves an animal by its identifier.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalOutput>> GetById(int id)
        {
            var animal = await _animalService.GetByIdAsync(id);
            return Ok(animal);
        }

        /// <summary>
        /// Creates a new animal record.
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AnimalOutput>> Create([FromBody] AnimalInput input)
        {
            var output = await _animalService.CreateAsync(input);
            return CreatedAtAction(nameof(GetById), new { id = output.Id }, output);
        }

        /// <summary>
        /// Updates an existing animal.
        /// </summary>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AnimalInput input)
        {
            await _animalService.UpdateAsync(id, input);
            return NoContent();
        }

        /// <summary>
        /// Deletes an animal by its identifier.
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            await _animalService.DeleteAsync(id);
            return NoContent();
        }
    }
}