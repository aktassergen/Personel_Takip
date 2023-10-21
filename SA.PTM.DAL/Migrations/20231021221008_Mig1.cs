using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SA.PTM.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DogumYeri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anne = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Baba = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YasadigiSehir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedeniDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fotograf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Departman = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Avanslar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    AvansMiktar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VerilisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OdemeVadesi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avanslar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avanslar_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bordrolar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    MaasMiktar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BordroTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bordrolar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bordrolar_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EgitimBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    OkulAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MezuniyetDerecesi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MezuniyetTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiplomaninOrnegi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SertifikaAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SirketIciEgitim = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EgitimBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EgitimBilgileri_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IzinBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    IzinBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IzinBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IzinTuru = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzinBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IzinBilgileri_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaasBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    MaasMiktar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OdemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaasBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaasBilgileri_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OzlukBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    IkametAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaglikRaporu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdliSicilBelgesi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ehliyet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sozlesme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBasvurusu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AileCuzdani = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AskerlikBelgesi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OzlukBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OzlukBilgileri_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avanslar_PersonelId",
                table: "Avanslar",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Bordrolar_PersonelId",
                table: "Bordrolar",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_EgitimBilgileri_PersonelId",
                table: "EgitimBilgileri",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_IzinBilgileri_PersonelId",
                table: "IzinBilgileri",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MaasBilgileri_PersonelId",
                table: "MaasBilgileri",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_OzlukBilgileri_PersonelId",
                table: "OzlukBilgileri",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_TcNo",
                table: "Personeller",
                column: "TcNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avanslar");

            migrationBuilder.DropTable(
                name: "Bordrolar");

            migrationBuilder.DropTable(
                name: "EgitimBilgileri");

            migrationBuilder.DropTable(
                name: "IzinBilgileri");

            migrationBuilder.DropTable(
                name: "MaasBilgileri");

            migrationBuilder.DropTable(
                name: "OzlukBilgileri");

            migrationBuilder.DropTable(
                name: "Personeller");
        }
    }
}
