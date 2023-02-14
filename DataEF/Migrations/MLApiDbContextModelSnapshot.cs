﻿// <auto-generated />
using System;
using DataEF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataEF.Migrations
{
    [DbContext(typeof(MLApiDbContext))]
    partial class MLApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                            created = new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1231)
                        },
                        new
                        {
                            Id = 2,
                            Description = "Mercado libre client secret",
                            Name = "CLIENT_SECRET",
                            Value = "",
                            active = true,
                            created = new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1266)
                        },
                        new
                        {
                            Id = 3,
                            Description = "Mercado libre code generated",
                            Name = "ML_CODE",
                            Value = "",
                            active = true,
                            created = new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1269)
                        },
                        new
                        {
                            Id = 4,
                            Description = "Mercado libre token generated",
                            Name = "ACCESS_TOKEN",
                            Value = "",
                            active = true,
                            created = new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1271)
                        },
                        new
                        {
                            Id = 5,
                            Description = "Mercado libre token exprire time miliseconds",
                            Name = "ACCESS_TOKEN_EXPIRE",
                            Value = "",
                            active = true,
                            created = new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1273)
                        },
                        new
                        {
                            Id = 6,
                            Description = "Mercado libre token user id",
                            Name = "ACCESS_TOKEN_USERID",
                            Value = "",
                            active = true,
                            created = new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1275)
                        },
                        new
                        {
                            Id = 7,
                            Description = "Mercado libre refresh token",
                            Name = "REFRESH_TOKEN",
                            Value = "",
                            active = true,
                            created = new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1277)
                        },
                        new
                        {
                            Id = 8,
                            Description = "Mercado libre exprire date",
                            Name = "ACCESS_TOKEN_EXPIRE_DATE",
                            Value = "",
                            active = true,
                            created = new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1279)
                        });
                });

            modelBuilder.Entity("DataEF.Models.MLCatalogs.MLSites", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DefaultCurrencyId")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("MLId")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.HasKey("Id");

                    b.ToTable("MLSites");
                });

            modelBuilder.Entity("DataEF.Models.MLUsers.MLUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsTestUser")
                        .HasColumnType("bit");

                    b.Property<int>("MLId")
                        .HasColumnType("int");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("site_status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MLUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
