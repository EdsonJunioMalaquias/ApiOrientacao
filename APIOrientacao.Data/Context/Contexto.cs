using APIOrientacao.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIOrientacao.Data.Context
{
    public class Contexto : DbContext 
    {
        public Contexto()
        { }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        { }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Orientacao> Orientacao {get;set;}
        public DbSet<TipoOrientacao> TipoOrientacao { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<SituacaoProjeto> SituacaoProjeto { get; set; }
        public DbSet<Situacao> Situacao { get; set; }


        //public void PessoaMB(ModelBuilder modelBuilder)
        //{
           
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            foreach (var relacionamento in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relacionamento.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.ForSqlServerUseIdentityColumns();
            modelBuilder.HasDefaultSchema("dbo");

            //PessoaMB(modelBuilder);

            modelBuilder.Entity<Pessoa>(e =>
            {
                e.ToTable("Pessoa");
                e.HasKey(c => c.Id).HasName("IdPessoa");
                e.Property(c => c.Id).HasColumnName("IdPessoa")
                .ValueGeneratedOnAdd();

                e.Property(c => c.Nome).HasColumnName("Nome")
                .IsRequired()
                .HasMaxLength(300);
            });

            modelBuilder.Entity<Curso>(e =>
            {
                e.ToTable("Curso");
                e.HasKey(c => c.Id).HasName("IdCurso");
                e.Property(c => c.Id).HasColumnName("IdCurso")
                    .ValueGeneratedOnAdd();

                e.Property(c => c.Nome).HasColumnName("Nome")
                    .IsRequired()
                    .HasMaxLength(150);

            });

            modelBuilder.Entity<Aluno>(e =>
            {
                e.ToTable("Aluno");
                e.HasKey(c => c.Id).HasName("IdPessoaAluno");
                e.Property(c => c.Id).HasColumnName("IdPessoa")
                    .IsRequired();
                e.Property(c => c.IdCurso).HasColumnName("IdCurso")
                    .IsRequired();

                e.Property(c => c.Matricula)
                .HasColumnName("Matricula")
                .IsRequired()
                .HasMaxLength(8);

                e.Property(c => c.RegistroAtivo)
                .HasColumnName("RegistroAtivo")
                .IsRequired();

                e.HasOne(d => d.Pessoa)
                    .WithOne(p => p.Aluno)
                    .HasForeignKey<Aluno>(d => d.Id)
                    .HasConstraintName("PFK_PessoaAluno");

                e.HasOne(d => d.Curso)
                    .WithMany(p => p.Alunos)
                    .HasForeignKey(d => d.IdCurso)
                    .HasConstraintName("FK_CursoAluno");
            });
           
            modelBuilder.Entity<Professor>(e =>
            {
                e.ToTable("Professor");
                e.HasKey(c => c.Id).HasName("IdPessoaProfessor");
                e.Property(c => c.Id).HasColumnName("IdPessoa")
                    .IsRequired();

                e.Property(c => c.RegistroAtivo)
                .HasColumnName("RegistroAtivo")
                .IsRequired();

                e.HasOne(d => d.Pessoa)
                    .WithOne(p => p.Professor)
                    .HasForeignKey<Professor>(d => d.Id)
                    .HasConstraintName("PFK_PessoaProfessor");
 
            });

            modelBuilder.Entity<TipoOrientacao>(e =>
            {
                e.ToTable("TipoOrientacao");
                e.HasKey(c => c.Id).HasName("TipoOrientacaoId");
                e.Property(c => c.Id).HasColumnName("TipoOrientacaoId")
                .ValueGeneratedOnAdd();
                e.Property(c => c.Descricao)
                .HasColumnName("Descricao")
                .IsRequired();
            });

            modelBuilder.Entity<Situacao>(e =>
            {
                e.ToTable("Situacao");
                e.HasKey(c => c.Id).HasName("SituacaoId");
                e.Property(c => c.Id).HasColumnName("SituacaoId")
                .ValueGeneratedOnAdd();
                e.Property(c => c.Descricao)
                .HasColumnName("Descricao")
                .IsRequired();
            });

            modelBuilder.Entity<Projeto>(e =>
            {
                e.ToTable("Projeto");

                e.HasKey(c => c.Id).HasName("ProjetoId");

                e.Property(c => c.Id).HasColumnName("ProjetoId")
                .ValueGeneratedOnAdd();

                e.Property(c => c.Nome)
                .HasMaxLength(200)
                .HasColumnName("Nome")
                .IsRequired();

                e.Property(c => c.Nota)
                .HasColumnName("Nota")
                .IsRequired();
                                
                e.Property(c => c.Encerrado)
                .HasColumnName("Encerrado")
                .IsRequired();

                e.Property(c => c.PessoaAlunoId)
                .HasColumnName("PessoaAlunoId")
                .IsRequired();
                
                e.HasOne(a=> a.Aluno)
                .WithMany(b=>b.Projetos)
                .HasForeignKey(a => a.PessoaAlunoId)
                .HasConstraintName("FK_PessoaAlunoId");
            });
   
            modelBuilder.Entity<SituacaoProjeto>(e =>
            {
                e.ToTable("SituacaoProjeto");
                e.HasKey(bc =>new {bc.ProjetoId,bc.SituacaoId});

                e.HasOne(a=> a.Situacao)
                .WithMany(b=>b.SituacaoProjetos)
                .HasForeignKey(a => a.SituacaoId)
                .HasConstraintName("PFK_SituacaoId");

                e.HasOne(a=> a.Projeto)
                .WithMany(b=>b.SituacaoProjetos)
                .HasForeignKey(a => a.ProjetoId)
                .HasConstraintName("PFK_ProjetoIdSituacaoProjeto");


                e.Property(c => c.DataRegistro)
                .HasColumnName("DataRegistro")
                .IsRequired();
            });
            modelBuilder.Entity<Orientacao>(e =>
            {
                e.ToTable("Orientacao");
                e.HasKey(bc => new { bc.ProfessorPessoaId, bc.ProjetoId });

                e.Property(c => c.ProfessorPessoaId)
                .HasColumnName("ProfessorPessoaId")
                .IsRequired();
                e.Property(c => c.ProjetoId)
                .HasColumnName("ProjetoId")
                .IsRequired();
                e.Property(c => c.TipoOrientacaoId)
                .HasColumnName("TipoOrientacaoId")
                .IsRequired();

                e.HasOne(a => a.Professor)
                .WithMany(b => b.Orientacoes)
                .HasForeignKey(a => a.ProfessorPessoaId)
                .HasConstraintName("PFK_ProfessorPessoaId");

                e.HasOne(a => a.Projeto)
                .WithMany(b => b.Orientacoes)
                .HasForeignKey(a => a.ProjetoId)
                .HasConstraintName("PFK_ProjetoIdOrientacao");

                e.HasOne(a => a.TipoOrientacao)
                .WithMany(b => b.Orientacoes)
                .HasForeignKey(a => a.TipoOrientacaoId)
                .HasConstraintName("FK_TipoOrientacaoId");

                e.Property(c => c.TipoOrientacaoId)
                .HasColumnName("IdTipoOrientacao")
                .IsRequired();

                e.Property(c => c.DataRegistro)
                .HasColumnName("DataRegistro")
                .IsRequired();
            });


        }
    }
}
