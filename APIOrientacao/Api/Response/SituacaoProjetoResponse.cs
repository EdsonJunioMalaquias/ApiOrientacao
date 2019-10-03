using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Response
{
    public class SituacaoProjetoResponse
    {
        public DateTime DataRegistro { get; set; }
        public int SituacaoId { get; set; }
        public int ProjetoId { get; set; }
    }
}
