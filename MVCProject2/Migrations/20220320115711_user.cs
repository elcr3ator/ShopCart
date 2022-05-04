using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject2.Migrations
{
    public partial class user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Provider = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NameIdentifier = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Roles = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "UserId", "Email", "Firstname", "Lastname", "Mobile", "NameIdentifier", "Password", "Provider", "Roles", "Username" },
                values: new object[] { 1, "bob@admonex.com", "Bob", "Tester", "800-555-1212", "my main account", "pizza", "Cookies", "Admin", "bob" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
