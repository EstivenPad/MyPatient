using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPatient.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenamingMAToDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalOrders_MAs_MAId",
                table: "MedicalOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_MAs_MAId",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MAs",
                table: "MAs");

            migrationBuilder.RenameTable(
                name: "MAs",
                newName: "Doctors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalOrders_Doctors_MAId",
                table: "MedicalOrders",
                column: "MAId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_MAId",
                table: "Patients",
                column: "MAId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalOrders_Doctors_MAId",
                table: "MedicalOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_MAId",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            migrationBuilder.RenameTable(
                name: "Doctors",
                newName: "MAs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MAs",
                table: "MAs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalOrders_MAs_MAId",
                table: "MedicalOrders",
                column: "MAId",
                principalTable: "MAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_MAs_MAId",
                table: "Patients",
                column: "MAId",
                principalTable: "MAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
