using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Odonto.API.Migrations
{
    /// <inheritdoc />
    public partial class alteracaodataconsulta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraConsulta",
                table: "Consultas",
                type: "Time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraConsulta",
                table: "Consultas");
        }
    }
}
