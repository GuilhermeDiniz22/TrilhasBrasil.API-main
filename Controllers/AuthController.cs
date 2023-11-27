using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrilhasBrasil.API.Models.Dtos;
using TrilhasBrasil.API.Repositories;

namespace TrilhasBrasil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;// injeção do user manager do identity
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar(UsuarioPostDto usuario) // metodo para criar um novo usuário
        {
            var IdentityUser = new IdentityUser
            {
                UserName = usuario.UserName,
                Email = usuario.UserName
            };

            var resultado = await userManager.CreateAsync(IdentityUser, usuario.Password);

            if (resultado.Succeeded)
            {
                if (usuario.Roles is not null && usuario.Roles.Any())
                {
                    resultado = await userManager.AddToRolesAsync(IdentityUser, usuario.Roles);
                }

                if (resultado.Succeeded)
                {
                    return Ok("Usário Registrado com Sucesso!");
                }

            }
            return BadRequest("Registro falhou!");

        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuario)
        {
            var loginUser = await userManager.FindByEmailAsync(usuario.UserName);

            if (loginUser is not null)
            {
                var resultado = await userManager.CheckPasswordAsync(loginUser, usuario.Password);

                if (resultado)
                {
                    //pegar os roles do usuário

                    var roles = await userManager.GetRolesAsync(loginUser);

                    if(roles is not null)
                    {
                        // criar o token

                        var token = tokenRepository.CriarJwtToken(loginUser, roles.ToList());

                        var resposta = new LoginRespostaDto
                        {
                            Resposta = token
                        };

                        return Ok(resposta);
                    }

                   

                    return Ok("Login realizado com sucesso!");
                }
            }

            return BadRequest("Usuário Ou Senha incorretos.");
        }
    }
}
