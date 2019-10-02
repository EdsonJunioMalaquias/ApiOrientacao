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
    public class SituacaoController : Controller
    {
        private readonly Contexto contexto;

        public SituacaoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SituacaoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            SituacaoRequest situacaoRequest)
        {

            var situacao = new Situacao
            {
                Nome = situacaoRequest.Nome
            };

            contexto.Situacao.Add(situacao);
            contexto.SaveChanges();

            var situacaoRetorno = contexto.Situacao.Where
                (x => x.Id == situacao.Id)
                .FirstOrDefault();

            SituacaoResponse response = new SituacaoResponse();

            if (situacaoRetorno != null)
            {
                response.IdSituacao = situacaoRetorno.Id;
                response.Nome = situacaoRetorno.Nome;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idSituacao}")]
        [ProducesResponseType(typeof(SituacaoResponse), 200)]
        public IActionResult Get(int idSituacao)
        {
            var situacao = contexto.Situacao.FirstOrDefault(
                x => x.Id == idSituacao);

            return StatusCode(situacao == null
                ? 404 :
                200, new SituacaoResponse
                {
                    IdSituacao = situacao == null ? -1 : situacao.Id,
                    Nome = situacao == null ? "Situacao não encontrada"
                    : situacao.Nome
                });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SituacaoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] SituacaoRequest situacaoRequest)
        {
            try
            {
                var situacao = contexto.Situacao.Where(x => x.Id == id)
                        .FirstOrDefault();

                if (situacao != null)
                {
                    situacao.Nome = situacaoRequest.Nome;
                }

                contexto.Entry(situacao).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

                var situacaoRetorno = contexto.Situacao.FirstOrDefault
                (
                    x => x.Id == id
                );

                SituacaoResponse retorno = new SituacaoResponse()
                {
                    IdSituacao = situacaoRetorno.Id,
                    Nome = situacaoRetorno.Nome
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
                var situacao = contexto.Situacao.FirstOrDefault(
                    x => x.Id == id);

                if (situacao != null)
                {
                    contexto.Situacao.Remove(situacao);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Situacao excluída com sucesso!");
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message
                    .FirstOrDefault());
            }
        }

    }
}