using Microsoft.EntityFrameworkCore.Migrations;
using Parkinson_API.Helpers.Roles;

#nullable disable

namespace Parkinson_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { PatientRole.Id, PatientRole.Name, PatientRole.NormalizedName, PatientRole.ConcurrencyStamp }
            );
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { AdminRole.Id, AdminRole.Name, AdminRole.NormalizedName, AdminRole.ConcurrencyStamp }
            );
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { DoctorRole.Id, DoctorRole.Name, DoctorRole.NormalizedName, DoctorRole.ConcurrencyStamp }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From [Role]");
        }
    }
}
