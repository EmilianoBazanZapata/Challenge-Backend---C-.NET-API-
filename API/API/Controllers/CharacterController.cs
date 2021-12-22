using API.Data;
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
        [Route("api/Character/ListCharacters")]
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
        [HttpPost]
        [Route("api/Character/AddCharacter")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMovie([FromBody] CreateCharacterDTO characterDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in{nameof(AddMovie)}");
                return BadRequest(ModelState);
            }
            try
            {
                var character = _mapper.Map<Character>(characterDTO);
                await _unitofwork.Characters.Insert(character);
                await _unitofwork.Save();
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Someting Went Wrong In The {nameof(AddMovie)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
        [HttpGet]
        [Route("api/Character/SearchChanacterByName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCharacterByName(string name)
        {
            try
            {
                var Character = await _unitofwork.Characters.Get(q => q.Name == name);
                var result = _mapper.Map<CharacterDTO>(Character);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Someting Went Wrong In The {nameof(GetCharacterByName)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
        [HttpGet]
        [Route("api/Character/SearchChanacterByAge/{age}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCharacterByAge(int age)
        {
            try
            {
                var Character = await _unitofwork.Characters.Get(q => q.Age == age);
                var result = _mapper.Map<CharacterDTO>(Character);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Someting Went Wrong In The {nameof(GetCharacterByName)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
    }
}