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
                _logger.LogError($"Someting Went Wrong In The {nameof(GetMovies)}",ex);
                return StatusCode(500,"Internal Server Error. Please Try Again Later");
            }
        }
    }
}