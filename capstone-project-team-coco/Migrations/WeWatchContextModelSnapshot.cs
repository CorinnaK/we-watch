﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using we_watch.Models;

namespace we_watch.Migrations
{
    [DbContext(typeof(WeWatchContext))]
    partial class WeWatchContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("we_watch.Models.Show", b =>
                {
                    b.Property<int>("ShowID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ShowID")
                        .HasColumnType("int(10)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("Title")
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.HasKey("ShowID");

                    b.ToTable("show");

                    b.HasData(
                        new
                        {
                            ShowID = -1,
                            Title = "Game of Thrones"
                        },
                        new
                        {
                            ShowID = -2,
                            Title = "American Idol"
                        },
                        new
                        {
                            ShowID = -3,
                            Title = "Fringe"
                        });
                });

            modelBuilder.Entity("we_watch.Models.ShowCard", b =>
                {
                    b.Property<int>("ShowCardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ShowCardID")
                        .HasColumnType("int(10)");

                    b.Property<short>("CurrentEpisode")
                        .HasColumnName("CurrentEpisode")
                        .HasColumnType("smallint(2)");

                    b.Property<short>("CurrentSeason")
                        .HasColumnName("CurrentSeason")
                        .HasColumnType("smallint(2)");

                    b.Property<int>("ShowID")
                        .HasColumnName("ShowID")
                        .HasColumnType("int(10)");

                    b.Property<int>("UserID")
                        .HasColumnName("UserID")
                        .HasColumnType("int(10)");

                    b.Property<int>("WatcherID")
                        .HasColumnName("WatcherID")
                        .HasColumnType("int(10)");

                    b.HasKey("ShowCardID");

                    b.HasIndex("ShowID")
                        .HasName("FK_ShowCard_Show");

                    b.HasIndex("UserID");

                    b.HasIndex("WatcherID")
                        .HasName("FK_ShowCard_Watcher");

                    b.ToTable("ShowCard");

                    b.HasData(
                        new
                        {
                            ShowCardID = -1,
                            CurrentEpisode = (short)1,
                            CurrentSeason = (short)-2,
                            ShowID = -1,
                            UserID = -3,
                            WatcherID = -2
                        },
                        new
                        {
                            ShowCardID = -2,
                            CurrentEpisode = (short)10,
                            CurrentSeason = (short)-2,
                            ShowID = -1,
                            UserID = -3,
                            WatcherID = -3
                        },
                        new
                        {
                            ShowCardID = -3,
                            CurrentEpisode = (short)12,
                            CurrentSeason = (short)-4,
                            ShowID = -2,
                            UserID = -3,
                            WatcherID = -3
                        },
                        new
                        {
                            ShowCardID = -4,
                            CurrentEpisode = (short)30,
                            CurrentSeason = (short)-5,
                            ShowID = -2,
                            UserID = -3,
                            WatcherID = -1
                        });
                });

            modelBuilder.Entity("we_watch.Models.ShowSeason", b =>
                {
                    b.Property<int>("ShowSeasonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ShowSeasonID")
                        .HasColumnType("int(10)");

                    b.Property<short>("IndividualSeason")
                        .HasColumnName("IndividualSeason")
                        .HasColumnType("smallint(2)");

                    b.Property<short>("SeasonEpisodes")
                        .HasColumnName("SeasonEpisodes")
                        .HasColumnType("smallint(2)");

                    b.Property<int>("ShowID")
                        .HasColumnName("ShowID")
                        .HasColumnType("int(10)");

                    b.HasKey("ShowSeasonID");

                    b.HasIndex("ShowID")
                        .HasName("FK_ShowSeason_Show");

                    b.ToTable("ShowSeason");

                    b.HasData(
                        new
                        {
                            ShowSeasonID = -1,
                            IndividualSeason = (short)1,
                            SeasonEpisodes = (short)10,
                            ShowID = -1
                        },
                        new
                        {
                            ShowSeasonID = -2,
                            IndividualSeason = (short)2,
                            SeasonEpisodes = (short)10,
                            ShowID = -1
                        },
                        new
                        {
                            ShowSeasonID = -3,
                            IndividualSeason = (short)3,
                            SeasonEpisodes = (short)10,
                            ShowID = -1
                        },
                        new
                        {
                            ShowSeasonID = -4,
                            IndividualSeason = (short)1,
                            SeasonEpisodes = (short)25,
                            ShowID = -2
                        },
                        new
                        {
                            ShowSeasonID = -5,
                            IndividualSeason = (short)8,
                            SeasonEpisodes = (short)40,
                            ShowID = -2
                        },
                        new
                        {
                            ShowSeasonID = -6,
                            IndividualSeason = (short)5,
                            SeasonEpisodes = (short)13,
                            ShowID = -3
                        });
                });

            modelBuilder.Entity("we_watch.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserID")
                        .HasColumnType("int(10)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasColumnType("varchar(30)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnName("HashPassword")
                        .HasColumnType("char(64)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnName("Salt")
                        .HasColumnType("varchar(10)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.HasKey("UserID");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserID = -1,
                            Email = "someone@somewhere.something",
                            HashPassword = "2334814362998297759587574090140267323532918138392977707124924545",
                            Salt = "wherew"
                        },
                        new
                        {
                            UserID = -2,
                            Email = "aspin@go.com",
                            HashPassword = "2334814362998297759587574090140267323532918138392977707124924545",
                            Salt = "Yes"
                        },
                        new
                        {
                            UserID = -3,
                            Email = "goaspin@gmail.com",
                            HashPassword = "3/H/c1lljJe2l9+DQCsr3NSSPhFyj/SZV7hA5wUQxnI=",
                            Salt = "1859530424"
                        });
                });

            modelBuilder.Entity("we_watch.Models.WatchHistory", b =>
                {
                    b.Property<int>("WatchHistoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("WatchHistoryID")
                        .HasColumnType("int(10)");

                    b.Property<sbyte>("Episode_Num")
                        .HasColumnName("EpisodeNum")
                        .HasColumnType("tinyint(2)");

                    b.Property<string>("Platform")
                        .HasColumnName("Platform")
                        .HasColumnType("varchar(20)");

                    b.Property<sbyte>("SeasonNum")
                        .HasColumnName("SeasonNum")
                        .HasColumnType("tinyint(2)");

                    b.Property<int>("ShowCardID")
                        .HasColumnName("ShowCardID")
                        .HasColumnType("int(10)");

                    b.HasKey("WatchHistoryID");

                    b.HasIndex("ShowCardID");

                    b.ToTable("WatchHistory");
                });

            modelBuilder.Entity("we_watch.Models.Watcher", b =>
                {
                    b.Property<int>("WatcherID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("WatcherID")
                        .HasColumnType("int(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("varchar(30)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.HasKey("WatcherID");

                    b.ToTable("Watcher");

                    b.HasData(
                        new
                        {
                            WatcherID = -1,
                            Name = "Bob Jones"
                        },
                        new
                        {
                            WatcherID = -2,
                            Name = "Sally Jenkins"
                        },
                        new
                        {
                            WatcherID = -3,
                            Name = "Austin Jane"
                        });
                });

            modelBuilder.Entity("we_watch.Models.ShowCard", b =>
                {
                    b.HasOne("we_watch.Models.Show", "Show")
                        .WithMany("ShowCards")
                        .HasForeignKey("ShowID")
                        .HasConstraintName("FK_ShowCard_Show")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("we_watch.Models.User", "User")
                        .WithMany("ShowCards")
                        .HasForeignKey("UserID")
                        .HasConstraintName("FK_ShowCard_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("we_watch.Models.Watcher", "Watcher")
                        .WithMany("ShowCards")
                        .HasForeignKey("WatcherID")
                        .HasConstraintName("FK_ShowCard_Watcher")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("we_watch.Models.ShowSeason", b =>
                {
                    b.HasOne("we_watch.Models.Show", "Show")
                        .WithMany("ShowSeasons")
                        .HasForeignKey("ShowID")
                        .HasConstraintName("FK_ShowSeason_Show")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("we_watch.Models.WatchHistory", b =>
                {
                    b.HasOne("we_watch.Models.ShowCard", "ShowCard")
                        .WithMany("WatchHistories")
                        .HasForeignKey("ShowCardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
