using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdontoFast.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "c_op_dentista",
                columns: table => new
                {
                    ID_DENTISTA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_DENTISTA = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    SENHA_DENTISTA = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    ESPECIALIDADE = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    CRO = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_op_dentista", x => x.ID_DENTISTA);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "c_op_dentista");
        }
    }
}
