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
    public class TipoOrientacaoController : Controller
    {
        private readonly Contexto contexto;

        public TipoOrientacaoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TipoOrientacaoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            TipoOrientacaoRequest tipoOrientacaoRequest)
        {

            var tipoOrientacao = new TipoOrientacao
            {
                Nome = tipoOrientacaoRequest.Nome
            };

            contexto.TipoOrientacao.Add(tipoOrientacao);
            contexto.SaveChanges();

            var tipoOrientacaoRetorno = contexto.TipoOrientacao.Where
                (x => x.Id == tipoOrientacao.Id)
                .FirstOrDefault();

            TipoOrientacaoResponse response = new TipoOrientacaoResponse();

            if (tipoOrientacaoRetorno != null)
            {
                response.IdTipoOrientacao = tipoOrientacaoRetorno.Id;
                response.Nome = tipoOrientacaoRetorno.Nome;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idTipoOrientacao}")]
        [ProducesResponseType(typeof(TipoOrientacaoResponse), 200)]
        public IActionResult Get(int idTipoOrientacao)
        {
            var tipoOrientacao = contexto.TipoOrientacao.FirstOrDefault(
                x => x.Id == idTipoOrientacao);

            return StatusCode(tipoOrientacao == null
                ? 404 :
                200, new TipoOrientacaoResponse
                {
                    IdTipoOrientacao = tipoOrientacao == null ? -1 : tipoOrientacao.Id,
                    Nome = tipoOrientacao == null ? "TipoOrientacao não encontrada"
                    : tipoOrientacao.Nome
                });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TipoOrientacaoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] TipoOrientacaoRequest tipoOrientacaoRequest)
        {
            try
            {
                var tipoOrientacao = contexto.TipoOrientacao.Where(x => x.Id == id)
                        .FirstOrDefault();

                if (tipoOrientacao != null)
                {
                    tipoOrientacao.Nome = tipoOrientacaoRequest.Nome;
                }

                contexto.Entry(tipoOrientacao).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

                var tipoOrientacaoRetorno = contexto.TipoOrientacao.FirstOrDefault
                (
                    x => x.Id == id
                );

                TipoOrientacaoResponse retorno = new TipoOrientacaoResponse()
                {
                    IdTipoOrientacao = tipoOrientacaoRetorno.Id,
                    Nome = tipoOrientacaoRetorno.Nome
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
                var tipoOrientacao = contexto.TipoOrientacao.FirstOrDefault(
                    x => x.Id == id);

                if (tipoOrientacao != null)
                {
                    contexto.TipoOrientacao.Remove(tipoOrientacao);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "TipoOrientacao excluída com sucesso!");
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message
                    .FirstOrDefault());
            }
        }

    }
}