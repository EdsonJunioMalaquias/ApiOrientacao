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
                SituacaoId = situacaoProjetoRequest.SituacaoId,
                ProjetoId = situacaoProjetoRequest.ProjetoId,
                DataRegistro = situacaoProjetoRequest.DataRegistro
            };

            contexto.SituacaoProjeto.Add(situacaoProjeto);
            contexto.SaveChanges();

            var situacaoProjetoRetorno = contexto.SituacaoProjeto.Where
                (x => x.SituacaoId == situacaoProjeto.SituacaoId&&x.ProjetoId == situacaoProjeto.ProjetoId)
                .FirstOrDefault();

            SituacaoProjetoResponse response = new SituacaoProjetoResponse();

            if (situacaoProjetoRetorno != null)
            {
                response.SituacaoId = situacaoProjetoRetorno.SituacaoId;
                response.ProjetoId = situacaoProjetoRetorno.ProjetoId;
                response.DataRegistro = situacaoProjetoRetorno.DataRegistro;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{SituacaoId}-{ProjetoId}")]
        [ProducesResponseType(typeof(SituacaoProjetoResponse), 200)]
        public IActionResult Get(int SituacaoId,int ProjetoId)
        {
            var situacaoProjeto = contexto.SituacaoProjeto.FirstOrDefault(
                x => x.SituacaoId == SituacaoId && x.ProjetoId == ProjetoId);

            return StatusCode(situacaoProjeto == null
                                ? 404 
                                : 200, new SituacaoProjetoResponse
                                        {
                                            SituacaoId = (situacaoProjeto == null) 
                                                ? -1 
                                                : situacaoProjeto.SituacaoId,
                                            ProjetoId = (situacaoProjeto == null) 
                                                ? -1 
                                                : situacaoProjeto.ProjetoId,
                                            DataRegistro = situacaoProjeto == null 
                                                ? new DateTime(1,1,1)
                                                : situacaoProjeto.DataRegistro
                                        }
                                );
        }

        [HttpPut("{SituacaoId}-{ProjetoId}")]
         [ProducesResponseType(typeof(SituacaoProjetoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int SituacaoId,int ProjetoId, [FromBody] SituacaoProjetoRequest situacaoProjetoRequest)
        {
            try
            {
                var situacaoProjeto = contexto.SituacaoProjeto.Where(x => x.SituacaoId == SituacaoId  && x.ProjetoId == ProjetoId)
                        .FirstOrDefault();

                if (situacaoProjeto != null)
                {
                    situacaoProjeto.DataRegistro = situacaoProjetoRequest.DataRegistro;
                }

                contexto.Entry(situacaoProjeto).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

                var situacaoProjetoRetorno = contexto.SituacaoProjeto.FirstOrDefault
                (
                    x => x.SituacaoId == SituacaoId&& x.ProjetoId == ProjetoId
                );

                SituacaoProjetoResponse retorno = new SituacaoProjetoResponse()
                {
                    SituacaoId = situacaoProjetoRetorno.SituacaoId,
                    ProjetoId = situacaoProjetoRetorno.ProjetoId,
                    DataRegistro = situacaoProjetoRetorno.DataRegistro
                };

                return StatusCode(200, retorno);
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.
                    Message.FirstOrDefault());
            }

        }

        [HttpDelete("{SituacaoId}-{ProjetoId}")]
        [ProducesResponseType(400)]
        public IActionResult Delete(int SituacaoId, int ProjetoId)
        {
            try
            {
                var situacaoProjeto = contexto.SituacaoProjeto.FirstOrDefault(
                    x => x.SituacaoId == SituacaoId&&x.ProjetoId == ProjetoId);

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