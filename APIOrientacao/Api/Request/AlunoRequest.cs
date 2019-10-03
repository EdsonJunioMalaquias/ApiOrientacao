using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class AlunoRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public int IdAluno { get; set; }
        public string Matricula { get; set; }
        public bool RegistroAtivo { get; set; }
        public int IdCurso { get; set; }
    }
}
