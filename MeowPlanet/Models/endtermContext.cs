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
        public virtual DbSet<Clue> Clues { get; set; } = null!;
        public virtual DbSet<Favorite> Favorites { get; set; } = null!;
        public virtual DbSet<Feature> Features { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Missing> Missings { get; set; } = null!;
        public virtual DbSet<Orderlist> Orderlists { get; set; } = null!;
        public virtual DbSet<Sitter> Sitters { get; set; } = null!;
        public virtual DbSet<SitterFeature> SitterFeatures { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=114.35.243.117,49172;Initial Catalog=endterm;Persist Security Info=True;User ID=endterm;Password=iii-fourth");
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

                entity.Property(e => e.Img01)
                    .HasMaxLength(100)
                    .HasColumnName("img_01")
                    .HasDefaultValueSql("(N'../images/defaultcat.png')");

                entity.Property(e => e.Img02)
                    .HasMaxLength(100)
                    .HasColumnName("img_02");

                entity.Property(e => e.Img03)
                    .HasMaxLength(100)
                    .HasColumnName("img_03");

                entity.Property(e => e.Img04)
                    .HasMaxLength(100)
                    .HasColumnName("img_04");

                entity.Property(e => e.Img05)
                    .HasMaxLength(100)
                    .HasColumnName("img_05");

                entity.Property(e => e.Introduce).HasColumnName("introduce");

                entity.Property(e => e.IsAdoptable).HasColumnName("is_adoptable");

                entity.Property(e => e.IsMissing).HasColumnName("is_missing");

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

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.ServiceId });

                entity.ToTable("favorite");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_favorite_member");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_favorite_sitter");
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.ToTable("feature");

                entity.Property(e => e.FeatureId)
                    .ValueGeneratedNever()
                    .HasColumnName("feature_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
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

                entity.Property(e => e.Photo)
                    .HasMaxLength(100)
                    .HasColumnName("photo")
                    .HasDefaultValueSql("(N'../images/defaultperson.png')");
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

                entity.Property(e => e.IsFound).HasColumnName("is_found");

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

                entity.Property(e => e.House)
                    .HasMaxLength(50)
                    .HasColumnName("house");

                entity.Property(e => e.Img01)
                    .HasMaxLength(100)
                    .HasColumnName("img_01");

                entity.Property(e => e.Img02)
                    .HasMaxLength(100)
                    .HasColumnName("img_02");

                entity.Property(e => e.Img03)
                    .HasMaxLength(100)
                    .HasColumnName("img_03");

                entity.Property(e => e.Img04)
                    .HasMaxLength(100)
                    .HasColumnName("img_04");

                entity.Property(e => e.Img05)
                    .HasMaxLength(100)
                    .HasColumnName("img_05");

                entity.Property(e => e.IsService).HasColumnName("is_service");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Outdoor)
                    .HasMaxLength(50)
                    .HasColumnName("outdoor");

                entity.Property(e => e.Pay).HasColumnName("pay");

                entity.Property(e => e.PosLat)
                    .HasColumnType("decimal(10, 8)")
                    .HasColumnName("pos_lat");

                entity.Property(e => e.PosLng)
                    .HasColumnType("decimal(11, 8)")
                    .HasColumnName("pos_lng");

                entity.Property(e => e.Sleep)
                    .HasMaxLength(50)
                    .HasColumnName("sleep");

                entity.Property(e => e.Summary).HasColumnName("summary");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Sitters)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sitter_member");
            });

            modelBuilder.Entity<SitterFeature>(entity =>
            {
                entity.HasKey(e => new { e.ServiceId, e.FeatureId });

                entity.ToTable("sitter_feature");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.Property(e => e.FeatureId).HasColumnName("feature_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.SitterFeatures)
                    .HasForeignKey(d => d.FeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sitter_feature_feature");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.SitterFeatures)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sitter_feature_sitter");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
