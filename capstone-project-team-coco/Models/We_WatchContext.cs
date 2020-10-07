using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using we_watch.Models;

namespace we_watch.Models
{
    public partial class We_WatchContext : DbContext
    {
        public We_WatchContext()
        {
        }

        public We_WatchContext(DbContextOptions<We_WatchContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TheUser> TheUser { get; set; }


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
                    .HasName("FK_" + nameof(Show) + "_" + nameof(TheUser));


                // Always in the one with the foreign key.
                entity.HasOne(child => child.TheUser)
                    .WithMany(parent => parent.Shows)
                    .HasForeignKey(child => child.UserID)

                    // Currently set to restrict until CRUD functionality on USER page
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_" + nameof(Show) + "_" + nameof(TheUser));
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


            modelBuilder.Entity<TheUser>(entity =>
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

            });

            modelBuilder.Entity<Watcher>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.UserID)
                // FK Child (many) + Parent (one)
                    .HasName("FK_" + nameof(Watcher) + "_" + nameof(TheUser));

                // Always in the one with the foreign key.
                entity.HasOne(child => child.TheUser)
                    .WithMany(parent => parent.Watchers)
                    .HasForeignKey(child => child.UserID)
                    
                    // Currently set to restrict until CRUD functionality on USER page
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_" + nameof(Watcher) + "_" + nameof(TheUser));

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
                    .HasConstraintName("FK_" + nameof(ShowSeason) + "_" + nameof(Show));
            });

            /* modelBuilder.Entity<PhoneNumber>(entity =>
             {
                 entity.HasIndex(e => e.PersonID)
                     .HasName("FK_" + nameof(PhoneNumber) + "_" + nameof(Person));

                 entity.Property(e => e.Number)
                     .HasCharSet("utf8mb4")
                     .HasCollation("utf8mb4_general_ci");

                 // Always in the one with the foreign key.
                 entity.HasOne(child => child.Person)
                     .WithMany(parent => parent.PhoneNumbers)
                     .HasForeignKey(child => child.PersonID)
                     // When we delete a record, we can modify the behaviour of the case where there are child records.
                     // Restrict: Throw an exception if we try to orphan a child record.
                     // Cascade: Remove any child records that would be orphaned by a removed parent.
                     // SetNull: Set the foreign key field to null on any orphaned child records.
                     // NoAction: Don't commit any deletions of parents which would orphan a child.
                     .OnDelete(DeleteBehavior.Cascade)
                     .HasConstraintName("FK_" + nameof(PhoneNumber) + "_" + nameof(Person));

             });*/

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}