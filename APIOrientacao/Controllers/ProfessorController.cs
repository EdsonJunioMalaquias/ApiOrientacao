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
    public class ProfessorController : Controller
    {
        private readonly Contexto contexto;

        public ProfessorController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProfessorResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            ProfessorRequest professorRequest)
        {

            var professor = new Professor
            {
                Nome = professorRequest.Nome
            };

            contexto.Professor.Add(professor);
            contexto.SaveChanges();

            var professorRetorno = contexto.Professor.Where
                (x => x.Id == professor.Id)
                .FirstOrDefault();

            ProfessorResponse response = new ProfessorResponse();

            if (professorRetorno != null)
            {
                response.IdProfessor = professorRetorno.Id;
                response.Nome = professorRetorno.Nome;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idProfessor}")]
        [ProducesResponseType(typeof(ProfessorResponse), 200)]
        public IActionResult Get(int idProfessor)
        {
            var professor = contexto.Professor.FirstOrDefault(
                x => x.Id == idProfessor);

            return StatusCode(professor == null
                ? 404 :
                200, new ProfessorResponse
                {
                    IdProfessor = professor == null ? -1 : professor.Id,
                    Nome = professor == null ? "Professor não encontrada"
                    : professor.Nome
                });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProfessorResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] ProfessorRequest professorRequest)
        {
            try
            {
                var professor = contexto.Professor.Where(x => x.Id == id)
                        .FirstOrDefault();

                if (professor != null)
                {
                    professor.Nome = professorRequest.Nome;
                }

                contexto.Entry(professor).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

                var professorRetorno = contexto.Professor.FirstOrDefault
                (
                    x => x.Id == id
                );

                ProfessorResponse retorno = new ProfessorResponse()
                {
                    IdProfessor = professorRetorno.Id,
                    Nome = professorRetorno.Nome
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
                var professor = contexto.Professor.FirstOrDefault(
                    x => x.Id == id);

                if (professor != null)
                {
                    contexto.Professor.Remove(professor);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Professor excluída com sucesso!");
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message
                    .FirstOrDefault());
            }
        }

    }
}