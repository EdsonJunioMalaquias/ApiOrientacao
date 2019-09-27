using System;

namespace APIOrientacao.Data.Entidades
{
    public class SituacaoProjeto
    {
        public int SituacaoId { get; set; }
        public int ProjetoId { get; set; }
        public DateTime DataRegistro { get; set; }
        public Projeto Projeto { get; set; }
        public Situacao Situacao { get; set; }
        
    }
}