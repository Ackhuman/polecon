using Microsoft.EntityFrameworkCore;
using polecon.service.Models;

namespace polecon.service
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DataPoint> DataPoint { get; set; }
        public virtual DbSet<DataSet> DataSet { get; set; }
        public virtual DbSet<DataSetSource> DataSetSource { get; set; }
        public virtual DbSet<Observation> Observation { get; set; }
        public virtual DbSet<ObservationSeries> ObservationSeries { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<Source> Source { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQL2017;Database=polecon;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataPoint>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.DataSet)
                    .WithMany(p => p.DataPoint)
                    .HasForeignKey(d => d.DataSetId)
                    .HasConstraintName("FK_DataPoint_DataSet");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.DataPoint)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_DataPoint_Unit");
            });

            modelBuilder.Entity<DataSet>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<DataSetSource>(entity =>
            {
                entity.HasKey(e => new { e.DataSetId, e.SourceId });

                entity.HasOne(d => d.DataSet)
                    .WithMany(p => p.DataSetSource)
                    .HasForeignKey(d => d.DataSetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DataSetSource_DataSet");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.DataSetSource)
                    .HasForeignKey(d => d.SourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DataSetSource_Source");
            });

            modelBuilder.Entity<Observation>(entity =>
            {
                entity.HasOne(d => d.DataPoint)
                    .WithMany(p => p.Observation)
                    .HasForeignKey(d => d.DataPointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Observation_DataPoint");
            });

            modelBuilder.Entity<ObservationSeries>(entity =>
            {
                entity.HasKey(e => new { e.DataPointId, e.SeriesId });

                entity.HasOne(d => d.DataPoint)
                    .WithMany(p => p.ObservationSeries)
                    .HasForeignKey(d => d.DataPointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservationSeries_DataPoint");

                entity.HasOne(d => d.Series)
                    .WithMany(p => p.ObservationSeries)
                    .HasForeignKey(d => d.SeriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservationSeries_Series");
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Href).IsUnicode(false);

                entity.Property(e => e.IsPrimary).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FormulaJson).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });
        }
    }
}
