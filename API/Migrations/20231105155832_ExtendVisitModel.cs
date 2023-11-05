using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ExtendVisitModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClinicId",
                table: "Visits",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorAdmissionId",
                table: "Visits",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ClinicId",
                table: "Visits",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_DoctorAdmissionId",
                table: "Visits",
                column: "DoctorAdmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Clinics_ClinicId",
                table: "Visits",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_DoctorAdmissionConditions_DoctorAdmissionId",
                table: "Visits",
                column: "DoctorAdmissionId",
                principalTable: "DoctorAdmissionConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Clinics_ClinicId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_DoctorAdmissionConditions_DoctorAdmissionId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_ClinicId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_DoctorAdmissionId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "DoctorAdmissionId",
                table: "Visits");
        }
    }
}
