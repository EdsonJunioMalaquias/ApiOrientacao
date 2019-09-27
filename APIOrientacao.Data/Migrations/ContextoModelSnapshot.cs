﻿// <auto-generated />
using System;
using APIOrientacao.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace APIOrientacao.Data.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("APIOrientacao.Data.Entidades.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("IdPessoa");

                    b.Property<int>("IdCurso")
                        .HasColumnName("IdCurso");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnName("Matricula")
                        .HasMaxLength(8);

                    b.Property<bool>("RegistroAtivo")
                        .HasColumnName("RegistroAtivo");

                    b.HasKey("Id")
                        .HasName("IdPessoaAluno");

                    b.HasIndex("IdCurso");

                    b.ToTable("Aluno");
                });

            modelBuilder.Entity("APIOrientacao.Data.Entidades.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdCurso")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Nome")
                        .HasMaxLength(150);

                    b.HasKey("Id")
                        .HasName("IdCurso");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("APIOrientacao.Data.Entidades.Orientacao", b =>
                {
                    b.Property<int>("ProfessorPessoaId")
                        .HasColumnName("ProfessorPessoaId");

                    b.Property<int>("ProjetoId")
                        .HasColumnName("ProjetoId");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnName("DataRegistro");

                    b.Property<int>("TipoOrientacaoId")
                        .HasColumnName("IdTipoOrientacao");

                    b.HasKey("ProfessorPessoaId", "ProjetoId");

                    b.HasIndex("ProjetoId");

                    b.HasIndex("TipoOrientacaoId");

                    b.ToTable("Orientacao");
                });

            modelBuilder.Entity("APIOrientacao.Data.Entidades.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdPessoa")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Nome")
                        .HasMaxLength(300);

                    b.HasKey("Id")
                        .HasName("IdPessoa");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("APIOrientacao.Data.Entidades.Professor", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("IdPessoa");

                    b.Property<bool>("RegistroAtivo")
                        .HasColumnName("RegistroAtivo");

                    b.HasKey("Id")
                        .HasName("IdPessoaProfessor");

                    b.ToTable("Professor");
                });

            modelBuilder.Entity("APIOrientacao.Data.Entidades.Projeto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ProjetoId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Encerrado")
                        .HasColumnName("Encerrado");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Nome")
                        .HasMaxLength(200);

                    b.Property<float>("Nota")
                        .HasColumnName("Nota");

                    b.Property<int>("PessoaAlunoId")
                        .HasColumnName("PessoaAlunoId");

                    b.HasKey("Id")
                        .HasName("ProjetoId");

                    b.HasIndex("PessoaAlunoId");

                    b.ToTable("Projeto");
                });

            modelBuilder.Entity("APIOrientacao.Data.Entidades.Situacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SituacaoId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("Descricao");

                    b.HasKey("Id")
                        .HasName("SituacaoId");

                    b.ToTable("Situacao");
                });

            modelBuilder.Entity("APIOrientacao.Data.Entidades.SituacaoProjeto", b =>
                {
                    b.Property<int>("ProjetoId");

                    b.Property<int>("SituacaoId");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnName("DataRegistro");

                    b.HasKey("ProjetoId", "SituacaoId");

                    b.HasIndex("SituacaoId");

                    b.ToTable("SituacaoProjeto");
                });

            modelBuilder.Entity("APIOrientacao.Data.Entidades.TipoOrientacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TipoOrientacaoId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("Descricao");

                    b.HasKey("Id")
                        .HasName("TipoOrientacaoId");

                    b.ToTable("TipoOrientacao");
                });

            modelBuilder.Entity("APIOrientacao.Data.Entidades.Aluno", b =>
                {
                    b.HasOne("APIOrientacao.Data.Entidades.Pessoa", "Pessoa")
                        .WithOne("Aluno")
                        .HasForeignKey("APIOrientacao.Data.Entidades.Aluno", "Id")
                        .HasConstraintName("PFK_PessoaAluno")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("APIOrientacao.Data.Entidades.Curso", "Curso")
                        .WithMany("Alunos")
                        .HasForeignKey("IdCurso")
                        .HasConstraintName("FK_CursoAluno")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("APIOrientacao.Data.Entidades.Orientacao", b =>
                {
                    b.HasOne("APIOrientacao.Data.Entidades.Professor", "Professor")
                        .WithMany("Orientacoes")
                        .HasForeignKey("ProfessorPessoaId")
                        .HasConstraintName("PFK_ProfessorPessoaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("APIOrientacao.Data.Entidades.Projeto", "Projeto")
                        .WithMany("Orientacoes")
                        .HasForeignKey("ProjetoId")
                        .HasConstraintName("PFK_ProjetoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("APIOrientacao.Data.Entidades.TipoOrientacao", "TipoOrientacao")
                        .WithMany("Orientacoes")
                        .HasForeignKey("TipoOrientacaoId")
                        .HasConstraintName("FK_TipoOrientacaoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("APIOrientacao.Data.Entidades.Professor", b =>
                {
                    b.HasOne("APIOrientacao.Data.Entidades.Pessoa", "Pessoa")
                        .WithOne("Professor")
                        .HasForeignKey("APIOrientacao.Data.Entidades.Professor", "Id")
                        .HasConstraintName("PFK_PessoaProfessor")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("APIOrientacao.Data.Entidades.Projeto", b =>
                {
                    b.HasOne("APIOrientacao.Data.Entidades.Aluno", "Aluno")
                        .WithMany("Projetos")
                        .HasForeignKey("PessoaAlunoId")
                        .HasConstraintName("FK_PessoaAlunoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("APIOrientacao.Data.Entidades.SituacaoProjeto", b =>
                {
                    b.HasOne("APIOrientacao.Data.Entidades.Projeto", "Projeto")
                        .WithMany("SituacaoProjetos")
                        .HasForeignKey("ProjetoId")
                        .HasConstraintName("PFK_ProjetoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("APIOrientacao.Data.Entidades.Situacao", "Situacao")
                        .WithMany("SituacaoProjetos")
                        .HasForeignKey("SituacaoId")
                        .HasConstraintName("PFK_SituacaoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
