using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class ProfessorRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public bool RegistroAtivo { get; set; }
        public int IdProfessor { get; set; }
    }
}
