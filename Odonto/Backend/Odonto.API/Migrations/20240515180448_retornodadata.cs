using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Odonto.API.Migrations
{
    /// <inheritdoc />
    public partial class retornodadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraConsulta",
                table: "Consultas");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataConsulta",
                table: "Consultas",
                type: "DateTime",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DataConsulta",
                table: "Consultas",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraConsulta",
                table: "Consultas",
                type: "Time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
