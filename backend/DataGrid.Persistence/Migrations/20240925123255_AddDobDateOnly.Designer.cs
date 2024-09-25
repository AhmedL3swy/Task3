﻿// <auto-generated />
using System;
using DataGrid.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataGrid.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240925123255_AddDobDateOnly")]
    partial class AddDobDateOnly
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataGrid.Domain.MaritalStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MaritalStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Single"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Married"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Divorced"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Widower"
                        });
                });

            modelBuilder.Entity("DataGrid.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateOnly?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("MaritalStatusId")
                        .HasColumnType("int");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasAnnotation("RegularExpression", "^05\\d{8}$");

                    b.Property<string>("MunicipalNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasAnnotation("RegularExpression", "^\\d{6,10}$");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasAnnotation("RegularExpression", "^\\d{10}$");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("MaritalStatusId");

                    b.HasIndex("Mobile")
                        .IsUnique();

                    b.HasIndex("MunicipalNo")
                        .IsUnique();

                    b.HasIndex("NationalId")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Address 1",
                            Email = "john.doe1@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            MaritalStatusId = 1,
                            Mobile = "0500000001",
                            MunicipalNo = "100001",
                            NationalId = "1234567890"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Address 2",
                            Email = "jane.smith1@example.com",
                            FirstName = "Jane",
                            LastName = "Smith",
                            MaritalStatusId = 1,
                            Mobile = "0500000002",
                            MunicipalNo = "100002",
                            NationalId = "1234567891"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Address 3",
                            Email = "alice.brown1@example.com",
                            FirstName = "Alice",
                            LastName = "Brown",
                            MaritalStatusId = 2,
                            Mobile = "0500000003",
                            MunicipalNo = "100003",
                            NationalId = "1234567892"
                        },
                        new
                        {
                            Id = 4,
                            Address = "Address 4",
                            Email = "bob.johnson1@example.com",
                            FirstName = "Bob",
                            LastName = "Johnson",
                            MaritalStatusId = 3,
                            Mobile = "0500000004",
                            MunicipalNo = "100004",
                            NationalId = "1234567893"
                        },
                        new
                        {
                            Id = 5,
                            Address = "Address 5",
                            Email = "chris.williams1@example.com",
                            FirstName = "Chris",
                            LastName = "Williams",
                            MaritalStatusId = 4,
                            Mobile = "0500000005",
                            MunicipalNo = "100005",
                            NationalId = "1234567894"
                        },
                        new
                        {
                            Id = 6,
                            Address = "Address 6",
                            Email = "diana.taylor1@example.com",
                            FirstName = "Diana",
                            LastName = "Taylor",
                            MaritalStatusId = 1,
                            Mobile = "0500000006",
                            MunicipalNo = "100006",
                            NationalId = "1234567895"
                        },
                        new
                        {
                            Id = 7,
                            Address = "Address 7",
                            Email = "ethan.anderson1@example.com",
                            FirstName = "Ethan",
                            LastName = "Anderson",
                            MaritalStatusId = 4,
                            Mobile = "0500000007",
                            MunicipalNo = "100007",
                            NationalId = "1234567896"
                        },
                        new
                        {
                            Id = 8,
                            Address = "Address 8",
                            Email = "fiona.clark1@example.com",
                            FirstName = "Fiona",
                            LastName = "Clark",
                            MaritalStatusId = 1,
                            Mobile = "0500000008",
                            MunicipalNo = "100008",
                            NationalId = "1234567897"
                        },
                        new
                        {
                            Id = 9,
                            Address = "Address 9",
                            Email = "george.davis1@example.com",
                            FirstName = "George",
                            LastName = "Davis",
                            MaritalStatusId = 2,
                            Mobile = "0500000009",
                            MunicipalNo = "100009",
                            NationalId = "1234567898"
                        },
                        new
                        {
                            Id = 10,
                            Address = "Address 10",
                            Email = "hannah.evans1@example.com",
                            FirstName = "Hannah",
                            LastName = "Evans",
                            MaritalStatusId = 1,
                            Mobile = "0500000010",
                            MunicipalNo = "100010",
                            NationalId = "1234567899"
                        },
                        new
                        {
                            Id = 11,
                            Address = "Address 11",
                            Email = "isaac.foster1@example.com",
                            FirstName = "Isaac",
                            LastName = "Foster",
                            MaritalStatusId = 3,
                            Mobile = "0500000011",
                            MunicipalNo = "100011",
                            NationalId = "1234567900"
                        },
                        new
                        {
                            Id = 12,
                            Address = "Address 12",
                            Email = "julia.gonzalez1@example.com",
                            FirstName = "Julia",
                            LastName = "Gonzalez",
                            MaritalStatusId = 1,
                            Mobile = "0500000012",
                            MunicipalNo = "100012",
                            NationalId = "1234567901"
                        },
                        new
                        {
                            Id = 13,
                            Address = "Address 13",
                            Email = "kevin.harris1@example.com",
                            FirstName = "Kevin",
                            LastName = "Harris",
                            MaritalStatusId = 1,
                            Mobile = "0500000013",
                            MunicipalNo = "100013",
                            NationalId = "1234567902"
                        },
                        new
                        {
                            Id = 14,
                            Address = "Address 14",
                            Email = "lily.irwin1@example.com",
                            FirstName = "Lily",
                            LastName = "Irwin",
                            MaritalStatusId = 1,
                            Mobile = "0500000014",
                            MunicipalNo = "100014",
                            NationalId = "1234567903"
                        },
                        new
                        {
                            Id = 15,
                            Address = "Address 15",
                            Email = "michael.jackson1@example.com",
                            FirstName = "Michael",
                            LastName = "Jackson",
                            MaritalStatusId = 2,
                            Mobile = "0500000015",
                            MunicipalNo = "100015",
                            NationalId = "1234567904"
                        },
                        new
                        {
                            Id = 16,
                            Address = "Address 16",
                            Email = "natalie.king1@example.com",
                            FirstName = "Natalie",
                            LastName = "King",
                            MaritalStatusId = 1,
                            Mobile = "0500000016",
                            MunicipalNo = "100016",
                            NationalId = "1234567905"
                        },
                        new
                        {
                            Id = 17,
                            Address = "Address 17",
                            Email = "oliver.lee1@example.com",
                            FirstName = "Oliver",
                            LastName = "Lee",
                            MaritalStatusId = 1,
                            Mobile = "0500000017",
                            MunicipalNo = "100017",
                            NationalId = "1234567906"
                        },
                        new
                        {
                            Id = 18,
                            Address = "Address 18",
                            Email = "penelope.miller1@example.com",
                            FirstName = "Penelope",
                            LastName = "Miller",
                            MaritalStatusId = 1,
                            Mobile = "0500000018",
                            MunicipalNo = "100018",
                            NationalId = "1234567907"
                        },
                        new
                        {
                            Id = 19,
                            Address = "Address 19",
                            Email = "quentin.nelson1@example.com",
                            FirstName = "Quentin",
                            LastName = "Nelson",
                            MaritalStatusId = 3,
                            Mobile = "0500000019",
                            MunicipalNo = "100019",
                            NationalId = "1234567908"
                        },
                        new
                        {
                            Id = 20,
                            Address = "Address 20",
                            Email = "rachel.oconnor1@example.com",
                            FirstName = "Rachel",
                            LastName = "O'Connor",
                            MaritalStatusId = 1,
                            Mobile = "0500000020",
                            MunicipalNo = "100020",
                            NationalId = "1234567909"
                        },
                        new
                        {
                            Id = 21,
                            Address = "Address 21",
                            Email = "samuel.parker1@example.com",
                            FirstName = "Samuel",
                            LastName = "Parker",
                            MaritalStatusId = 1,
                            Mobile = "0500000021",
                            MunicipalNo = "100021",
                            NationalId = "1234567910"
                        },
                        new
                        {
                            Id = 22,
                            Address = "Address 22",
                            Email = "tiffany.quinn1@example.com",
                            FirstName = "Tiffany",
                            LastName = "Quinn",
                            MaritalStatusId = 1,
                            Mobile = "0500000022",
                            MunicipalNo = "100022",
                            NationalId = "1234567911"
                        },
                        new
                        {
                            Id = 23,
                            Address = "Address 23",
                            Email = "ulysses.robinson1@example.com",
                            FirstName = "Ulysses",
                            LastName = "Robinson",
                            MaritalStatusId = 3,
                            Mobile = "0500000023",
                            MunicipalNo = "100023",
                            NationalId = "1234567912"
                        },
                        new
                        {
                            Id = 24,
                            Address = "Address 24",
                            Email = "victoria.smith1@example.com",
                            FirstName = "Victoria",
                            LastName = "Smith",
                            MaritalStatusId = 1,
                            Mobile = "0500000024",
                            MunicipalNo = "100024",
                            NationalId = "1234567913"
                        },
                        new
                        {
                            Id = 25,
                            Address = "Address 25",
                            Email = "william.taylor1@example.com",
                            FirstName = "William",
                            LastName = "Taylor",
                            MaritalStatusId = 2,
                            Mobile = "0500000025",
                            MunicipalNo = "100025",
                            NationalId = "1234567914"
                        },
                        new
                        {
                            Id = 26,
                            Address = "Address 26",
                            Email = "xavier.upton1@example.com",
                            FirstName = "Xavier",
                            LastName = "Upton",
                            MaritalStatusId = 1,
                            Mobile = "0500000026",
                            MunicipalNo = "100026",
                            NationalId = "1234567915"
                        },
                        new
                        {
                            Id = 27,
                            Address = "Address 27",
                            Email = "yvonne.vargas1@example.com",
                            FirstName = "Yvonne",
                            LastName = "Vargas",
                            MaritalStatusId = 3,
                            Mobile = "0500000027",
                            MunicipalNo = "100027",
                            NationalId = "1234567916"
                        },
                        new
                        {
                            Id = 28,
                            Address = "Address 28",
                            Email = "zachary.watson1@example.com",
                            FirstName = "Zachary",
                            LastName = "Watson",
                            MaritalStatusId = 1,
                            Mobile = "0500000028",
                            MunicipalNo = "100028",
                            NationalId = "1234567917"
                        },
                        new
                        {
                            Id = 29,
                            Address = "Address 29",
                            Email = "amy.young1@example.com",
                            FirstName = "Amy",
                            LastName = "Young",
                            MaritalStatusId = 4,
                            Mobile = "0500000029",
                            MunicipalNo = "100029",
                            NationalId = "1234567918"
                        },
                        new
                        {
                            Id = 30,
                            Address = "Address 30",
                            Email = "david.green1@example.com",
                            FirstName = "David",
                            LastName = "Green",
                            MaritalStatusId = 2,
                            Mobile = "0500000030",
                            MunicipalNo = "100030",
                            NationalId = "1234567919"
                        });
                });

            modelBuilder.Entity("DataGrid.Domain.User", b =>
                {
                    b.HasOne("DataGrid.Domain.MaritalStatus", "MaritalStatus")
                        .WithMany("Users")
                        .HasForeignKey("MaritalStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MaritalStatus");
                });

            modelBuilder.Entity("DataGrid.Domain.MaritalStatus", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
