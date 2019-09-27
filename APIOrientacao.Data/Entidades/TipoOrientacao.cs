using System.Collections.Generic;

namespace APIOrientacao.Data.Entidades
{
    public class TipoOrientacao: EntityBase
    {
        public string Descricao { get; set; }
        public ICollection<Orientacao> Orientacoes { get; set; }
        public TipoOrientacao()
        {
            Orientacoes = new HashSet<Orientacao>();
        }
    }
}