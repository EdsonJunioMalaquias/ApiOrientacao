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
    public class OrientacaoController : Controller
    {
        private readonly Contexto contexto;

        public OrientacaoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrientacaoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            OrientacaoRequest orientacaoRequest)
        {

            var orientacao = new Orientacao
            {
                Nome = orientacaoRequest.Nome
            };

            contexto.Orientacao.Add(orientacao);
            contexto.SaveChanges();

            var orientacaoRetorno = contexto.Orientacao.Where
                (x => x.Id == orientacao.Id)
                .FirstOrDefault();

            OrientacaoResponse response = new OrientacaoResponse();

            if (orientacaoRetorno != null)
            {
                response.IdOrientacao = orientacaoRetorno.Id;
                response.Nome = orientacaoRetorno.Nome;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idOrientacao}")]
        [ProducesResponseType(typeof(OrientacaoResponse), 200)]
        public IActionResult Get(int idOrientacao)
        {
            var orientacao = contexto.Orientacao.FirstOrDefault(
                x => x.Id == idOrientacao);

            return StatusCode(orientacao == null
                ? 404 :
                200, new OrientacaoResponse
                {
                    IdOrientacao = orientacao == null ? -1 : orientacao.Id,
                    Nome = orientacao == null ? "Orientacao não encontrada"
                    : orientacao.Nome
                });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OrientacaoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] OrientacaoRequest orientacaoRequest)
        {
            try
            {
                var orientacao = contexto.Orientacao.Where(x => x.Id == id)
                        .FirstOrDefault();

                if (orientacao != null)
                {
                    orientacao.Nome = orientacaoRequest.Nome;
                }

                contexto.Entry(orientacao).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

                var orientacaoRetorno = contexto.Orientacao.FirstOrDefault
                (
                    x => x.Id == id
                );

                OrientacaoResponse retorno = new OrientacaoResponse()
                {
                    IdOrientacao = orientacaoRetorno.Id,
                    Nome = orientacaoRetorno.Nome
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
                var orientacao = contexto.Orientacao.FirstOrDefault(
                    x => x.Id == id);

                if (orientacao != null)
                {
                    contexto.Orientacao.Remove(orientacao);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Orientacao excluída com sucesso!");
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message
                    .FirstOrDefault());
            }
        }

    }
}