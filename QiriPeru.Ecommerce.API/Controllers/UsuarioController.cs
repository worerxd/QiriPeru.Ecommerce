using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QiriPeru.Ecommerce.API.Dtos;
using QiriPeru.Ecommerce.API.Errors;
using QiriPeru.Ecommerce.API.Extensions;
using QiriPeru.Ecommerce.Core.Entities;
using QiriPeru.Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.API.Controllers
{
    public class UsuarioController : BaseApiController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ITokenService tokenService, IMapper mapper, IPasswordHasher<Usuario> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _userManager.FindByEmailAsync(loginDto.Email);

            if (User == null)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, loginDto.Password, false);

            if (!resultado.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            return new UsuarioDto
            {
                Email = usuario.Email,
                UserName = usuario.UserName,
                Token = _tokenService.CreateToken(usuario),
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
            };
        }


        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioDto>> Registrar(RegistrarDto registrarDto)
        {
            var usuario = new Usuario
            {
                Email = registrarDto.Email,
                UserName = registrarDto.UserName,
                Nombre = registrarDto.Nombre,
                Apellido = registrarDto.Apellido,
            };

            var resultado = await _userManager.CreateAsync(usuario, registrarDto.Password);

            if (!resultado.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(400));
            }

            return new UsuarioDto
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Token = _tokenService.CreateToken(usuario),
                Email = usuario.Email,
                UserName = usuario.UserName,
            };
        }


        [HttpPut("actualizar/{id}")]

        public async Task<ActionResult<UsuarioDto>> Actualizar(string id, RegistrarDto registrarDto)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound(new CodeErrorResponse(404, "El usuario no existe"));
            }

            usuario.Nombre = registrarDto.Nombre;
            usuario.Apellido = registrarDto.Apellido;
            usuario.PasswordHash = _passwordHasher.HashPassword(usuario, registrarDto.Password);

            var resultado = await _userManager.UpdateAsync(usuario);

            if (!resultado.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(400, "No se pudo actualizar el usuario"));
            }

            return new UsuarioDto
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                UserName = usuario.UserName,
                Token = _tokenService.CreateToken(usuario),
                Imagen = usuario.Imagen
            };


        }



        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UsuarioDto>> GetUsuario()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            //var usuario = await _userManager.FindByEmailAsync(email);

            var usuario = await _userManager.BuscarUsuarioAsync(HttpContext.User);

            return new UsuarioDto
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                UserName = usuario.UserName,
                Token = _tokenService.CreateToken(usuario)
            };
        }

        [HttpGet("emailvalido")]
        public async Task<ActionResult<bool>> ValidarEmail([FromQuery] string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario == null) return false;

            return true;
        }

        [Authorize]
        [HttpGet("direccion")]
        public async Task<ActionResult<DireccionDto>> GetDireccion()
        {
            var usuario = await _userManager.BuscarUsuarioConDireccionAsync(HttpContext.User);

            return _mapper.Map<Direccion, DireccionDto>(usuario.Direccion);
        }


        [Authorize]
        [HttpPut("direccion")]
        public async Task<ActionResult<DireccionDto>> UpdateDireccion(DireccionDto direccion)
        {
            var usuario = await _userManager.BuscarUsuarioConDireccionAsync(HttpContext.User);

            usuario.Direccion = _mapper.Map<DireccionDto, Direccion>(direccion);

            var resultado = await _userManager.UpdateAsync(usuario);

            if (resultado.Succeeded) return Ok(_mapper.Map<Direccion, DireccionDto>(usuario.Direccion));

            return BadRequest("No se pudo actualizar la dirección del usuario");
        }

    }
}
