using System;
using System.Collections.Generic;
using System.Text;

namespace APIOrientacao.Data.Entidades
{
    public class Curso : EntityBase
    {


        public string Nome { get; set; }

        public ICollection<Aluno> Alunos { get; set; }
        public Curso()
        {
            Alunos = new HashSet<Aluno>();
        }
    }
}
