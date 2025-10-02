using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWorldModel.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarMakes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    make = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    origin = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarMakes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CarModels",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    make_id = table.Column<int>(type: "int", nullable: false),
                    model = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    mpg = table.Column<double>(type: "float", nullable: false),
                    cylinders = table.Column<int>(type: "int", nullable: false),
                    displacement = table.Column<double>(type: "float", nullable: false),
                    horsepower = table.Column<int>(type: "int", nullable: false),
                    weight = table.Column<int>(type: "int", nullable: false),
                    acceleration = table.Column<double>(type: "float", nullable: false),
                    model_year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModels", x => x.id);
                    table.ForeignKey(
                        name: "FK_CarModels_CarMakes",
                        column: x => x.make_id,
                        principalTable: "CarMakes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarModels_make_id",
                table: "CarModels",
                column: "make_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarModels");

            migrationBuilder.DropTable(
                name: "CarMakes");
        }
    }
}
