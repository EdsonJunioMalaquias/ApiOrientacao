using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class SituacaoProjetoRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public int SituacaoId { get; set; }
        public int ProjetoId { get; set; }
    }
}
