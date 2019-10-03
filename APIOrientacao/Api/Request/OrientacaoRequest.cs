using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class OrientacaoRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public int ProjetoId { get; set; }
        public int ProfessorPessoaId { get; set; }
        public int TipoOrientacaoId { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
