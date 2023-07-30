using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Entity.Migrations
{
    /// <inheritdoc />
    public partial class DbTableUptaded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IletisimBilgileri_Oteller_OtelId",
                table: "IletisimBilgileri");

            migrationBuilder.DropForeignKey(
                name: "FK_OtelYetkilileri_Oteller_OtelId",
                table: "OtelYetkilileri");

            migrationBuilder.DropForeignKey(
                name: "FK_Raporlar_Oteller_OtelId",
                table: "Raporlar");

            migrationBuilder.AlterColumn<int>(
                name: "OtelId",
                table: "Raporlar",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Durum",
                table: "Raporlar",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "OtelYetkilisiId",
                table: "Raporlar",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Soyad",
                table: "OtelYetkilileri",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "OtelId",
                table: "OtelYetkilileri",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "FirmaUnvan",
                table: "OtelYetkilileri",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "OtelYetkilileri",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Oteller",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "OtelId",
                table: "IletisimBilgileri",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "BilgiTipi",
                table: "IletisimBilgileri",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BilgiIcerigi",
                table: "IletisimBilgileri",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "IletisimBilgileri",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "IletisimBilgileri",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Raporlar_OtelYetkilisiId",
                table: "Raporlar",
                column: "OtelYetkilisiId");

            migrationBuilder.AddForeignKey(
                name: "FK_IletisimBilgileri_Oteller_OtelId",
                table: "IletisimBilgileri",
                column: "OtelId",
                principalTable: "Oteller",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtelYetkilileri_Oteller_OtelId",
                table: "OtelYetkilileri",
                column: "OtelId",
                principalTable: "Oteller",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Raporlar_OtelYetkilileri_OtelYetkilisiId",
                table: "Raporlar",
                column: "OtelYetkilisiId",
                principalTable: "OtelYetkilileri",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Raporlar_Oteller_OtelId",
                table: "Raporlar",
                column: "OtelId",
                principalTable: "Oteller",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IletisimBilgileri_Oteller_OtelId",
                table: "IletisimBilgileri");

            migrationBuilder.DropForeignKey(
                name: "FK_OtelYetkilileri_Oteller_OtelId",
                table: "OtelYetkilileri");

            migrationBuilder.DropForeignKey(
                name: "FK_Raporlar_OtelYetkilileri_OtelYetkilisiId",
                table: "Raporlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Raporlar_Oteller_OtelId",
                table: "Raporlar");

            migrationBuilder.DropIndex(
                name: "IX_Raporlar_OtelYetkilisiId",
                table: "Raporlar");

            migrationBuilder.DropColumn(
                name: "OtelYetkilisiId",
                table: "Raporlar");

            migrationBuilder.DropColumn(
                name: "Adres",
                table: "IletisimBilgileri");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "IletisimBilgileri");

            migrationBuilder.AlterColumn<int>(
                name: "OtelId",
                table: "Raporlar",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Durum",
                table: "Raporlar",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Soyad",
                table: "OtelYetkilileri",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OtelId",
                table: "OtelYetkilileri",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirmaUnvan",
                table: "OtelYetkilileri",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "OtelYetkilileri",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Oteller",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OtelId",
                table: "IletisimBilgileri",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BilgiTipi",
                table: "IletisimBilgileri",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BilgiIcerigi",
                table: "IletisimBilgileri",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IletisimBilgileri_Oteller_OtelId",
                table: "IletisimBilgileri",
                column: "OtelId",
                principalTable: "Oteller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OtelYetkilileri_Oteller_OtelId",
                table: "OtelYetkilileri",
                column: "OtelId",
                principalTable: "Oteller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Raporlar_Oteller_OtelId",
                table: "Raporlar",
                column: "OtelId",
                principalTable: "Oteller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
