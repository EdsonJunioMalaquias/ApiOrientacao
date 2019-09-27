using System.Collections.Generic;

namespace APIOrientacao.Data.Entidades
{
    public class Projeto: EntityBase
    {
        public string Nome { get; set; }
        public bool Encerrado { get; set; }
        public int PessoaAlunoId { get; set; }
        public float Nota { get; set; }
        public Aluno Aluno { get; set; }
        public ICollection<SituacaoProjeto> SituacaoProjetos { get; set; }
        public ICollection<Orientacao> Orientacoes  { get; set; }
        public Projeto()
        {
            Orientacoes = new HashSet<Orientacao>();
            SituacaoProjetos = new HashSet<SituacaoProjeto>();
            
        }

    }

}