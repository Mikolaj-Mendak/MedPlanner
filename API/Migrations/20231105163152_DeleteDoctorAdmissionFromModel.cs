using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDoctorAdmissionFromModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_DoctorAdmissionConditions_DoctorAdmissionId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_DoctorAdmissionId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "DoctorAdmissionId",
                table: "Visits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DoctorAdmissionId",
                table: "Visits",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Visits_DoctorAdmissionId",
                table: "Visits",
                column: "DoctorAdmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_DoctorAdmissionConditions_DoctorAdmissionId",
                table: "Visits",
                column: "DoctorAdmissionId",
                principalTable: "DoctorAdmissionConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
