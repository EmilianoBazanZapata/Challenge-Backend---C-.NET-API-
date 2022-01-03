using API.Data;
using API.IRepository;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static API.Models.MovieAndCharactersDTO;

namespace API.Controllers
{
    public class MovieAndCharacterController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly ILogger<MovieAndCharacterController> _logger;
        private readonly IMapper _mapper;
        public MovieAndCharacterController(IUnitOfWork unitOfWork,
                                           ILogger<MovieAndCharacterController> logger,
                                           IMapper mapper)
        {
            _unitofwork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("api/CharactersAndMovies/AddRelationShip")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRelationShip([FromBody] MovieAndCharactersDTO relationShipDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in{nameof(AddRelationShip)}");
                return BadRequest(ModelState);
            }
            try
            {
                var RelationShip = _mapper.Map<CharactersAndMovies>(relationShipDTO);
                await _unitofwork.CharactersAndMovies.Insert(RelationShip);
                await _unitofwork.Save();
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Someting Went Wrong In The {nameof(AddRelationShip)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
        [HttpPut]
        [Authorize(Roles = "Administrator")]
        [Route("api/CharactersAndMovies/UpdateRelationShip/{idUpdate}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRelationShip(int idUpdate, [FromBody] UpdateMovieAndCharactersDTO relationDTO)
        {
            if (!ModelState.IsValid || idUpdate < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateRelationShip)}");
                return BadRequest(ModelState);
            }
            try
            {
                var relation = await _unitofwork.CharactersAndMovies.Get(x => x.IdCharactersAndMovies == idUpdate);
                if (relation == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateRelationShip)}");
                    return BadRequest("Submitted data is Invalid");
                }
                _mapper.Map(relationDTO, relation);
                _unitofwork.CharactersAndMovies.Update(relation);
                await _unitofwork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong in the {nameof(UpdateRelationShip)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
    }
}
