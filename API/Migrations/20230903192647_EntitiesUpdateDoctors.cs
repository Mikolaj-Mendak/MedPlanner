using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesUpdateDoctors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctor_User_DoctorId",
                table: "ClinicDoctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_User_ClinicOwnerId",
                table: "Clinics");

            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_User_DoctorId",
                table: "Clinics");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorAdmissionConditions_User_DoctorId",
                table: "DoctorAdmissionConditions");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Photos_PhotoId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_User_DoctorId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_User_PatientId",
                table: "Visits");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_User_PhotoId",
                table: "Users",
                newName: "IX_Users_PhotoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctor_Users_DoctorId",
                table: "ClinicDoctor",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_Users_ClinicOwnerId",
                table: "Clinics",
                column: "ClinicOwnerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_Users_DoctorId",
                table: "Clinics",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorAdmissionConditions_Users_DoctorId",
                table: "DoctorAdmissionConditions",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Photos_PhotoId",
                table: "Users",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Users_DoctorId",
                table: "Visits",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Users_PatientId",
                table: "Visits",
                column: "PatientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctor_Users_DoctorId",
                table: "ClinicDoctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_Users_ClinicOwnerId",
                table: "Clinics");

            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_Users_DoctorId",
                table: "Clinics");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorAdmissionConditions_Users_DoctorId",
                table: "DoctorAdmissionConditions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Photos_PhotoId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Users_DoctorId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Users_PatientId",
                table: "Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PhotoId",
                table: "User",
                newName: "IX_User_PhotoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitId = table.Column<Guid>(type: "uuid", nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_VisitId",
                table: "Documents",
                column: "VisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctor_User_DoctorId",
                table: "ClinicDoctor",
                column: "DoctorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_User_ClinicOwnerId",
                table: "Clinics",
                column: "ClinicOwnerId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_User_DoctorId",
                table: "Clinics",
                column: "DoctorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorAdmissionConditions_User_DoctorId",
                table: "DoctorAdmissionConditions",
                column: "DoctorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Photos_PhotoId",
                table: "User",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_User_DoctorId",
                table: "Visits",
                column: "DoctorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_User_PatientId",
                table: "Visits",
                column: "PatientId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
