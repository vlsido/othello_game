﻿// <auto-generated />
using System;
using DAL.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Db.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221031143740_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("Domain.OthelloGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("GameOverAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("GameWonByPlayer")
                        .HasColumnType("TEXT");

                    b.Property<int>("OthelloOptionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Player1Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<int>("Player1Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Player2Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<int>("Player2Type")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OthelloOptionId");

                    b.ToTable("OthelloGames");
                });

            modelBuilder.Entity("Domain.OthelloGameState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AxisX")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AxisY")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BlackScore")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("OthelloGameId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SerializedGameState")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("WhiteScore")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Winner")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OthelloGameId");

                    b.ToTable("OthelloGamesStates");
                });

            modelBuilder.Entity("Domain.OthelloOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CurrentPlayer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Width")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("OthelloOptions");
                });

            modelBuilder.Entity("Domain.OthelloGame", b =>
                {
                    b.HasOne("Domain.OthelloOption", "OthelloOption")
                        .WithMany("OthelloGames")
                        .HasForeignKey("OthelloOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OthelloOption");
                });

            modelBuilder.Entity("Domain.OthelloGameState", b =>
                {
                    b.HasOne("Domain.OthelloGame", "OthelloGame")
                        .WithMany("OthelloGameStates")
                        .HasForeignKey("OthelloGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OthelloGame");
                });

            modelBuilder.Entity("Domain.OthelloGame", b =>
                {
                    b.Navigation("OthelloGameStates");
                });

            modelBuilder.Entity("Domain.OthelloOption", b =>
                {
                    b.Navigation("OthelloGames");
                });
#pragma warning restore 612, 618
        }
    }
}
