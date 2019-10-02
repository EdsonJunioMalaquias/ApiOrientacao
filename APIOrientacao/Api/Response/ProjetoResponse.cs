using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Response
{
    public class ProjetoResponse
    {
        public int IdProjeto { get; set; }
        public string Nome { get; set; }
        public bool Encerrado { get; set; }
        public int PessoaAlunoId { get; set; }
        public float Nota { get; set; }    }
}
