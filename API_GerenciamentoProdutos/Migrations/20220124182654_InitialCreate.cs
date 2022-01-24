using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_GerenciamentoProdutos.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cartoes",
                columns: table => new
                {
                    numero_cartao = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    titular = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    data_expiracao = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    bandeira = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    cvv = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartoes", x => x.numero_cartao);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    produto_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    valor_unitario = table.Column<decimal>(type: "smallmoney", nullable: false),
                    qtde_estoque = table.Column<int>(type: "int", nullable: false),
                    data_ultima_compra = table.Column<DateTime>(type: "datetime2", nullable: true),
                    valor_ultima_compra = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.produto_id);
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    pagamento_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero_cartao = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    valor = table.Column<decimal>(type: "money", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.pagamento_id);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Cartoes_numero_cartao",
                        column: x => x.numero_cartao,
                        principalTable: "Cartoes",
                        principalColumn: "numero_cartao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    compra_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    produto_id = table.Column<int>(type: "int", nullable: false),
                    numero_cartao = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    qtde_comprada = table.Column<int>(type: "int", nullable: false),
                    data_compra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.compra_id);
                    table.ForeignKey(
                        name: "FK_Compras_Cartoes_numero_cartao",
                        column: x => x.numero_cartao,
                        principalTable: "Cartoes",
                        principalColumn: "numero_cartao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compras_Produtos_produto_id",
                        column: x => x.produto_id,
                        principalTable: "Produtos",
                        principalColumn: "produto_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_numero_cartao",
                table: "Compras",
                column: "numero_cartao");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_produto_id",
                table: "Compras",
                column: "produto_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_numero_cartao",
                table: "Pagamentos",
                column: "numero_cartao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Cartoes");
        }
    }
}
