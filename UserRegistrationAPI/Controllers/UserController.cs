using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserRegistrationAPI.Models;
using UserRegistrationAPI.Models.DTOs;
using UserRegistrationAPI.Repository.IRepository;
using UserRegistrationAPI.Services;
using UserRegistrationAPI.Services.IServices;
using UserRegistrationAPI.Utilities;

namespace UserRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/UserRegistrationAPI")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly DataProtectionService _dataProtectionService;
        protected APIResponse _response;

        public UserController(IUserRepository userRepository, IMapper mapper, ITokenService tokenService, DataProtectionService dataProtectionService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _dataProtectionService = dataProtectionService;
            _tokenService = tokenService;
            _response = new APIResponse();
        }

        [HttpPost(Name = "UserRegistration")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UserRegistration(UserCreateDTO userCreateDTO)
        {
            var emailIsValid = RegexHelper.IsValidEmail(userCreateDTO.Email);
            var passwordIsvalid = RegexHelper.IsValidPassword(userCreateDTO.Password);
            var existingUser = await _userRepository.IsUniqueUserAsync(userCreateDTO.Email);

            if (userCreateDTO == null)
            {
                ModelState.AddModelError("User", "El usuario es requerido.");
            }

            if (!emailIsValid)
            {
                ModelState.AddModelError("Email", "Por favor, ingrese un Email válido.");
            }

            if (!passwordIsvalid)
            {
                ModelState.AddModelError("Password", "Contraseña inválida. La contraseña debe tener al menos 8 caracteres, incluir al menos una letra, un número y un carácter especial (@$!%*?&).");
            }

            if (!existingUser)
            {
                ModelState.AddModelError("Email", "El correo ya está registrado.");
            }

            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Mensaje = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(_response);
            }

            var user = _mapper.Map<User>(userCreateDTO);
            user.Password = _dataProtectionService.Protect(userCreateDTO.Password);
            user.Token = _tokenService.GenerateJwtToken(user.Email);

            //Propiedades Date
            user.Created = DateTime.UtcNow;
            user.Modified = DateTime.UtcNow;
            user.LastLogin = DateTime.UtcNow;

            await _userRepository.RegisterAsync(user);

            var userDTO = _mapper.Map<UserDTO>(user);
            _response.Result = userDTO;
            _response.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("UserRegistration", new { id = user.Id }, _response);
        }
    }
}
