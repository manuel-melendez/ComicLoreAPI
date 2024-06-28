﻿using AutoMapper;
using ComicLoreApi.Entities;
using ComicLoreApi.Models;
using ComicLoreApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComicLoreApi.Controllers
{
    [Route("api/powers")]
    [ApiController]
    [Authorize]
    public class PowersController : ControllerBase
    {
        private readonly IPowerRepository _powerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PowersController> _logger;

        public PowersController(IPowerRepository powerRepository, IMapper mapper, ILogger<PowersController> logger)
        {
            _powerRepository = powerRepository ?? throw new ArgumentNullException(nameof(powerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Power>>> GetPowers()
        {
            var powers = await _powerRepository.getAllPowersAsync();
            if (powers == null)
            {
                _logger.LogInformation("No powers found");
                return NotFound();
            }

            var powerDtosToReturn = _mapper.Map<IEnumerable<PowerDto>>(powers);

            return Ok(powerDtosToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPowerById(int id)
        {
            var power = await _powerRepository.getPowerByIdAsync(id);

            if (power == null)
            {
                _logger.LogInformation($"Power with id {id} not found");
                return NotFound();
            }

            return Ok(_mapper.Map<PowerDto>(power));
        }

        [HttpPost]
        public async Task<ActionResult<Power>> AddPower(PowerDto powerDto)
        {
            var power = _mapper.Map<Power>(powerDto);

            await _powerRepository.addPowerAsync(power);

            return CreatedAtAction(nameof(GetPowerById), new { id = power.Id }, power);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePower(int id, Power power)
        {
            if (id != power.Id)
            {
                _logger.LogInformation("Id mismatch");
                return BadRequest();
            }

            await _powerRepository.addPowerAsync(power);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePower(int id)
        {
            var power = await _powerRepository.getPowerByIdAsync(id);

            if (power == null)
            {
                _logger.LogInformation($"Power with id {id} not found");
                return NotFound();
            }

            _powerRepository.deletePower(power);

            await _powerRepository.SaveChangesAsync();

            return NoContent();
        }

    }
}
