using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uyeler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Soyad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    KullaniciAdi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    KullaniciYorum = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    MailAdresi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    KullaniciResimYolu = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: true),
                    DogumGunu = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OnayliMi = table.Column<bool>(type: "boolean", nullable: false),
                    MasterId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(name: "Created Date", type: "timestamp with time zone", nullable: true),
                    CreatedComputerName = table.Column<string>(name: "Created Computer Name", type: "text", nullable: true),
                    CreatedIP = table.Column<string>(name: "Created IP", type: "text", nullable: true),
                    CreatedADUserName = table.Column<string>(name: "Created AD User Name", type: "text", nullable: true),
                    CreatedBy = table.Column<string>(name: "Created By", type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(name: "Modified Date", type: "timestamp with time zone", nullable: true),
                    ModifiedComputerName = table.Column<string>(name: "Modified Computer Name", type: "text", nullable: true),
                    ModifiedIP = table.Column<string>(name: "Modified IP", type: "text", nullable: true),
                    ModifiedADUserName = table.Column<string>(name: "Modified AD User Name", type: "text", nullable: true),
                    ModifiedBy = table.Column<string>(name: "Modified By", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uyeler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Makaleler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MakaleIcerigi = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: true),
                    MakaleBasligi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ResimYolu = table.Column<string>(type: "text", nullable: true),
                    OkunmaSayisi = table.Column<int>(type: "integer", nullable: false),
                    OnayliMi = table.Column<bool>(type: "boolean", nullable: false),
                    UyeId = table.Column<Guid>(type: "uuid", nullable: false),
                    MasterId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(name: "Created Date", type: "timestamp with time zone", nullable: true),
                    CreatedComputerName = table.Column<string>(name: "Created Computer Name", type: "text", nullable: true),
                    CreatedIP = table.Column<string>(name: "Created IP", type: "text", nullable: true),
                    CreatedADUserName = table.Column<string>(name: "Created AD User Name", type: "text", nullable: true),
                    CreatedBy = table.Column<string>(name: "Created By", type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(name: "Modified Date", type: "timestamp with time zone", nullable: true),
                    ModifiedComputerName = table.Column<string>(name: "Modified Computer Name", type: "text", nullable: true),
                    ModifiedIP = table.Column<string>(name: "Modified IP", type: "text", nullable: true),
                    ModifiedADUserName = table.Column<string>(name: "Modified AD User Name", type: "text", nullable: true),
                    ModifiedBy = table.Column<string>(name: "Modified By", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makaleler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Makaleler_Uyeler_UyeId",
                        column: x => x.UyeId,
                        principalTable: "Uyeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Makaleler_UyeId",
                table: "Makaleler",
                column: "UyeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Makaleler");

            migrationBuilder.DropTable(
                name: "Uyeler");
        }
    }
}
