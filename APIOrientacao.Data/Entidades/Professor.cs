using System.Collections.Generic;

namespace APIOrientacao.Data.Entidades
{
    public class Professor : EntityBase
    {
        public bool RegistroAtivo { get; set; }
        public Pessoa Pessoa { get; set; }

        public ICollection<Orientacao> Orientacoes { get; set; }

        public Professor()
        {
            Orientacoes = new HashSet<Orientacao>();
        }
    }
}