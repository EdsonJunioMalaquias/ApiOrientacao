using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace APIOrientacao.Data.Entidades
{
    public class Pessoa : EntityBase
    {
        [Required(ErrorMessage = "O nome é obrigatório!")]
        public string Nome { get; set; }

        public Aluno Aluno { get; set; }
        public Professor Professor { get; set; }
        
    
    }
}
