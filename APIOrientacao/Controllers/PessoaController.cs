using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIOrientacao.Api.Request;
using APIOrientacao.Api.Response;
using APIOrientacao.Data;
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
                Nome = pessoaRequest.Nome,
                Cpf = pessoaRequest.Cpf
            };

            contexto.Pessoa.Add(pessoa);
            contexto.SaveChanges();

            var pessoaRetorno = contexto.Pessoa.Where
                (x => x.Id == pessoa.Id)
                .FirstOrDefault();

            PessoaResponse response = new PessoaResponse();

            if (pessoaRetorno != null)
            {
                response.IdPessoa = pessoaRetorno.Id;
                response.Nome = pessoaRetorno.Nome;
                response.Cpf = pessoaRetorno.Cpf;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idPessoa}")]
        [ProducesResponseType(typeof(PessoaResponse), 200)]
        public IActionResult Get(int idPessoa)
        {
            var pessoa = contexto.Pessoa.FirstOrDefault(
                x => x.Id == idPessoa);

            return StatusCode(pessoa == null
                ? 404 :
                200, new PessoaResponse
                {
                    IdPessoa = pessoa == null 
                        ? -1 
                        : pessoa.Id,
                    Nome = pessoa == null 
                        ? "Pessoa não encontrada"
                        : pessoa.Nome,
                    Cpf = pessoa == null 
                        ? "Pessoa não encontrada"
                        : pessoa.Cpf
                });
        }
        [HttpGet("cpf/{cpf}")]
        [ProducesResponseType(typeof(PessoaResponse), 200)]
        public IActionResult Get(string cpf)
        {
            var pessoa = contexto.Pessoa.FirstOrDefault(
                x => x.Cpf.Equals(cpf));

            return StatusCode(pessoa == null
                ? 404 :
                200, new PessoaResponse
                {
                    IdPessoa = pessoa == null 
                        ? -1 
                        : pessoa.Id,
                    Nome = pessoa == null 
                        ? "Pessoa não encontrada"
                        : pessoa.Nome,
                    Cpf = pessoa == null 
                        ? "Pessoa não encontrada"
                        : pessoa.Cpf
                });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PessoaResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] PessoaRequest pessoaRequest)
        {
            try
            {
                var pessoa = contexto.Pessoa.Where(x => x.Id == id)
                        .FirstOrDefault();

                if (pessoa != null)
                {
                    pessoa.Nome = pessoaRequest.Nome;
                    pessoa.Cpf = pessoaRequest.Cpf;
                }

                contexto.Entry(pessoa).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

                var pessoaRetorno = contexto.Pessoa.FirstOrDefault
                (
                    x => x.Id == id
                );

                PessoaResponse retorno = new PessoaResponse()
                {
                    IdPessoa = pessoaRetorno.Id,
                    Nome = pessoaRetorno.Nome,
                    Cpf = pessoaRetorno.Cpf
                };

                return StatusCode(200, retorno);
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.
                    Message.FirstOrDefault());
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            try
            {
                var pessoa = contexto.Pessoa.FirstOrDefault(
                    x => x.Id == id);

                if (pessoa != null)
                {
                    contexto.Pessoa.Remove(pessoa);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Pessoa excluída com sucesso!");
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message
                    .FirstOrDefault());
            }
        }

    }
}