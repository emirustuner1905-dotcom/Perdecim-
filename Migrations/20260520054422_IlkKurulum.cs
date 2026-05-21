using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerdeCim.Migrations
{
    /// <inheritdoc />
    public partial class IlkKurulum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perdeler",
                columns: table => new
                {
                    PerdeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PerdeAdi = table.Column<string>(type: "TEXT", nullable: false),
                    PerdeModeli = table.Column<string>(type: "TEXT", nullable: false),
                    PerdeGorsel = table.Column<string>(type: "TEXT", nullable: false),
                    PerdeAciklama = table.Column<string>(type: "TEXT", nullable: false),
                    KumasTuru = table.Column<string>(type: "TEXT", nullable: false),
                    PerdeFiyati = table.Column<decimal>(type: "TEXT", nullable: false),
                    IndirimOrani = table.Column<int>(type: "INTEGER", nullable: true),
                    StokAdedi = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perdeler", x => x.PerdeId);
                });

            migrationBuilder.CreateTable(
                name: "Saticilar",
                columns: table => new
                {
                    SaticiId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SaticiAdi = table.Column<string>(type: "TEXT", nullable: true),
                    SaticiCv = table.Column<string>(type: "TEXT", nullable: true),
                    SaticiFoto = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saticilar", x => x.SaticiId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userName = table.Column<string>(type: "TEXT", nullable: true),
                    userPass = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Perdeler");

            migrationBuilder.DropTable(
                name: "Saticilar");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
