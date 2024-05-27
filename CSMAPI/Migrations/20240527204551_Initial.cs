using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CSMAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cruises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StarRating = table.Column<double>(type: "float", nullable: false),
                    Seating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cruises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BuyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cruises",
                columns: new[] { "Id", "EndDate", "Name", "Seating", "StarRating", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 25, 23, 45, 50, 999, DateTimeKind.Local).AddTicks(3465), "Fetorna", 2000, 4.2999999999999998, new DateTime(2024, 5, 27, 23, 45, 50, 999, DateTimeKind.Local).AddTicks(3463) },
                    { 2, new DateTime(2024, 9, 4, 23, 45, 50, 999, DateTimeKind.Local).AddTicks(3470), "Remoria", 250, 5.0, new DateTime(2024, 5, 17, 23, 45, 50, 999, DateTimeKind.Local).AddTicks(3468) },
                    { 3, new DateTime(2024, 9, 30, 23, 45, 50, 999, DateTimeKind.Local).AddTicks(3474), "Extasy", 4000, 4.7999999999999998, new DateTime(2024, 6, 10, 23, 45, 50, 999, DateTimeKind.Local).AddTicks(3472) }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Address", "DateOfBirth", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, "st. Saedinenie 2a", new DateTime(2003, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "stu2201321037@uni-plovdiv.bg", "Yoan", "Kimanov", "+359 897 8595 58" },
                    { 2, "st. Snejanka 54", new DateTime(2006, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "i.ivanov@gmail.com", "Ivano", "Ivanov", "+359 895 6363 42" },
                    { 3, "st. Asmokorev 3", new DateTime(2000, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "yan.bi@bi.yan", "Yan", "Bibiyan", "+359 888 8888 88" },
                    { 4, "st. Yorkshire 15", new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jobody@yahoo.com", "John", "Nobody", "+1 202 555 0118" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "BuyDate", "Description", "ExpireDate", "PersonId", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 27, 23, 45, 50, 999, DateTimeKind.Local).AddTicks(3396), "Access to Cruise - Fetorna (From France to Japan), Medium-sized room, Free Breakfast and Lunch", new DateTime(2025, 5, 27, 23, 45, 50, 999, DateTimeKind.Local).AddTicks(3437), 1, 1000.0 },
                    { 2, new DateTime(2024, 5, 7, 23, 45, 50, 999, DateTimeKind.Local).AddTicks(3442), "Access to Cruise - Remoria (World Cruise), King-sized room, Free Breakfast, Lunch and Alchohol", new DateTime(2025, 5, 27, 23, 45, 50, 999, DateTimeKind.Local).AddTicks(3444), 2, 5200.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PersonId",
                table: "Tickets",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cruises");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
