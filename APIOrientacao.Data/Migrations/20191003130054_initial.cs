using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIOrientacao.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Curso",
                schema: "dbo",
                columns: table => new
                {
                    IdCurso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdCurso", x => x.IdCurso);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                schema: "dbo",
                columns: table => new
                {
                    IdPessoa = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 300, nullable: false),
                    Cpf = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdPessoa", x => x.IdPessoa);
                });

            migrationBuilder.CreateTable(
                name: "Situacao",
                schema: "dbo",
                columns: table => new
                {
                    SituacaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SituacaoId", x => x.SituacaoId);
                });

            migrationBuilder.CreateTable(
                name: "TipoOrientacao",
                schema: "dbo",
                columns: table => new
                {
                    TipoOrientacaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TipoOrientacaoId", x => x.TipoOrientacaoId);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                schema: "dbo",
                columns: table => new
                {
                    IdPessoa = table.Column<int>(nullable: false),
                    Matricula = table.Column<string>(maxLength: 8, nullable: false),
                    RegistroAtivo = table.Column<bool>(nullable: false),
                    IdCurso = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdPessoaAluno", x => x.IdPessoa);
                    table.ForeignKey(
                        name: "PFK_PessoaAluno",
                        column: x => x.IdPessoa,
                        principalSchema: "dbo",
                        principalTable: "Pessoa",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CursoAluno",
                        column: x => x.IdCurso,
                        principalSchema: "dbo",
                        principalTable: "Curso",
                        principalColumn: "IdCurso",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                schema: "dbo",
                columns: table => new
                {
                    IdPessoa = table.Column<int>(nullable: false),
                    RegistroAtivo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdPessoaProfessor", x => x.IdPessoa);
                    table.ForeignKey(
                        name: "PFK_PessoaProfessor",
                        column: x => x.IdPessoa,
                        principalSchema: "dbo",
                        principalTable: "Pessoa",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                schema: "dbo",
                columns: table => new
                {
                    ProjetoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Encerrado = table.Column<bool>(nullable: false),
                    PessoaAlunoId = table.Column<int>(nullable: false),
                    Nota = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProjetoId", x => x.ProjetoId);
                    table.ForeignKey(
                        name: "FK_PessoaAlunoId",
                        column: x => x.PessoaAlunoId,
                        principalSchema: "dbo",
                        principalTable: "Aluno",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orientacao",
                schema: "dbo",
                columns: table => new
                {
                    ProjetoId = table.Column<int>(nullable: false),
                    ProfessorPessoaId = table.Column<int>(nullable: false),
                    IdTipoOrientacao = table.Column<int>(nullable: false),
                    DataRegistro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orientacao", x => new { x.ProfessorPessoaId, x.ProjetoId });
                    table.ForeignKey(
                        name: "PFK_ProfessorPessoaId",
                        column: x => x.ProfessorPessoaId,
                        principalSchema: "dbo",
                        principalTable: "Professor",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "PFK_ProjetoIdOrientacao",
                        column: x => x.ProjetoId,
                        principalSchema: "dbo",
                        principalTable: "Projeto",
                        principalColumn: "ProjetoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TipoOrientacaoId",
                        column: x => x.IdTipoOrientacao,
                        principalSchema: "dbo",
                        principalTable: "TipoOrientacao",
                        principalColumn: "TipoOrientacaoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SituacaoProjeto",
                schema: "dbo",
                columns: table => new
                {
                    SituacaoId = table.Column<int>(nullable: false),
                    ProjetoId = table.Column<int>(nullable: false),
                    DataRegistro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SituacaoProjeto", x => new { x.ProjetoId, x.SituacaoId });
                    table.ForeignKey(
                        name: "PFK_ProjetoIdSituacaoProjeto",
                        column: x => x.ProjetoId,
                        principalSchema: "dbo",
                        principalTable: "Projeto",
                        principalColumn: "ProjetoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "PFK_SituacaoId",
                        column: x => x.SituacaoId,
                        principalSchema: "dbo",
                        principalTable: "Situacao",
                        principalColumn: "SituacaoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_IdCurso",
                schema: "dbo",
                table: "Aluno",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_Orientacao_ProjetoId",
                schema: "dbo",
                table: "Orientacao",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Orientacao_IdTipoOrientacao",
                schema: "dbo",
                table: "Orientacao",
                column: "IdTipoOrientacao");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_PessoaAlunoId",
                schema: "dbo",
                table: "Projeto",
                column: "PessoaAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_SituacaoProjeto_SituacaoId",
                schema: "dbo",
                table: "SituacaoProjeto",
                column: "SituacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orientacao",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SituacaoProjeto",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Professor",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TipoOrientacao",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Projeto",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Situacao",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Aluno",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Pessoa",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Curso",
                schema: "dbo");
        }
    }
}
