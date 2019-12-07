using Microsoft.EntityFrameworkCore.Migrations;

namespace SaneServer.Server.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scanners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    FriendlyName = table.Column<string>(maxLength: 100, nullable: true),
                    Vendor = table.Column<string>(maxLength: 100, nullable: true),
                    Model = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scanners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(maxLength: 120, nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: true),
                    PasswordHash = table.Column<string>(maxLength: 1024, nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Role", "UserName" },
                values: new object[] { 1, null, "AQAAAAEAACcQAAAAEJgBTWevbuyMnLD9KwP3aaFpwT0NS1SkTZVIEGsmsjPhEMcUWQjAc/5yGE0k3OWiHA==", 0, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scanners");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
