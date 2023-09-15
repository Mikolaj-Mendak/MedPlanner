using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ClinicDoctorRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctor_Clinics_ClinicId",
                table: "ClinicDoctor");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctor_Users_DoctorId",
                table: "ClinicDoctor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicDoctor",
                table: "ClinicDoctor");

            migrationBuilder.RenameTable(
                name: "ClinicDoctor",
                newName: "ClinicDoctors");

            migrationBuilder.RenameIndex(
                name: "IX_ClinicDoctor_DoctorId",
                table: "ClinicDoctors",
                newName: "IX_ClinicDoctors_DoctorId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VisitDate",
                table: "Visits",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WorkHoursStart",
                table: "DoctorAdmissionConditions",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "WorkHoursEnd",
                table: "DoctorAdmissionConditions",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OfficeHoursStartDate",
                table: "Clinics",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OfficeHoursEndDate",
                table: "Clinics",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicDoctors",
                table: "ClinicDoctors",
                columns: new[] { "ClinicId", "DoctorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctors_Clinics_ClinicId",
                table: "ClinicDoctors",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctors_Users_DoctorId",
                table: "ClinicDoctors",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctors_Clinics_ClinicId",
                table: "ClinicDoctors");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctors_Users_DoctorId",
                table: "ClinicDoctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicDoctors",
                table: "ClinicDoctors");

            migrationBuilder.RenameTable(
                name: "ClinicDoctors",
                newName: "ClinicDoctor");

            migrationBuilder.RenameIndex(
                name: "IX_ClinicDoctors_DoctorId",
                table: "ClinicDoctor",
                newName: "IX_ClinicDoctor_DoctorId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VisitDate",
                table: "Visits",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WorkHoursStart",
                table: "DoctorAdmissionConditions",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "WorkHoursEnd",
                table: "DoctorAdmissionConditions",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OfficeHoursStartDate",
                table: "Clinics",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OfficeHoursEndDate",
                table: "Clinics",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicDoctor",
                table: "ClinicDoctor",
                columns: new[] { "ClinicId", "DoctorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctor_Clinics_ClinicId",
                table: "ClinicDoctor",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctor_Users_DoctorId",
                table: "ClinicDoctor",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
