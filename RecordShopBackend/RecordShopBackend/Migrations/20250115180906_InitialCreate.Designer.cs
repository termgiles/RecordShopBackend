﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecordShopBackend.Database;

#nullable disable

namespace RecordShopBackend.Migrations
{
    [DbContext(typeof(RecordShopDbContext))]
    [Migration("20250115180906_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RecordShopBackend.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Released")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Albums");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Artist = "L'Ed Sheeran",
                            Genre = "La Pop",
                            Information = "un album du chanteur Ed Sheeran nommé d'après un symbiole mathématique",
                            Name = "L'addition",
                            Released = 2011
                        },
                        new
                        {
                            Id = 2,
                            Artist = "L'Ed Sheeran",
                            Genre = "La Pop",
                            Information = "un album du chanteur Ed Sheeran nommé d'après un symbiole mathématique",
                            Name = "La multiplication",
                            Released = 2014
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
