using System;

namespace APIOrientacao.Data.Entidades
{
    public class Orientacao
    {
        public int ProjetoId { get; set; }
        public Projeto Projeto { get; set; }

        public int ProfessorPessoaId { get; set; }
        public Professor Professor { get; set; }

        public int TipoOrientacaoId { get; set; }
        public TipoOrientacao TipoOrientacao { get; set; }
        public DateTime DataRegistro { get; set; }
        

    }
}