// <auto-generated />
using System;
using DataEF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataEF.Migrations
{
    [DbContext(typeof(MLApiDbContext))]
    [Migration("20230211133423_AddMoreGlobalConfig")]
    partial class AddMoreGlobalConfig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataEF.Models.Global.GlobalConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("GlobalConfigurations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Mercado libre client id",
                            Name = "CLIENT_ID",
                            Value = "",
                            active = true,
                            created = new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7545)
                        },
                        new
                        {
                            Id = 2,
                            Description = "Mercado libre client secret",
                            Name = "CLIENT_SECRET",
                            Value = "",
                            active = true,
                            created = new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7577)
                        },
                        new
                        {
                            Id = 3,
                            Description = "Mercado libre code generated",
                            Name = "ML_CODE",
                            Value = "",
                            active = true,
                            created = new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7580)
                        },
                        new
                        {
                            Id = 4,
                            Description = "Mercado libre token generated",
                            Name = "ACCESS_TOKEN",
                            Value = "",
                            active = true,
                            created = new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7582)
                        },
                        new
                        {
                            Id = 5,
                            Description = "Mercado libre token exprire time miliseconds",
                            Name = "ACCESS_TOKEN_EXPIRE",
                            Value = "",
                            active = true,
                            created = new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7584)
                        },
                        new
                        {
                            Id = 6,
                            Description = "Mercado libre token user id",
                            Name = "ACCESS_TOKEN_USERID",
                            Value = "",
                            active = true,
                            created = new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7587)
                        },
                        new
                        {
                            Id = 7,
                            Description = "Mercado libre refresh token",
                            Name = "REFRESH_TOKEN",
                            Value = "",
                            active = true,
                            created = new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7589)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
