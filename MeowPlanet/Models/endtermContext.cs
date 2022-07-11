using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MeowPlanet.Models
{
    public partial class endtermContext : DbContext
    {
        public endtermContext()
        {
        }

        public endtermContext(DbContextOptions<endtermContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adopt> Adopts { get; set; } = null!;
        public virtual DbSet<Cat> Cats { get; set; } = null!;
        public virtual DbSet<CatBreed> CatBreeds { get; set; } = null!;
        public virtual DbSet<CatPicture> CatPictures { get; set; } = null!;
        public virtual DbSet<Clue> Clues { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Missing> Missings { get; set; } = null!;
        public virtual DbSet<Orderlist> Orderlists { get; set; } = null!;
        public virtual DbSet<Sitter> Sitters { get; set; } = null!;
        public virtual DbSet<SitterHouse> SitterHouses { get; set; } = null!;
        public virtual DbSet<SitterIndoor> SitterIndoors { get; set; } = null!;
        public virtual DbSet<SitterOutdoor> SitterOutdoors { get; set; } = null!;
        public virtual DbSet<SitterPicture> SitterPictures { get; set; } = null!;
        public virtual DbSet<SitterSleep> SitterSleeps { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adopt>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.CatId });

                entity.ToTable("adopt");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.CatId).HasColumnName("cat_id");

                entity.Property(e => e.DateOver)
                    .HasColumnType("date")
                    .HasColumnName("date_over");

                entity.Property(e => e.DateStart)
                    .HasColumnType("date")
                    .HasColumnName("date_start");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Adopts)
                    .HasForeignKey(d => d.CatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_adopt_cat");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Adopts)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_adopt_member");
            });

            modelBuilder.Entity<Cat>(entity =>
            {
                entity.ToTable("cat");

                entity.Property(e => e.CatId).HasColumnName("cat_id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.BreedId).HasColumnName("breed_id");

                entity.Property(e => e.Introduce).HasColumnName("introduce");

                entity.Property(e => e.IsAdoptable).HasColumnName("is_adoptable");

                entity.Property(e => e.IsSitting).HasColumnName("is_sitting");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.PosLat)
                    .HasColumnType("decimal(10, 8)")
                    .HasColumnName("pos_lat");

                entity.Property(e => e.PosLng)
                    .HasColumnType("decimal(11, 8)")
                    .HasColumnName("pos_lng");

                entity.Property(e => e.Sex).HasColumnName("sex");

                entity.HasOne(d => d.Breed)
                    .WithMany(p => p.Cats)
                    .HasForeignKey(d => d.BreedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cat_cat_breed");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Cats)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cat_member");
            });

            modelBuilder.Entity<CatBreed>(entity =>
            {
                entity.HasKey(e => e.BreedId);

                entity.ToTable("cat_breed");

                entity.Property(e => e.BreedId).HasColumnName("breed_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CatPicture>(entity =>
            {
                entity.HasKey(e => e.PicId);

                entity.ToTable("cat_picture");

                entity.Property(e => e.PicId).HasColumnName("pic_id");

                entity.Property(e => e.CatId).HasColumnName("cat_id");

                entity.Property(e => e.Path)
                    .HasMaxLength(100)
                    .HasColumnName("path");

                entity.Property(e => e.PicOrder).HasColumnName("pic_order");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.CatPictures)
                    .HasForeignKey(d => d.CatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cat_picture_cat1");
            });

            modelBuilder.Entity<Clue>(entity =>
            {
                entity.ToTable("clue");

                entity.Property(e => e.ClueId).HasColumnName("clue_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .HasColumnName("image_path");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.MissingId).HasColumnName("missing_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.WitnessLat)
                    .HasColumnType("decimal(10, 8)")
                    .HasColumnName("witness_lat");

                entity.Property(e => e.WitnessLng)
                    .HasColumnType("decimal(11, 8)")
                    .HasColumnName("witness_lng");

                entity.Property(e => e.WitnessTime)
                    .HasColumnType("datetime")
                    .HasColumnName("witness_time");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Clues)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_clue_member");

                entity.HasOne(d => d.Missing)
                    .WithMany(p => p.Clues)
                    .HasForeignKey(d => d.MissingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_clue_missing");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("member");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.HasMany(d => d.Services)
                    .WithMany(p => p.Members)
                    .UsingEntity<Dictionary<string, object>>(
                        "Favorite",
                        l => l.HasOne<Sitter>().WithMany().HasForeignKey("ServiceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_favorite_sitter"),
                        r => r.HasOne<Member>().WithMany().HasForeignKey("MemberId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_favorite_member"),
                        j =>
                        {
                            j.HasKey("MemberId", "ServiceId");

                            j.ToTable("favorite");

                            j.IndexerProperty<int>("MemberId").HasColumnName("member_id");

                            j.IndexerProperty<int>("ServiceId").HasColumnName("service_id");
                        });
            });

            modelBuilder.Entity<Missing>(entity =>
            {
                entity.ToTable("missing");

                entity.Property(e => e.MissingId).HasColumnName("missing_id");

                entity.Property(e => e.CatId).HasColumnName("cat_id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.Found).HasColumnName("found");

                entity.Property(e => e.Lat)
                    .HasColumnType("decimal(10, 8)")
                    .HasColumnName("lat");

                entity.Property(e => e.Lng)
                    .HasColumnType("decimal(11, 8)")
                    .HasColumnName("lng");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Missings)
                    .HasForeignKey(d => d.CatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_missing_cat");
            });

            modelBuilder.Entity<Orderlist>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("orderlist");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CatId).HasColumnName("cat_id");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.DateOrder)
                    .HasColumnType("date")
                    .HasColumnName("date_order");

                entity.Property(e => e.DateOver)
                    .HasColumnType("date")
                    .HasColumnName("date_over");

                entity.Property(e => e.DateStart)
                    .HasColumnType("date")
                    .HasColumnName("date_start");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.Property(e => e.Star)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("star");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Orderlists)
                    .HasForeignKey(d => d.CatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orderlist_cat");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orderlists)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orderlist_member");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Orderlists)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orderlist_sitter");
            });

            modelBuilder.Entity<Sitter>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.ToTable("sitter");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.Property(e => e.HouseId).HasColumnName("house_id");

                entity.Property(e => e.IndoorId).HasColumnName("indoor_id");

                entity.Property(e => e.IsService).HasColumnName("is_service");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.OutdoorId).HasColumnName("outdoor_id");

                entity.Property(e => e.Pay).HasColumnName("pay");

                entity.Property(e => e.PosLat)
                    .HasColumnType("decimal(10, 8)")
                    .HasColumnName("pos_lat");

                entity.Property(e => e.PosLng)
                    .HasColumnType("decimal(11, 8)")
                    .HasColumnName("pos_lng");

                entity.Property(e => e.SleepId).HasColumnName("sleep_id");

                entity.Property(e => e.Summary).HasColumnName("summary");

                entity.HasOne(d => d.House)
                    .WithMany(p => p.Sitters)
                    .HasForeignKey(d => d.HouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sitter_sitter_house");

                entity.HasOne(d => d.Indoor)
                    .WithMany(p => p.Sitters)
                    .HasForeignKey(d => d.IndoorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sitter_sitter_indoor");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Sitters)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sitter_member");

                entity.HasOne(d => d.Outdoor)
                    .WithMany(p => p.Sitters)
                    .HasForeignKey(d => d.OutdoorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sitter_sitter_outdoor1");

                entity.HasOne(d => d.Sleep)
                    .WithMany(p => p.Sitters)
                    .HasForeignKey(d => d.SleepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sitter_sitter_sleep");
            });

            modelBuilder.Entity<SitterHouse>(entity =>
            {
                entity.HasKey(e => e.HouseId);

                entity.ToTable("sitter_house");

                entity.Property(e => e.HouseId).HasColumnName("house_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SitterIndoor>(entity =>
            {
                entity.HasKey(e => e.IndoorId);

                entity.ToTable("sitter_indoor");

                entity.Property(e => e.IndoorId)
                    .ValueGeneratedNever()
                    .HasColumnName("indoor_id");

                entity.Property(e => e.Name)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SitterOutdoor>(entity =>
            {
                entity.HasKey(e => e.OutdoorId);

                entity.ToTable("sitter_outdoor");

                entity.Property(e => e.OutdoorId).HasColumnName("outdoor_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SitterPicture>(entity =>
            {
                entity.HasKey(e => e.PicId);

                entity.ToTable("sitter_picture");

                entity.Property(e => e.PicId).HasColumnName("pic_id");

                entity.Property(e => e.Path)
                    .HasMaxLength(100)
                    .HasColumnName("path");

                entity.Property(e => e.PicOrder).HasColumnName("pic_order");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.SitterPictures)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sitter_picture_sitter");
            });

            modelBuilder.Entity<SitterSleep>(entity =>
            {
                entity.HasKey(e => e.SleepId);

                entity.ToTable("sitter_sleep");

                entity.Property(e => e.SleepId).HasColumnName("sleep_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
