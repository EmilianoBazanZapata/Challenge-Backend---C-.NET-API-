using API.Data;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcountController : Controller
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ILogger<AcountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;
        private readonly IEmailSender _emailSender;
        public AcountController(UserManager<ApiUser> userManager,
                                ILogger<AcountController> logger,
                                IMapper mapper,
                                IAuthManager authManager,
                                IEmailSender emailSender)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
            _emailSender = emailSender;
        }
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {

            //_logger.LogInformation($"Registration Attempt for {userDTO}");
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //try
            //{
            //    var user = _mapper.Map<ApiUser>(userDTO);
            //    user.UserName = userDTO.Email;
            //    user.FisrtName = userDTO.FirstName;
            //    var result = await _userManager.CreateAsync(user, userDTO.Password);
            //    if (!result.Succeeded)
            //    {
            //        foreach (var error in result.Errors)
            //        {
            //            ModelState.AddModelError(error.Code, error.Description);
            //        }
            //        return BadRequest(ModelState);
            //    }
            //    await _userManager.AddToRolesAsync(user, userDTO.Roles);
                await _emailSender.SendEmailAsync(userDTO.Email, "Bienvenido a Disney", "Gracias por inscribirse al sistema de peliculas de Disney, desde ya muchas gracias y ahora a disfrutar de su subcripcion");
                return Accepted($"successfully registered user");

            //}
            //catch (Exception ex)
            //{

            //    _logger.LogError(ex, $"Something Went Wrong in the {nameof(Register)}");
            //    return Problem($"Something Went Wrong in the {nameof(Register)}", statusCode: 500);
            //}
        }
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            _logger.LogInformation($"Login Attempt for {userDTO.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!await _authManager.ValidateUser(userDTO))
                {
                    return Unauthorized();
                }

                return Accepted(new { Token = await _authManager.CreateToken() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Login)}");
                return Problem($"Something Went Wrong in the {nameof(Login)}", statusCode: 500);
            }
        }
    }
}
