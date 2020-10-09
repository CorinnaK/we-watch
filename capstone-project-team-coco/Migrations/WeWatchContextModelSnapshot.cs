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

                    b.Property<short>("TotalSeasons")
                        .HasColumnName("TotalSeasons")
                        .HasColumnType("smallint(2)");

                    b.Property<int>("UserID")
                        .HasColumnName("UserID")
                        .HasColumnType("int(10)");

                    b.HasKey("ShowID");

                    b.HasIndex("UserID")
                        .HasName("FK_Show_User");

                    b.ToTable("show");
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

                    b.Property<string>("Platform")
                        .HasColumnName("Platform")
                        .HasColumnType("varchar(20)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<int>("ShowID")
                        .HasColumnName("ShowID")
                        .HasColumnType("int(10)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnName("Status")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("WatcherID")
                        .HasColumnName("WatcherID")
                        .HasColumnType("int(10)");

                    b.HasKey("ShowCardID");

                    b.HasIndex("ShowID")
                        .HasName("FK_ShowCard_Show");

                    b.HasIndex("WatcherID")
                        .HasName("FK_ShowCard_Watcher");

                    b.ToTable("ShowCard");
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
                        .HasColumnType("varchar(32)")
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
                            HashPassword = "2334814362998297759587574090140267323532918138392977707124924545",
                            Salt = "Yes"
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

                    b.HasIndex("ShowCardID")
                        .HasName("FK_WatchHistory_ShowCard");

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

                    b.Property<int>("UserID")
                        .HasColumnName("UserID")
                        .HasColumnType("int(10)");

                    b.HasKey("WatcherID");

                    b.HasIndex("UserID")
                        .HasName("FK_Watcher_User");

                    b.ToTable("Watcher");
                });

            modelBuilder.Entity("we_watch.Models.Show", b =>
                {
                    b.HasOne("we_watch.Models.User", "User")
                        .WithMany("Shows")
                        .HasForeignKey("UserID")
                        .HasConstraintName("FK_Show_User")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("we_watch.Models.ShowCard", b =>
                {
                    b.HasOne("we_watch.Models.Show", "Show")
                        .WithMany("ShowCards")
                        .HasForeignKey("ShowID")
                        .HasConstraintName("FK_ShowCard_Show")
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
                        .HasConstraintName("FK_WatchHistory_ShowCard")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("we_watch.Models.Watcher", b =>
                {
                    b.HasOne("we_watch.Models.User", "User")
                        .WithMany("Watchers")
                        .HasForeignKey("UserID")
                        .HasConstraintName("FK_Watcher_User")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
