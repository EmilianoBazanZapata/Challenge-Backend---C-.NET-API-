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
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly ILogger<MovieController> _logger;
        private readonly IMapper _mapper;
        public MovieController(IUnitOfWork unitOfWork, ILogger<MovieController> logger, IMapper mapper)
        {
            _unitofwork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("api/Movie/ListMovies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMovies()
        {
            try
            {
                var movies = await _unitofwork.Movies.GetAll();
                var results = _mapper.Map<IList<MovieDTO>>(movies);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Someting Went Wrong In The {nameof(GetMovies)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
        [HttpPost]
        [Route("api/Movie/AddMovie")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMovie([FromBody] CreateMovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in{nameof(AddMovie)}");
                return BadRequest(ModelState);
            }
            try
            {
                var movie = _mapper.Map<Movie>(movieDTO);
                await _unitofwork.Movies.Insert(movie);
                await _unitofwork.Save();
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Someting Went Wrong In The {nameof(GetMovies)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
        [HttpGet]
        [Route("api/Movie/SearchMovieById/{id}", Name = "GetMovieById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMovieById(int id)
        {
            try
            {
                var Country = await _unitofwork.Movies.Get(q => q.IdMovie == id, new List<string> { "Gender" });
                var result = _mapper.Map<MovieDTO>(Country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Somehing Went Wrong in the {nameof(GetMovieById)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        [HttpGet]
        [Route("api/Movie/OrdeBy/{order}", Name = "MoviesOrderBy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMovieAndOrder(string order)
        {
            try
            {
                var Country = await _unitofwork.Movie.GetAllAndOrderBy(order);
                var result = _mapper.Map<IList<MovieDTO>>(Country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Somehing Went Wrong in the {nameof(GetMovieAndOrder)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        [HttpGet]
        [Route("api/Movie/SearchMovieByGender/{idGender}", Name = "MoviesByGender")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMovieByGender(int idGender)
        {
            try
            {
                var Country = await _unitofwork.Movie.GetAllByGender(idGender);
                var result = _mapper.Map<IList<MovieDTO>>(Country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Somehing Went Wrong in the {nameof(GetMovieById)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        [HttpGet]
        [Route("api/Movie/SearchMovieByName/{name}", Name = "GetMovieByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMovieByName(string name)
        {
            try
            {
                var Country = await _unitofwork.Movie.GetAllByName(name);
                var result = _mapper.Map<IList<MovieDTO>>(Country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Somehing Went Wrong in the {nameof(GetMovieByName)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        [HttpPut]
        [Route("api/Movie/UpdateMovie/{idUpdate}", Name = "UpdateMovie")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMovie(int idUpdate, [FromBody] UpdateMovieDTO movieDTO)
        {
            if (!ModelState.IsValid || idUpdate < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMovie)}");
                return BadRequest(ModelState);
            }
            try
            {
                var movie = await _unitofwork.Movies.Get(x => x.IdMovie == idUpdate);
                if (movie == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMovie)}");
                    return BadRequest("Submitted data is Invalid");
                }
                if (movieDTO.GenderId == 0) 
                {
                    return BadRequest("The Gender Is Required");
                }
                _mapper.Map(movieDTO, movie);
                _unitofwork.Movies.Update(movie);
                await _unitofwork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong in the {nameof(UpdateMovie)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        [HttpDelete]
        [Route("api/Movie/DeleteMovie/{idDelete}", Name = "DeleteMovie")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMovie(int idDelete)
        {
            if (idDelete < 1) 
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteMovie)}");
                return BadRequest();
            }
            try
            {
                var movie = await _unitofwork.Movies.Get(x=>x.IdMovie == idDelete);
                if (movie == null)
                {
                    return BadRequest("Submmited data is Invalid");
                }
                await _unitofwork.Movies.Delete(idDelete);
                await _unitofwork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong in the {nameof(DeleteMovie)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
    }
}