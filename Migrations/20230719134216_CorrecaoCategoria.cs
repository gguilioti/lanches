using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanches.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DescricaoCurta",
                table: "Categorias",
                newName: "Descricao");

                migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome, Descricao) VALUES ('Normal', 'Lanche feito com ingredientes normais')");
                migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome, Descricao) VALUES ('Natural', 'Lanche feito com ingredientes integrais e naturais')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Categorias",
                newName: "DescricaoCurta");

            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}