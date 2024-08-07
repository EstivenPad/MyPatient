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
                name: "MAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<bool>(type: "bit", nullable: false),
                    Exequatur = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAs", x => x.Id);
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
                    MAId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_MAs_MAId",
                        column: x => x.MAId,
                        principalTable: "MAs",
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
                    Alergies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enterconsult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Labs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientId = table.Column<long>(type: "bigint", nullable: true),
                    MAId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalOrders_MAs_MAId",
                        column: x => x.MAId,
                        principalTable: "MAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalOrders_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.InsertData(
                table: "MAs",
                columns: new[] { "Id", "Exequatur", "FirstName", "Identification", "LastName", "Sex" },
                values: new object[] { 1, "1536-23", "Miguel", "402-1234567-0", "Tejada", false });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "ARS", "Age", "Identification", "IsInsured", "MAId", "NSS", "Name", "Record", "Sex", "Weight" },
                values: new object[] { 1L, "SeNaSa", 25, "402-1234567-1", true, 1, "1234", "Guillermo Reyes", "1234", false, 145.30000000000001 });

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

            migrationBuilder.Sql(
            @"
                EXEC ('CREATE PROCEDURE SP_GetMedicalOrderById
                    @Id BIGINT,
                    @Type INT
                AS
                    BEGIN
                        SELECT * FROM MedicalOrders AS mo
		                    INNER JOIN Patients AS P ON mo.PatientId = P.Id
		                    INNER JOIN MAs AS ma ON mo.MAId = ma.Id
		                    INNER JOIN MedicalOrderDetails AS mo_det ON mo_det.MedicalOrderId = mo.Id
	                    WHERE mo.Id = @Id AND mo.Type = @Type;
                    END')");
            }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalOrderDetails");

            migrationBuilder.DropTable(
                name: "MedicalOrders");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "MAs");
        }
    }
}
