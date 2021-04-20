using Microsoft.EntityFrameworkCore.Migrations;

namespace CarSell.DAL.Migrations
{
    public partial class initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarName = table.Column<string>(maxLength: 1000, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Region = table.Column<string>(maxLength: 100, nullable: true),
                    YearOfRelease = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    ManufacturerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Tayota" });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "UzAM" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CarName", "Image", "ManufacturerId", "Price", "Region", "YearOfRelease" },
                values: new object[] { 1, "Tayota Land Crusier Prado", "https://kluz-photos.kcdn.online/webp/27/27ea59a8-982f-4951-9598-de7b0dd2c86c/1-160x120.jpg", 1, 24000.0, "Tashkent", 2007 });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CarName", "Image", "ManufacturerId", "Price", "Region", "YearOfRelease" },
                values: new object[] { 2, "Lacetti, 3rd position", "https://kluz-photos.kcdn.online/webp/90/9097f693-c587-49da-8baf-2d0c0b10af0c/1-160x120.jpg", 2, 11000.0, "Tashkent", 2018 });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ManufacturerId",
                table: "Cars",
                column: "ManufacturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
