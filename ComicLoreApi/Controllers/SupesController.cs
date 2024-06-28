using AutoMapper;
using ComicLoreApi.Entities;
using ComicLoreApi.Models;
using ComicLoreApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComicLoreApi.Controllers
{
    [Route("api/supes")]
    [ApiController]
    [Authorize]
    public class SupesController : ControllerBase
    {
        private readonly ISupeRepository _supeRepository;
        private readonly IMapper _mapper;
        private readonly ISupeService _supeService;
        private readonly ILogger<SupesController> _logger;

        public SupesController(ISupeRepository supeRepository, IMapper mapper, ISupeService supeService, ILogger<SupesController> logger)
        {
            _supeRepository = supeRepository ?? throw new ArgumentNullException(nameof(supeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _supeService = supeService ?? throw new ArgumentNullException(nameof(supeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supe>>> GetSupes()
        {
            var supes = await _supeRepository.getAllSupesAsync();
            if (supes == null)
            {
                _logger.LogInformation("No supes found");
                return NotFound();
            }

            var supeDtosToReturn = _mapper.Map<IEnumerable<SupeWithPowersDto>>(supes);

            return Ok(supeDtosToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSupeById(int id)
        {
            var supe = await _supeRepository.getSupeByIdAsync(id);

            if(supe == null)
            {
                _logger.LogInformation($"Supe with id {id} not found");
                return NotFound();
            }

            var supeDtoToReturn = _mapper.Map<SupeWithPowersDto>(supe);

            return Ok(supeDtoToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<Supe>> AddSupe(SupeForUpdateDto supeForCreationDto)
        {
            var supe = _mapper.Map<Supe>(supeForCreationDto);

            await _supeRepository.addSupeAsync(supe);

            return CreatedAtAction(nameof(GetSupeById), new { id = supe.Id }, supe);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSupe(int id)
        {
            var supe = await _supeRepository.getSupeByIdAsync(id);

            if (supe == null)
            {
                _logger.LogInformation($"Supe with id {id} not found");
                return NotFound();
            }

            _supeRepository.deleteSupe(supe);

            await _supeRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSupe(int id, SupeForUpdateDto supeForUpdateDto)
        {
            var supe = await _supeRepository.getSupeByIdAsync(id);

            if (supe == null)
            {
                _logger.LogInformation($"Supe with id {id} not found");
                return NotFound();
            }

            supe.Alias = supeForUpdateDto.Alias;

            await _supeRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("${supeId}/powers/{powerId}")]
        public async Task<ActionResult> AddPowerToSupe(int supeId, int powerId)
        {
            await _supeService.AddPowerToSupeAsync(supeId, powerId);

            return NoContent();
        }
    }
}
