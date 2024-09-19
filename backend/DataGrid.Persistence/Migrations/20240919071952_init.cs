using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataGrid.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MunicipalNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "FirstName", "LastName", "MaritalStatus", "Mobile", "MunicipalNo", "NationalId" },
                values: new object[,]
                {
                    { 1, "Address 1", "john.doe1@example.com", "John", "Doe", 0, "0500000001", "100001", "1234567890" },
                    { 2, "Address 2", "jane.smith1@example.com", "Jane", "Smith", 1, "0500000002", "100002", "1234567891" },
                    { 3, "Address 3", "alice.brown1@example.com", "Alice", "Brown", 2, "0500000003", "100003", "1234567892" },
                    { 4, "Address 4", "bob.johnson1@example.com", "Bob", "Johnson", 3, "0500000004", "100004", "1234567893" },
                    { 5, "Address 5", "chris.williams1@example.com", "Chris", "Williams", 0, "0500000005", "100005", "1234567894" },
                    { 6, "Address 6", "diana.taylor1@example.com", "Diana", "Taylor", 1, "0500000006", "100006", "1234567895" },
                    { 7, "Address 7", "ethan.anderson1@example.com", "Ethan", "Anderson", 0, "0500000007", "100007", "1234567896" },
                    { 8, "Address 8", "fiona.clark1@example.com", "Fiona", "Clark", 1, "0500000008", "100008", "1234567897" },
                    { 9, "Address 9", "george.davis1@example.com", "George", "Davis", 0, "0500000009", "100009", "1234567898" },
                    { 10, "Address 10", "hannah.evans1@example.com", "Hannah", "Evans", 1, "0500000010", "100010", "1234567899" },
                    { 11, "Address 11", "isaac.foster1@example.com", "Isaac", "Foster", 0, "0500000011", "100011", "1234567900" },
                    { 12, "Address 12", "julia.gonzalez1@example.com", "Julia", "Gonzalez", 1, "0500000012", "100012", "1234567901" },
                    { 13, "Address 13", "kevin.harris1@example.com", "Kevin", "Harris", 0, "0500000013", "100013", "1234567902" },
                    { 14, "Address 14", "lily.irwin1@example.com", "Lily", "Irwin", 1, "0500000014", "100014", "1234567903" },
                    { 15, "Address 15", "michael.jackson1@example.com", "Michael", "Jackson", 0, "0500000015", "100015", "1234567904" },
                    { 16, "Address 16", "natalie.king1@example.com", "Natalie", "King", 1, "0500000016", "100016", "1234567905" },
                    { 17, "Address 17", "oliver.lee1@example.com", "Oliver", "Lee", 0, "0500000017", "100017", "1234567906" },
                    { 18, "Address 18", "penelope.miller1@example.com", "Penelope", "Miller", 1, "0500000018", "100018", "1234567907" },
                    { 19, "Address 19", "quentin.nelson1@example.com", "Quentin", "Nelson", 0, "0500000019", "100019", "1234567908" },
                    { 20, "Address 20", "rachel.oconnor1@example.com", "Rachel", "O'Connor", 1, "0500000020", "100020", "1234567909" },
                    { 21, "Address 21", "samuel.parker1@example.com", "Samuel", "Parker", 0, "0500000021", "100021", "1234567910" },
                    { 22, "Address 22", "tiffany.quinn1@example.com", "Tiffany", "Quinn", 1, "0500000022", "100022", "1234567911" },
                    { 23, "Address 23", "ulysses.robinson1@example.com", "Ulysses", "Robinson", 0, "0500000023", "100023", "1234567912" },
                    { 24, "Address 24", "victoria.smith1@example.com", "Victoria", "Smith", 1, "0500000024", "100024", "1234567913" },
                    { 25, "Address 25", "william.taylor1@example.com", "William", "Taylor", 0, "0500000025", "100025", "1234567914" },
                    { 26, "Address 26", "xavier.upton1@example.com", "Xavier", "Upton", 1, "0500000026", "100026", "1234567915" },
                    { 27, "Address 27", "yvonne.vargas1@example.com", "Yvonne", "Vargas", 0, "0500000027", "100027", "1234567916" },
                    { 28, "Address 28", "zachary.watson1@example.com", "Zachary", "Watson", 1, "0500000028", "100028", "1234567917" },
                    { 29, "Address 29", "amy.young1@example.com", "Amy", "Young", 0, "0500000029", "100029", "1234567918" },
                    { 30, "Address 30", "david.green1@example.com", "David", "Green", 2, "0500000030", "100030", "1234567919" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Mobile",
                table: "Users",
                column: "Mobile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MunicipalNo",
                table: "Users",
                column: "MunicipalNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_NationalId",
                table: "Users",
                column: "NationalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
