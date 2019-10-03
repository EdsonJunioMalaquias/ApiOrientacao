using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Response
{
    public class OrientacaoResponse
    {
        public int ProjetoId { get; set; }
        public int ProfessorPessoaId { get; set; }
        public int TipoOrientacaoId { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
