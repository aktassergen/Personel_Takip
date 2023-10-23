using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SA.PTM.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Yoneticiler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciSifre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yoneticiler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YoneticiId = table.Column<int>(type: "int", nullable: false),
                    maasBilgisiId = table.Column<int>(type: "int", nullable: false),
                    EgitimBilgisiId = table.Column<int>(type: "int", nullable: false),
                    OzlukBilgisiId = table.Column<int>(type: "int", nullable: false),
                    TcNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Personeller_Yoneticiler_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "Yoneticiler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avanslar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    AvansMiktar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VerilisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OdemeVadesi = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    MaasMiktar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BordroTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    PersonelId = table.Column<int>(type: "int", nullable: false)
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
                    IzinBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IzinBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IzinTuru = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    IkametAdresi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaglikRaporu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdliSicilBelgesi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ehliyet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sozlesme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBasvurusu = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Okullar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OkulAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MezuniyetDerecesi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MezuniyetTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiplomaOrnegi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EgitimTipleri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EgitimBilgisiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Okullar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Okullar_EgitimBilgileri_EgitimBilgisiId",
                        column: x => x.EgitimBilgisiId,
                        principalTable: "EgitimBilgileri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sertifikalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SertifikaAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlinanKurum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SertifikaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EgitimBilgisiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sertifikalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sertifikalar_EgitimBilgileri_EgitimBilgisiId",
                        column: x => x.EgitimBilgisiId,
                        principalTable: "EgitimBilgileri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SirketIciEgitimler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EgitimAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlinmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EgitimBilgisiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SirketIciEgitimler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SirketIciEgitimler_EgitimBilgileri_EgitimBilgisiId",
                        column: x => x.EgitimBilgisiId,
                        principalTable: "EgitimBilgileri",
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
                column: "PersonelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IzinBilgileri_PersonelId",
                table: "IzinBilgileri",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MaasBilgileri_PersonelId",
                table: "MaasBilgileri",
                column: "PersonelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Okullar_EgitimBilgisiId",
                table: "Okullar",
                column: "EgitimBilgisiId");

            migrationBuilder.CreateIndex(
                name: "IX_OzlukBilgileri_PersonelId",
                table: "OzlukBilgileri",
                column: "PersonelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_TcNo",
                table: "Personeller",
                column: "TcNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_YoneticiId",
                table: "Personeller",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_Sertifikalar_EgitimBilgisiId",
                table: "Sertifikalar",
                column: "EgitimBilgisiId");

            migrationBuilder.CreateIndex(
                name: "IX_SirketIciEgitimler_EgitimBilgisiId",
                table: "SirketIciEgitimler",
                column: "EgitimBilgisiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avanslar");

            migrationBuilder.DropTable(
                name: "Bordrolar");

            migrationBuilder.DropTable(
                name: "IzinBilgileri");

            migrationBuilder.DropTable(
                name: "MaasBilgileri");

            migrationBuilder.DropTable(
                name: "Okullar");

            migrationBuilder.DropTable(
                name: "OzlukBilgileri");

            migrationBuilder.DropTable(
                name: "Sertifikalar");

            migrationBuilder.DropTable(
                name: "SirketIciEgitimler");

            migrationBuilder.DropTable(
                name: "EgitimBilgileri");

            migrationBuilder.DropTable(
                name: "Personeller");

            migrationBuilder.DropTable(
                name: "Yoneticiler");
        }
    }
}
