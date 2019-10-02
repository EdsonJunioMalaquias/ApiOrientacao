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
    public class SituacaoProjetoController : Controller
    {
        private readonly Contexto contexto;

        public SituacaoProjetoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SituacaoProjetoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            SituacaoProjetoRequest situacaoProjetoRequest)
        {

            var situacaoProjeto = new SituacaoProjeto
            {
                Nome = situacaoProjetoRequest.Nome
            };

            contexto.SituacaoProjeto.Add(situacaoProjeto);
            contexto.SaveChanges();

            var situacaoProjetoRetorno = contexto.SituacaoProjeto.Where
                (x => x.Id == situacaoProjeto.Id)
                .FirstOrDefault();

            SituacaoProjetoResponse response = new SituacaoProjetoResponse();

            if (situacaoProjetoRetorno != null)
            {
                response.IdSituacaoProjeto = situacaoProjetoRetorno.Id;
                response.Nome = situacaoProjetoRetorno.Nome;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idSituacaoProjeto}")]
        [ProducesResponseType(typeof(SituacaoProjetoResponse), 200)]
        public IActionResult Get(int idSituacaoProjeto)
        {
            var situacaoProjeto = contexto.SituacaoProjeto.FirstOrDefault(
                x => x.Id == idSituacaoProjeto);

            return StatusCode(situacaoProjeto == null
                ? 404 :
                200, new SituacaoProjetoResponse
                {
                    IdSituacaoProjeto = situacaoProjeto == null ? -1 : situacaoProjeto.Id,
                    Nome = situacaoProjeto == null ? "SituacaoProjeto não encontrada"
                    : situacaoProjeto.Nome
                });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SituacaoProjetoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] SituacaoProjetoRequest situacaoProjetoRequest)
        {
            try
            {
                var situacaoProjeto = contexto.SituacaoProjeto.Where(x => x.Id == id)
                        .FirstOrDefault();

                if (situacaoProjeto != null)
                {
                    situacaoProjeto.Nome = situacaoProjetoRequest.Nome;
                }

                contexto.Entry(situacaoProjeto).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

                var situacaoProjetoRetorno = contexto.SituacaoProjeto.FirstOrDefault
                (
                    x => x.Id == id
                );

                SituacaoProjetoResponse retorno = new SituacaoProjetoResponse()
                {
                    IdSituacaoProjeto = situacaoProjetoRetorno.Id,
                    Nome = situacaoProjetoRetorno.Nome
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
                var situacaoProjeto = contexto.SituacaoProjeto.FirstOrDefault(
                    x => x.Id == id);

                if (situacaoProjeto != null)
                {
                    contexto.SituacaoProjeto.Remove(situacaoProjeto);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "SituacaoProjeto excluída com sucesso!");
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message
                    .FirstOrDefault());
            }
        }

    }
}