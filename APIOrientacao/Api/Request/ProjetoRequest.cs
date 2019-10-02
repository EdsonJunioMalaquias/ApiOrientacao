using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class ProjetoRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }
        public bool Encerrado { get; set; }
        public int PessoaAlunoId { get; set; }
        public float Nota { get; set; }

    }
}
