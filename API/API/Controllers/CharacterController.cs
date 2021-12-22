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
        public async Task<IActionResult> AddCharacter([FromBody] CreateCharacterDTO characterDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in{nameof(AddCharacter)}");
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

                _logger.LogError($"Someting Went Wrong In The {nameof(AddCharacter)}", ex);
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
        [HttpGet]
        [Route("api/Character/SearchChanacterByMovie/{movie}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCharacterByMovie(int movie)
        {
            try
            {
                var Character = await _unitofwork.Character.GetByIdMovie(movie);
                var result = _mapper.Map<IList<CharacterDTO>>(Character);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Someting Went Wrong In The {nameof(GetCharacterByMovie)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
        [HttpPut]
        [Route("api/Character/UpdateCharacter/{idUpdate}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCharacter(int idUpdate, [FromBody] UpdateCharacterDTO movieDTO)
        {
            if (!ModelState.IsValid || idUpdate < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCharacter)}");
                return BadRequest(ModelState);
            }
            try
            {
                var character = await _unitofwork.Characters.Get(x => x.IdCaharacter == idUpdate);
                if (character == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCharacter)}");
                    return BadRequest("Submitted data is Invalid");
                }
                _mapper.Map(movieDTO, character);
                _unitofwork.Characters.Update(character);
                await _unitofwork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong in the {nameof(UpdateCharacter)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        [HttpDelete]
        [Route("api/Character/DeleteCharacter/{idDelete}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCharacter(int idDelete)
        {
            if (idDelete < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCharacter)}");
                return BadRequest();
            }
            try
            {
                var character = await _unitofwork.Characters.Get(x => x.IdCaharacter == idDelete);
                if (character == null)
                {
                    return BadRequest("Submmited data is Invalid");
                }
                await _unitofwork.Characters.Delete(idDelete);
                await _unitofwork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong in the {nameof(DeleteCharacter)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
    }
}