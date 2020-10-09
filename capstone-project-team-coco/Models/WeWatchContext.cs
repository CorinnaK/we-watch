using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using we_watch.Models;

namespace we_watch.Models
{
    public partial class WeWatchContext : DbContext
    {
        public WeWatchContext()
        {
        }

        public WeWatchContext(DbContextOptions<WeWatchContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Show> Show { get; set; }
        public virtual DbSet<ShowCard> ShowCard { get; set; }
        public virtual DbSet<ShowSeason> ShowSeason { get; set; }
        public virtual DbSet<Watcher> Watcher { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=We_Watch", x => x.ServerVersion("10.4.13-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Show>(entity =>
            {
                entity.Property(e => e.Title)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.UserID)
                    .HasName("FK_" + nameof(Show) + "_" + nameof(User));


                // Always in the one with the foreign key.
                entity.HasOne(child => child.User)
                    .WithMany(parent => parent.Shows)
                    .HasForeignKey(child => child.UserID)

                    // Currently set to restrict until CRUD functionality on USER page
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_" + nameof(Show) + "_" + nameof(User));
            });


            modelBuilder.Entity<ShowCard>(entity =>
            {
                entity.Property(e => e.Platform)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");


                entity.HasIndex(e => e.ShowID)
                // FK Child + Parent
                    .HasName("FK_" + nameof(ShowCard) + "_" + nameof(Show));


                // Always in the one with the foreign key.
                entity.HasOne(child => child.Show)
                    .WithMany(parent => parent.ShowCards)
                    .HasForeignKey(child => child.ShowID)

                    // Currently set to restrict until CRUD functionality on USER page
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_" + nameof(ShowCard) + "_" + nameof(Show));


                entity.HasIndex(e => e.WatcherID)
                // FK Child (many) + Parent (one)
                    .HasName("FK_" + nameof(ShowCard) + "_" + nameof(Watcher));

                // Always in the one with the foreign key.
                entity.HasOne(child => child.Watcher)
                    .WithMany(parent => parent.ShowCards)
                    .HasForeignKey(child => child.WatcherID)

                    // Currently set to restrict until CRUD functionality on USER page
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_" + nameof(ShowCard) + "_" + nameof(Watcher));
            });


            modelBuilder.Entity<ShowSeason>(entity =>
            {
                entity.HasIndex(e => e.ShowID)
                // FK Child (many) + Parent (one)
                    .HasName("FK_" + nameof(ShowSeason) + "_" + nameof(Show));

                // Always in the one with the foreign key.
                entity.HasOne(child => child.Show)
                    .WithMany(parent => parent.ShowSeasons)
                    .HasForeignKey(child => child.ShowID)

                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_" + nameof(ShowSeason) + "_" + nameof(Show));

            });


            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Salt)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.HashPassword)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasData(
                    new User()
                    {
                        UserID = -1,
                        Email = "someone@somewhere.something",
                        Salt= "wherew",
                        HashPassword = "2334814362998297759587574090140267323532918138392977707124924545"
                    },
                    new User()
                    {
                        UserID = -2,
                        Email = "aspin@go.com",
                        Salt= "Yes",
                        HashPassword = "2334814362998297759587574090140267323532918138392977707124924545"
                    },
                    new User()
                    {
                        UserID = -3,
                        Email = "goaspin@gmail.com",
                        Salt= "Yes",
                        HashPassword = "2334814362998297759587574090140267323532918138392977707124924545"
                    }

                    );


            });

            modelBuilder.Entity<Watcher>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.UserID)
                // FK Child (many) + Parent (one)
                    .HasName("FK_" + nameof(Watcher) + "_" + nameof(User));

                // Always in the one with the foreign key.
                entity.HasOne(child => child.User)
                    .WithMany(parent => parent.Watchers)
                    .HasForeignKey(child => child.UserID)
                    
                    // Currently set to restrict until CRUD functionality on USER page
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_" + nameof(Watcher) + "_" + nameof(User));

            });

            modelBuilder.Entity<WatchHistory>(entity =>
            {
                entity.HasIndex(e => e.ShowCardID)
                // FK Child (many) + Parent (one)
                .HasName("FK_" + nameof(WatchHistory) + "_" + nameof(ShowCard));

                // Always in the one with the foreign key.
                entity.HasOne(child => child.ShowCard)
                    .WithMany(parent => parent.WatchHistories)
                    .HasForeignKey(child => child.ShowCardID)

                    // Currently set to restrict until CRUD functionality on USER page
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_" + nameof(WatchHistory) + "_" + nameof(ShowCard));
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}