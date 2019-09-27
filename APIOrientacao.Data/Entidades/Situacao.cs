using System.Collections.Generic;

namespace APIOrientacao.Data.Entidades
{
    public class Situacao: EntityBase
    {
        public string Descricao { get; set; }
        public ICollection<SituacaoProjeto> SituacaoProjetos { get; set; }
        public Situacao()
        {
            SituacaoProjetos = new HashSet<SituacaoProjeto>();
        }
    }
}