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
    public class ProjetoController : Controller
    {
        private readonly Contexto contexto;

        public ProjetoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProjetoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            ProjetoRequest projetoRequest)
        {

            var projeto = new Projeto
            {
                Nome = projetoRequest.Nome,
                Encerrado = projetoRequest.Encerrado,
                PessoaAlunoId = projetoRequest.PessoaAlunoId,
                Nota = projetoRequest.Nota
            };

            contexto.Projeto.Add(projeto);
            contexto.SaveChanges();

            var projetoRetorno = contexto.Projeto.Where
                (x => x.Id == projeto.Id)
                .FirstOrDefault();

            ProjetoResponse response = new ProjetoResponse();

            if (projetoRetorno != null)
            {
                response.IdProjeto = projetoRetorno.Id;
                response.Nome = projetoRetorno.Nome;
                response.Encerrado = projetoRequest.Encerrado;
                response.PessoaAlunoId = projetoRequest.PessoaAlunoId;
                response.Nota = projetoRequest.Nota;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idProjeto}")]
        [ProducesResponseType(typeof(ProjetoResponse), 200)]
        public IActionResult Get(int idProjeto)
        {
            var projeto = contexto.Projeto.FirstOrDefault(
                x => x.Id == idProjeto);

            return StatusCode(projeto == null
                ? 404 :
                200, new ProjetoResponse
                {
                    IdProjeto = projeto == null ? -1 : projeto.Id,
                    Nome = projeto == null 
                        ? "Projeto não encontrada"
                        : projeto.Nome,
                    Encerrado = projeto == null 
                        ? false
                        : projeto.Encerrado,
                    PessoaAlunoId = projeto == null 
                        ? -1
                        : projeto.PessoaAlunoId,
                    Nota = projeto == null 
                        ? -1
                        : projeto.Nota                                                
                });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProjetoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] ProjetoRequest projetoRequest)
        {
            try
            {
                var projeto = contexto.Projeto.Where(x => x.Id == id)
                        .FirstOrDefault();

                if (projeto != null)
                {
                    projeto.Nome = projetoRequest.Nome;
                }

                contexto.Entry(projeto).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

                var projetoRetorno = contexto.Projeto.FirstOrDefault
                (
                    x => x.Id == id
                );

                ProjetoResponse retorno = new ProjetoResponse()
                {
                    IdProjeto = projetoRetorno.Id,
                    Nome = projetoRetorno.Nome,
                    Encerrado = projetoRetorno.Encerrado,
                    PessoaAlunoId = projetoRetorno.PessoaAlunoId,
                    Nota = projetoRetorno.Nota

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
                var projeto = contexto.Projeto.FirstOrDefault(
                    x => x.Id == id);

                if (projeto != null)
                {
                    contexto.Projeto.Remove(projeto);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Projeto excluída com sucesso!");
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message
                    .FirstOrDefault());
            }
        }

    }
}