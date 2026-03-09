using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Users.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "usuarios",
                columns: new[] { "id", "nome", "email", "senha", "perfil", "ativo", "data_criacao", "data_atualizacao" },
                values: new object[]
                {
                    new Guid("0ea5d907-6ce6-4167-b165-8aa42b023ee4"),
                    "admin",
                    "admin@fcg.com.br",
                    "51Ba401eUC++k5ajm5FYMg==./7iHSwbLGxojHXfSFJHdaaOJyIk4D8nk/yA6mfuJgXE=",
                    2,
                    true,
                    DateTime.UtcNow,
                    (DateTime?)null
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "id",
                keyValue: new Guid("0ea5d907-6ce6-4167-b165-8aa42b023ee4"));
        }
    }
}
