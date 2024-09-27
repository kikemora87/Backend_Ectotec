using Maqueta_Backend_EctoTec.Context;
using Maqueta_Backend_EctoTec.Custom;
using Maqueta_Backend_EctoTec.DTOs;
using Maqueta_Backend_EctoTec.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Maqueta_Backend_EctoTec.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Utilidades _utilidades;
        public AccessController(AppDbContext context, Utilidades utilidades)
        {
            _context = context;
            _utilidades = utilidades;
        }


        [HttpGet]
        [Route("exception")]
        public async Task<IActionResult> ThrowException()
        {
            throw new Exception("tst");
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Registrarse(UserDTO objeto)
        {

            var modeloUsuario = new User
            {
                Name = objeto.Name,
                Email = objeto.Email,
                Pass = _utilidades.encriptarSHA256(objeto.Pass)
            };

            await _context.Users.AddAsync(modeloUsuario);
            await _context.SaveChangesAsync();

            if (modeloUsuario.Id != 0)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO objeto)
        {
            var usuarioEncontrado = await _context.Users
                                                    .Where(u =>
                                                        u.Email == objeto.Email &&
                                                        u.Pass == _utilidades.encriptarSHA256(objeto.Pass)
                                                      ).FirstOrDefaultAsync();

            if (usuarioEncontrado == null)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.generarJWT(usuarioEncontrado) });
        }
    }
}
