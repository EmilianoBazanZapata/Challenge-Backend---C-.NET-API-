using API.IRepository;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CharacterController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly ILogger<MovieController> _logger;
        private readonly IMapper _mapper;
        public CharacterController(IUnitOfWork unitOfWork, ILogger<MovieController> logger, IMapper mapper)
        {
            _unitofwork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("api/Movie/ListCharacters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCharacters()
        {
            try
            {
                var characters = await _unitofwork.Characters.GetAll();
                var results = _mapper.Map<IList<CharacterDTO>>(characters);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Someting Went Wrong In The {nameof(GetCharacters)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
    }
}
