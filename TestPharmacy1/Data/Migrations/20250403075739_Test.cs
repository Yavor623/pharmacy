using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestPharmacy1.Data.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "ConsistencyOfMedication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsistencyOfMedication", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrescribedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.PrescriptionId);
                    table.ForeignKey(
                        name: "FK_Prescription_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfMedication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfMedication", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsPrescriptionNeeded = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TypeOfMedicationId = table.Column<int>(type: "int", nullable: false),
                    ConsistencyOfMedicationId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medication_ConsistencyOfMedication_ConsistencyOfMedicationId",
                        column: x => x.ConsistencyOfMedicationId,
                        principalTable: "ConsistencyOfMedication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medication_TypeOfMedication_TypeOfMedicationId",
                        column: x => x.TypeOfMedicationId,
                        principalTable: "TypeOfMedication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnedMedication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnedMedication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnedMedication_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnedMedication_Medication_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ConsistencyOfMedication",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "таблетки" },
                    { 2, "капсули" },
                    { 3, "прах" },
                    { 4, "гранули" },
                    { 5, "разтвор" },
                    { 6, "емулсия" },
                    { 7, "суспензия" },
                    { 8, "сироп" },
                    { 9, "капки" },
                    { 10, "запарка, отвара" },
                    { 11, "тинктура" },
                    { 12, "инжекционни форми" },
                    { 13, "крем" },
                    { 14, "гел, желе" },
                    { 15, "паста" },
                    { 16, "маз, мехлем" },
                    { 17, "свещичка" },
                    { 18, "пяна" },
                    { 19, "пластир, трансдермални терапевтични системи" },
                    { 20, "спрей" }
                });

            migrationBuilder.InsertData(
                table: "TypeOfMedication",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Адренокортикоактивни средства" },
                    { 2, "Адренолитици" },
                    { 3, "Адреномиметици" },
                    { 4, "Анксиолитици (транквилизатори)" },
                    { 5, "Антиаритмични средства" },
                    { 6, "Антибактериалните средства" },
                    { 7, "Антидепресанти" },
                    { 8, "Антидиабетични средства" },
                    { 9, "Антидиарични средства" },
                    { 10, "Антиеметични средства" },
                    { 11, "Антиепилептични средства" },
                    { 12, "Антимикотични средства" },
                    { 13, "Антисекреторни средства" },
                    { 14, "Антиулкусни средства" },
                    { 15, "Антихипогликемични средства" },
                    { 16, "Гонадоактивни средства" },
                    { 17, "Диуретици" },
                    { 18, "Ензимни панкреатични средства" },
                    { 19, "Калциеви антагонисти" },
                    { 20, "М-холинолитици" },
                    { 21, "Муколитици" },
                    { 22, "Невролептици (антипсихотици)" },
                    { 23, "Неопиоидни (антипиретични) аналгетици" },
                    { 24, "Нестероидни противовъзпалителни средства (Сох-инхибитори)" },
                    { 25, "Орални антидиабетични средства" },
                    { 26, "Очистителни (лаксативни) средства" },
                    { 27, "Противовирусни средства" },
                    { 28, "Средства, повлияващи растежния хормон" },
                    { 29, "Средства, прилагани при суха кашлица" },
                    { 30, "Тиреоактивни средства" },
                    { 31, "Хепатопротектори" },
                    { 32, "Холеретични (жлъчетворни) и холекинетични (жлъчегонни) средства" },
                    { 33, "Холиномиметици" },
                    { 34, "Хормонални контрацептиви" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medication_ConsistencyOfMedicationId",
                table: "Medication",
                column: "ConsistencyOfMedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_TypeOfMedicationId",
                table: "Medication",
                column: "TypeOfMedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedMedication_MedicationId",
                table: "OwnedMedication",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedMedication_UserId",
                table: "OwnedMedication",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_UserId",
                table: "Prescription",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnedMedication");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "Medication");

            migrationBuilder.DropTable(
                name: "ConsistencyOfMedication");

            migrationBuilder.DropTable(
                name: "TypeOfMedication");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
