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
                ProjetoId = orientacaoRequest.ProjetoId,
                ProfessorPessoaId = orientacaoRequest.ProfessorPessoaId,
                DataRegistro = orientacaoRequest.DataRegistro,
                TipoOrientacaoId = orientacaoRequest.TipoOrientacaoId

                
            };

            contexto.Orientacao.Add(orientacao);
            contexto.SaveChanges();

            var orientacaoRetorno = contexto.Orientacao.Where
                (x => x.ProfessorPessoaId == orientacao.ProfessorPessoaId && x.ProjetoId == orientacao.ProjetoId)
                .FirstOrDefault();

            OrientacaoResponse response = new OrientacaoResponse();

            if (orientacaoRetorno != null)
            {
                response.ProfessorPessoaId = orientacaoRetorno.ProfessorPessoaId;
                response.ProjetoId = orientacaoRetorno.ProjetoId;
                response.TipoOrientacaoId = orientacaoRetorno.TipoOrientacaoId;
                response.DataRegistro = orientacaoRetorno.DataRegistro;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{ProfessorPessoaId}-{ProjetoId}")]
        [ProducesResponseType(typeof(OrientacaoResponse), 200)]
        public IActionResult Get(int ProfessorPessoaId, int ProjetoId)
        {
            var orientacao = contexto.Orientacao.FirstOrDefault(
                x => x.ProfessorPessoaId == ProfessorPessoaId && x.ProjetoId == ProjetoId);

            return StatusCode(orientacao == null
                ? 404 
                : 200, new OrientacaoResponse
                        {
                            ProfessorPessoaId = orientacao == null 
                                ? -1 
                                : orientacao.ProfessorPessoaId,
                            ProjetoId = orientacao == null 
                                ? -1 
                                : orientacao.ProjetoId,
                            TipoOrientacaoId = orientacao == null 
                                ? -1 
                                : orientacao.TipoOrientacaoId,
                            DataRegistro = orientacao == null 
                                ? new DateTime(0,0,0)
                                : orientacao.DataRegistro
                        });
        }

        [HttpPut("{ProfessorPessoaId}-{ProjetoId}")]
        [ProducesResponseType(typeof(OrientacaoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int ProfessorPessoaId, int ProjetoId, [FromBody] OrientacaoRequest orientacaoRequest)
        {
            try
            {
                var orientacao = contexto.Orientacao.Where(x => x.ProfessorPessoaId == ProfessorPessoaId && x.ProjetoId == ProjetoId)
                        .FirstOrDefault();

                if (orientacao != null)
                {
                    orientacao.DataRegistro = orientacaoRequest.DataRegistro;
                }

                contexto.Entry(orientacao).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

                var orientacaoRetorno = contexto.Orientacao.FirstOrDefault
                (
                    
                    x => x.ProfessorPessoaId == ProfessorPessoaId && x.ProjetoId == ProjetoId
                );

                OrientacaoResponse retorno = new OrientacaoResponse()
                {
                    ProfessorPessoaId = orientacaoRetorno.ProfessorPessoaId,
                    ProjetoId = orientacaoRetorno.ProjetoId,
                    TipoOrientacaoId = orientacaoRetorno.TipoOrientacaoId,
                    DataRegistro = orientacaoRetorno.DataRegistro
                };

                return StatusCode(200, retorno);
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.
                    Message.FirstOrDefault());
            }

        }

        [HttpDelete("{ProfessorPessoaId}-{ProjetoId}")]
        [ProducesResponseType(400)]
        public IActionResult Delete(int ProfessorPessoaId, int ProjetoId)
        {
            try
            {
                var orientacao = contexto.Orientacao.FirstOrDefault(
                    x => x.ProfessorPessoaId == ProfessorPessoaId && x.ProjetoId == ProjetoId);
                if (orientacao != null)
                {
                    contexto.Orientacao.Remove(orientacao);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Orientacao excluída com sucesso!");
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

    }
}