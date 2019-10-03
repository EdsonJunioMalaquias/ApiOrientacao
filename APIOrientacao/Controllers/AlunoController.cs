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
                Id = alunoRequest.IdAluno,
                Matricula = alunoRequest.Matricula,
                RegistroAtivo =alunoRequest.RegistroAtivo,
                IdCurso = alunoRequest.IdCurso
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
                response.Matricula = alunoRetorno.Matricula;
                response.RegistroAtivo = alunoRetorno.RegistroAtivo;
                response.IdCurso = alunoRetorno.IdCurso;
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
                    IdAluno = aluno == null 
                        ? -1 
                        : aluno.Id,
                    Matricula = aluno == null 
                        ? "Aluno não encontrada"
                        : aluno.Matricula,
                    RegistroAtivo = aluno == null 
                        ? false
                        : aluno.RegistroAtivo,
                    IdCurso = aluno == null 
                        ? -1
                        : aluno.IdCurso
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
                    aluno.Matricula = alunoRequest.Matricula;
                    aluno.IdCurso = alunoRequest.IdCurso;
                    aluno.RegistroAtivo = alunoRequest.RegistroAtivo;
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
                    Matricula = alunoRetorno.Matricula,
                    RegistroAtivo = alunoRetorno.RegistroAtivo,
                    IdCurso = alunoRetorno.IdCurso

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