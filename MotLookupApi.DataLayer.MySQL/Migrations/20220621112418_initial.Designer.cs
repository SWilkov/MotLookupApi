// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MotLookupApi.DataLayer.MySQL.Data;

#nullable disable

namespace MotLookupApi.DataLayer.MySQL.Migrations
{
    [DbContext(typeof(VehicleDataContext))]
    [Migration("20220621112418_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MotLookupApi.DataLayer.MySQL.DataModels.CommentDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<bool>("Dangerous")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("dangerous");

                    b.Property<int>("MotTestId")
                        .HasColumnType("int")
                        .HasColumnName("mot_test_id");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("MotTestId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("MotLookupApi.DataLayer.MySQL.DataModels.MotTestDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CompletedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("completed_date");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("expiry_date");

                    b.Property<int>("Mileage")
                        .HasColumnType("int")
                        .HasColumnName("mileage");

                    b.Property<string>("OdometerResultType")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("odometer_result_type");

                    b.Property<string>("OdometerUnit")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("odometer_unit");

                    b.Property<long>("TestNumber")
                        .HasColumnType("bigint")
                        .HasColumnName("test_number");

                    b.Property<string>("TestResult")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("test_result");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int")
                        .HasColumnName("vehicle_id");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("MotTests");
                });

            modelBuilder.Entity("MotLookupApi.DataLayer.MySQL.DataModels.VehicleDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<long>("DvlaId")
                        .HasColumnType("bigint")
                        .HasColumnName("dvla_id");

                    b.Property<string>("EngineSize")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("engine_size");

                    b.Property<DateTime>("FirstUsedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("first_used_date");

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("fuel_type");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("make");

                    b.Property<DateTime>("ManufactureDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("manufacture_date");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("model");

                    b.Property<DateTime>("MotTestDueDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("mot_test_due_date");

                    b.Property<string>("PrimaryColour")
                        .HasColumnType("longtext")
                        .HasColumnName("primary_colour");

                    b.Property<string>("Registration")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("registration");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("registration_date");

                    b.Property<string>("UniqueVehicleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("unique_vehicle_id");

                    b.HasKey("Id");

                    b.HasIndex("UniqueVehicleId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("MotLookupApi.DataLayer.MySQL.DataModels.VehicleQueryDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int")
                        .HasColumnName("vehicle_id");

                    b.HasKey("Id");

                    b.ToTable("VehicleQueries");
                });

            modelBuilder.Entity("MotLookupApi.DataLayer.MySQL.DataModels.CommentDataModel", b =>
                {
                    b.HasOne("MotLookupApi.DataLayer.MySQL.DataModels.MotTestDataModel", "MotTest")
                        .WithMany("Comments")
                        .HasForeignKey("MotTestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MotTest");
                });

            modelBuilder.Entity("MotLookupApi.DataLayer.MySQL.DataModels.MotTestDataModel", b =>
                {
                    b.HasOne("MotLookupApi.DataLayer.MySQL.DataModels.VehicleDataModel", "Vehicle")
                        .WithMany("MotTests")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("MotLookupApi.DataLayer.MySQL.DataModels.MotTestDataModel", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("MotLookupApi.DataLayer.MySQL.DataModels.VehicleDataModel", b =>
                {
                    b.Navigation("MotTests");
                });
#pragma warning restore 612, 618
        }
    }
}
