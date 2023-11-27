using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrilhasBrasil.API.Migrations
{
    /// <inheritdoc />
    public partial class alimentaçãodedados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Dificuldades",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { new Guid("472f64f1-d96e-4a61-ba3b-b7899657ed57"), "Médio" },
                    { new Guid("6d729fe9-54e6-4482-a88e-59d6e6921f00"), "Fácil" },
                    { new Guid("8d0fb2f1-20b7-4f78-9e28-67acf50a41d7"), "Difícil" }
                });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "EstadoImagemURl", "Nome", "Sigla" },
                values: new object[,]
                {
                    { new Guid("50bb6ee2-9569-4a40-bd85-2ca90a16678b"), "https://media.gettyimages.com/id/560616935/pt/foto/portico-of-gramado-rio-grande-do-sul-brazil.jpg?s=2048x2048&w=gi&k=20&c=ExGu5ZKM5lWt0XsPuGQFwAQ2FCgjr7DLdqmSTI9RLRw=", "Rio Grande do Sul", "RS" },
                    { new Guid("72f01c89-0c88-4f78-b184-5735f0ea30d6"), null, "Rio Grande do Norte", "RN" },
                    { new Guid("94c09e4b-14b2-453e-9c9c-8e48ec3f9683"), null, "Mato Grosso do Sul", "MS" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dificuldades",
                keyColumn: "Id",
                keyValue: new Guid("472f64f1-d96e-4a61-ba3b-b7899657ed57"));

            migrationBuilder.DeleteData(
                table: "Dificuldades",
                keyColumn: "Id",
                keyValue: new Guid("6d729fe9-54e6-4482-a88e-59d6e6921f00"));

            migrationBuilder.DeleteData(
                table: "Dificuldades",
                keyColumn: "Id",
                keyValue: new Guid("8d0fb2f1-20b7-4f78-9e28-67acf50a41d7"));

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: new Guid("50bb6ee2-9569-4a40-bd85-2ca90a16678b"));

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: new Guid("72f01c89-0c88-4f78-b184-5735f0ea30d6"));

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: new Guid("94c09e4b-14b2-453e-9c9c-8e48ec3f9683"));
        }
    }
}
