﻿using System;
using System.Collections.Generic;
using System.Text;

namespace APIOrientacao.Data.Entidades
{
    public class Aluno : EntityBase
    {
        public string Matricula { get; set; }
        public bool RegistroAtivo { get; set; }
        public int IdCurso { get; set; }
        public Curso Curso { get; set; }
        public Pessoa Pessoa { get; set; }

        public ICollection<Projeto> Projetos{get;set;}
        public Aluno()
        {
            Projetos = new HashSet<Projeto>();
        }

        //Diferença entre Collection, List, Enumerable
    }
}
