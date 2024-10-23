using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPatient.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sex = table.Column<bool>(type: "bit", nullable: false),
                    Exequatur = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Record = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identification = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Sex = table.Column<bool>(type: "bit", nullable: false),
                    IsInsured = table.Column<bool>(type: "bit", nullable: false),
                    ARS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NSS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MAId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_MAId",
                        column: x => x.MAId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Diagnostic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralMeasures = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cures = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialControls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DREN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alergies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enterconsult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Labs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientId = table.Column<long>(type: "bigint", nullable: true),
                    MAId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalOrders_Doctors_MAId",
                        column: x => x.MAId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_MedicalOrders_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "MedicalOrderDetails",
                columns: table => new
                {
                    MedicalOrderDetailId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolutionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dose = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Frecuency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Via = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MedicalOrderId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalOrderDetails", x => x.MedicalOrderDetailId);
                    table.ForeignKey(
                        name: "FK_MedicalOrderDetails_MedicalOrders_MedicalOrderId",
                        column: x => x.MedicalOrderId,
                        principalTable: "MedicalOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctor_SurgicalProcedure",
                columns: table => new
                {
                    DoctorId = table.Column<long>(type: "bigint", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "SurgicalProceduresDiscoveries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurgicalProcedureId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurgicalProceduresDiscoveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurgicalProceduresDiscoveries_SurgicalProcedures_SurgicalProcedureId",
                        column: x => x.SurgicalProcedureId,
                        principalTable: "SurgicalProcedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Exequatur", "FirstName", "Identification", "LastName", "Sex", "Type" },
                values: new object[] { 1L, "1536-23", "Miguel", "402-1234567-0", "Tejada", false, 1 });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "ARS", "Age", "Identification", "IsInsured", "MAId", "NSS", "Name", "Record", "Sex", "Weight" },
                values: new object[] { 1L, "SeNaSa", 25, "402-1234567-1", true, 1L, "1234", "Guillermo Reyes", "1234", false, 145.30000000000001 });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_SurgicalProcedure_SurgicalProdecureId",
                table: "Doctor_SurgicalProcedure",
                column: "SurgicalProdecureId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalOrderDetails_MedicalOrderId",
                table: "MedicalOrderDetails",
                column: "MedicalOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalOrders_MAId",
                table: "MedicalOrders",
                column: "MAId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalOrders_PatientId",
                table: "MedicalOrders",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MAId",
                table: "Patients",
                column: "MAId");

            migrationBuilder.CreateIndex(
                name: "IX_SurgicalProcedures_PatientId",
                table: "SurgicalProcedures",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_SurgicalProceduresDiscoveries_SurgicalProcedureId",
                table: "SurgicalProceduresDiscoveries",
                column: "SurgicalProcedureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor_SurgicalProcedure");

            migrationBuilder.DropTable(
                name: "MedicalOrderDetails");

            migrationBuilder.DropTable(
                name: "SurgicalProceduresDiscoveries");

            migrationBuilder.DropTable(
                name: "MedicalOrders");

            migrationBuilder.DropTable(
                name: "SurgicalProcedures");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
