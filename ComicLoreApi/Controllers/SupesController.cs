using AutoMapper;
using ComicLoreApi.Entities;
using ComicLoreApi.Models;
using ComicLoreApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ComicLoreApi.Controllers
{
    [Route("api/supes")]
    [ApiController]
    public class SupesController : ControllerBase
    {
        private readonly ISupeRepository _supeRepository;
        private readonly IMapper _mapper;

        public SupesController(ISupeRepository supeRepository, IMapper mapper)
        {
            _supeRepository = supeRepository ?? throw new ArgumentNullException(nameof(supeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supe>>> GetSupes()
        {
            var supes = await _supeRepository.getAllSupesAsync();
            if (supes == null)
            {
                return NotFound();
            }

            return Ok(supes);
        }

    }
}
