using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parkinson_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientTests");

            migrationBuilder.CreateTable(
                name: "ClickTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    LeftHand = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RightHand = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClickTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClickTests_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemoryTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemoryTests_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReactionTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactionTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReactionTests_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpiralTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpiralTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpiralTests_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClickTests_PatientId",
                table: "ClickTests",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MemoryTests_PatientId",
                table: "MemoryTests",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ReactionTests_PatientId",
                table: "ReactionTests",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_SpiralTests_PatientId",
                table: "SpiralTests",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClickTests");

            migrationBuilder.DropTable(
                name: "MemoryTests");

            migrationBuilder.DropTable(
                name: "ReactionTests");

            migrationBuilder.DropTable(
                name: "SpiralTests");

            migrationBuilder.CreateTable(
                name: "PatientTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    TestedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientTests_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientTests_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientTests_PatientId",
                table: "PatientTests",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientTests_TestId",
                table: "PatientTests",
                column: "TestId");
        }
    }
}
