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
    public class AlunoController : Controller
    {
        private readonly Contexto contexto;

        public AlunoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AlunoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            AlunoRequest alunoRequest)
        {

            var aluno = new Aluno
            {
                Nome = alunoRequest.Nome
            };

            contexto.Aluno.Add(aluno);
            contexto.SaveChanges();

            var alunoRetorno = contexto.Aluno.Where
                (x => x.Id == aluno.Id)
                .FirstOrDefault();

            AlunoResponse response = new AlunoResponse();

            if (alunoRetorno != null)
            {
                response.IdAluno = alunoRetorno.Id;
                response.Nome = alunoRetorno.Nome;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idAluno}")]
        [ProducesResponseType(typeof(AlunoResponse), 200)]
        public IActionResult Get(int idAluno)
        {
            var aluno = contexto.Aluno.FirstOrDefault(
                x => x.Id == idAluno);

            return StatusCode(aluno == null
                ? 404 :
                200, new AlunoResponse
                {
                    IdAluno = aluno == null ? -1 : aluno.Id,
                    Nome = aluno == null ? "Aluno não encontrada"
                    : aluno.Nome
                });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AlunoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] AlunoRequest alunoRequest)
        {
            try
            {
                var aluno = contexto.Aluno.Where(x => x.Id == id)
                        .FirstOrDefault();

                if (aluno != null)
                {
                    aluno.Nome = alunoRequest.Nome;
                }

                contexto.Entry(aluno).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

                var alunoRetorno = contexto.Aluno.FirstOrDefault
                (
                    x => x.Id == id
                );

                AlunoResponse retorno = new AlunoResponse()
                {
                    IdAluno = alunoRetorno.Id,
                    Nome = alunoRetorno.Nome
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
                var aluno = contexto.Aluno.FirstOrDefault(
                    x => x.Id == id);

                if (aluno != null)
                {
                    contexto.Aluno.Remove(aluno);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Aluno excluída com sucesso!");
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message
                    .FirstOrDefault());
            }
        }

    }
}