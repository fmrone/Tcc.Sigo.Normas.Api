using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Api.Dtos;
using Tcc.Sigo.Normas.Api.Services;

namespace Tcc.Sigo.Normas.Api.Controllers
{
    [Route("usuarios")]
    [ApiController]
    public class UsuarioController : ApiControllerBase
    {
        /// <summary>
        /// Autentica um usuário na API e rotorna o token de autenticação
        /// </summary>
        /// <param name="usuarioPost"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("autenticar")]
        public async Task<ActionResult<dynamic>> Autenticar(
            [FromBody] UsuarioPost usuarioPost)
        {
            if (usuarioPost.Login == "usersigo" && usuarioPost.Senha == "pswsigo")
            {
                var id = Guid.Parse("92d52308-bec5-44b6-9133-746e12aff332");
                var role = "gestornormas";
                
                var token = TokenService.GenerateToken(id, role);

                return new { token };
            }
            else
            {
                return NotFound(new { mensagem = "Usuário ou senha inválidos" });
            }
        }
    }
}
