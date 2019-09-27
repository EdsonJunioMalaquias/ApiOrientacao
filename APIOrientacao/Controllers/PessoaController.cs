using System.Linq;
using APIOrientacao.Api.Request;
using APIOrientacao.Api.Response;
using APIOrientacao.Data.Context;
using APIOrientacao.Data.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace APIOrientacao.Controllers
{
    [Route("api/[controller]")]
    public class PessoaController : Controller
    {
        private readonly Contexto contexto;

        public PessoaController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PessoaResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            PessoaRequest pessoaRequest)
        {

            var pessoa = new Pessoa
            {
                Nome = pessoaRequest.Nome
            };

            contexto.Pessoa.Add(pessoa);
            contexto.SaveChanges();

            var pessoaRetorno = contexto.Pessoa.Where
                (x => x.Id == pessoa.Id)
                .FirstOrDefault();

            PessoaResponse response = new PessoaResponse();

            if (pessoaRetorno != null)
            {
                response.Id = pessoaRetorno.Id;
                response.Nome = pessoaRetorno.Nome;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(PessoaResponse), 200)]
        public IActionResult Get(int Id)
        {
            var pessoa = contexto.Pessoa.FirstOrDefault(
                x => x.Id == Id);

            return StatusCode(pessoa == null
                ? 404 :
                200, new PessoaResponse
                {
                    Id = pessoa == null ? -1 : pessoa.Id,
                    Nome = pessoa == null ? "Pessoa não encontrada"
                    : pessoa.Nome
                });
        }

    }
}