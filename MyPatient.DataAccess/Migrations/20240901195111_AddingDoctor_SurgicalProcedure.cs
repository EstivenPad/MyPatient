using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPatient.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddingDoctor_SurgicalProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SurgicalProcedures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Diagnostic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Procedure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurgicalProcedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurgicalProcedures_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Doctor_SurgicalProcedure",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    SurgicalProdecureId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor_SurgicalProcedure", x => new { x.DoctorId, x.SurgicalProdecureId });
                    table.ForeignKey(
                        name: "FK_Doctor_SurgicalProcedure_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctor_SurgicalProcedure_SurgicalProcedures_SurgicalProdecureId",
                        column: x => x.SurgicalProdecureId,
                        principalTable: "SurgicalProcedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_SurgicalProcedure_SurgicalProdecureId",
                table: "Doctor_SurgicalProcedure",
                column: "SurgicalProdecureId");

            migrationBuilder.CreateIndex(
                name: "IX_SurgicalProcedures_PatientId",
                table: "SurgicalProcedures",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor_SurgicalProcedure");

            migrationBuilder.DropTable(
                name: "SurgicalProcedures");
        }
    }
}
