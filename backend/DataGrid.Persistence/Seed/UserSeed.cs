using DataGrid.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataGrid.Persistence.Seed
{
    public static class UserSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe1@example.com", Mobile = "0500000001", MunicipalNo = "100001", NationalId = "1234567890", MaritalStatusId = 1, Address = "Address 1" },
                new User { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith1@example.com", Mobile = "0500000002", MunicipalNo = "100002", NationalId = "1234567891", MaritalStatusId = 1, Address = "Address 2" },
                new User { Id = 3, FirstName = "Alice", LastName = "Brown", Email = "alice.brown1@example.com", Mobile = "0500000003", MunicipalNo = "100003", NationalId = "1234567892", MaritalStatusId = 2, Address = "Address 3" },
                new User { Id = 4, FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson1@example.com", Mobile = "0500000004", MunicipalNo = "100004", NationalId = "1234567893", MaritalStatusId = 3, Address = "Address 4" },
                new User { Id = 5, FirstName = "Chris", LastName = "Williams", Email = "chris.williams1@example.com", Mobile = "0500000005", MunicipalNo = "100005", NationalId = "1234567894", MaritalStatusId = 4, Address = "Address 5" },
                new User { Id = 6, FirstName = "Diana", LastName = "Taylor", Email = "diana.taylor1@example.com", Mobile = "0500000006", MunicipalNo = "100006", NationalId = "1234567895", MaritalStatusId = 1, Address = "Address 6" },
                new User { Id = 7, FirstName = "Ethan", LastName = "Anderson", Email = "ethan.anderson1@example.com", Mobile = "0500000007", MunicipalNo = "100007", NationalId = "1234567896", MaritalStatusId = 4, Address = "Address 7" },
                new User { Id = 8, FirstName = "Fiona", LastName = "Clark", Email = "fiona.clark1@example.com", Mobile = "0500000008", MunicipalNo = "100008", NationalId = "1234567897", MaritalStatusId = 1, Address = "Address 8" },
                new User { Id = 9, FirstName = "George", LastName = "Davis", Email = "george.davis1@example.com", Mobile = "0500000009", MunicipalNo = "100009", NationalId = "1234567898", MaritalStatusId = 2, Address = "Address 9" },
                new User { Id = 10, FirstName = "Hannah", LastName = "Evans", Email = "hannah.evans1@example.com", Mobile = "0500000010", MunicipalNo = "100010", NationalId = "1234567899", MaritalStatusId = 1, Address = "Address 10" },
                new User { Id = 11, FirstName = "Isaac", LastName = "Foster", Email = "isaac.foster1@example.com", Mobile = "0500000011", MunicipalNo = "100011", NationalId = "1234567900", MaritalStatusId = 3, Address = "Address 11" },
                new User { Id = 12, FirstName = "Julia", LastName = "Gonzalez", Email = "julia.gonzalez1@example.com", Mobile = "0500000012", MunicipalNo = "100012", NationalId = "1234567901", MaritalStatusId = 1, Address = "Address 12" },
                new User { Id = 13, FirstName = "Kevin", LastName = "Harris", Email = "kevin.harris1@example.com", Mobile = "0500000013", MunicipalNo = "100013", NationalId = "1234567902", MaritalStatusId = 1, Address = "Address 13" },
                new User { Id = 14, FirstName = "Lily", LastName = "Irwin", Email = "lily.irwin1@example.com", Mobile = "0500000014", MunicipalNo = "100014", NationalId = "1234567903", MaritalStatusId = 1, Address = "Address 14" },
                new User { Id = 15, FirstName = "Michael", LastName = "Jackson", Email = "michael.jackson1@example.com", Mobile = "0500000015", MunicipalNo = "100015", NationalId = "1234567904", MaritalStatusId = 2, Address = "Address 15" },
                new User { Id = 16, FirstName = "Natalie", LastName = "King", Email = "natalie.king1@example.com", Mobile = "0500000016", MunicipalNo = "100016", NationalId = "1234567905", MaritalStatusId = 1, Address = "Address 16" },
                new User { Id = 17, FirstName = "Oliver", LastName = "Lee", Email = "oliver.lee1@example.com", Mobile = "0500000017", MunicipalNo = "100017", NationalId = "1234567906", MaritalStatusId = 1, Address = "Address 17" },
                new User { Id = 18, FirstName = "Penelope", LastName = "Miller", Email = "penelope.miller1@example.com", Mobile = "0500000018", MunicipalNo = "100018", NationalId = "1234567907", MaritalStatusId = 1, Address = "Address 18" },
                new User { Id = 19, FirstName = "Quentin", LastName = "Nelson", Email = "quentin.nelson1@example.com", Mobile = "0500000019", MunicipalNo = "100019", NationalId = "1234567908", MaritalStatusId = 3, Address = "Address 19" },
                new User { Id = 20, FirstName = "Rachel", LastName = "O'Connor", Email = "rachel.oconnor1@example.com", Mobile = "0500000020", MunicipalNo = "100020", NationalId = "1234567909", MaritalStatusId = 1, Address = "Address 20" },
                new User { Id = 21, FirstName = "Samuel", LastName = "Parker", Email = "samuel.parker1@example.com", Mobile = "0500000021", MunicipalNo = "100021", NationalId = "1234567910", MaritalStatusId = 1, Address = "Address 21" },
                new User { Id = 22, FirstName = "Tiffany", LastName = "Quinn", Email = "tiffany.quinn1@example.com", Mobile = "0500000022", MunicipalNo = "100022", NationalId = "1234567911", MaritalStatusId = 1, Address = "Address 22" },
                new User { Id = 23, FirstName = "Ulysses", LastName = "Robinson", Email = "ulysses.robinson1@example.com", Mobile = "0500000023", MunicipalNo = "100023", NationalId = "1234567912", MaritalStatusId = 3, Address = "Address 23" },
                new User { Id = 24, FirstName = "Victoria", LastName = "Smith", Email = "victoria.smith1@example.com", Mobile = "0500000024", MunicipalNo = "100024", NationalId = "1234567913", MaritalStatusId = 1, Address = "Address 24" },
                new User { Id = 25, FirstName = "William", LastName = "Taylor", Email = "william.taylor1@example.com", Mobile = "0500000025", MunicipalNo = "100025", NationalId = "1234567914", MaritalStatusId = 2, Address = "Address 25" },
                new User { Id = 26, FirstName = "Xavier", LastName = "Upton", Email = "xavier.upton1@example.com", Mobile = "0500000026", MunicipalNo = "100026", NationalId = "1234567915", MaritalStatusId = 1, Address = "Address 26" },
                new User { Id = 27, FirstName = "Yvonne", LastName = "Vargas", Email = "yvonne.vargas1@example.com", Mobile = "0500000027", MunicipalNo = "100027", NationalId = "1234567916", MaritalStatusId = 3, Address = "Address 27" },
                new User { Id = 28, FirstName = "Zachary", LastName = "Watson", Email = "zachary.watson1@example.com", Mobile = "0500000028", MunicipalNo = "100028", NationalId = "1234567917", MaritalStatusId = 1, Address = "Address 28" },
                new User { Id = 29, FirstName = "Amy", LastName = "Young", Email = "amy.young1@example.com", Mobile = "0500000029", MunicipalNo = "100029", NationalId = "1234567918", MaritalStatusId = 4, Address = "Address 29" },
                new User { Id = 30, FirstName = "David", LastName = "Green", Email = "david.green1@example.com", Mobile = "0500000030", MunicipalNo = "100030", NationalId = "1234567919", MaritalStatusId = 2, Address = "Address 30" }
            );
            // Seed Marital Status
            modelBuilder.Entity<MaritalStatus>().HasData(
                new MaritalStatus { Id = 1, Name = "Single" },
                new MaritalStatus { Id = 2, Name = "Married" },
                new MaritalStatus { Id = 3, Name = "Divorced" },
                new MaritalStatus { Id = 4, Name = "Widower" }
            );
        }
    }
}
