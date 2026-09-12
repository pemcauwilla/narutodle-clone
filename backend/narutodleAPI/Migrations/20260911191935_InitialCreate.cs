using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace narutodleAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ninjas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Gender = table.Column<int>(type: "INTEGER", nullable: true),
                    DebutArc = table.Column<int>(type: "INTEGER", nullable: true),
                    Affiliations = table.Column<string>(type: "TEXT", nullable: false),
                    JutsuTypes = table.Column<string>(type: "TEXT", nullable: false),
                    NatureTypes = table.Column<string>(type: "TEXT", nullable: false),
                    KekkeiGenkais = table.Column<string>(type: "TEXT", nullable: false),
                    Classifications = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ninjas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ninjas");
        }
    }
}
