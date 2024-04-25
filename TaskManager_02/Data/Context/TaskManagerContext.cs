using Microsoft.EntityFrameworkCore;
using TaskManager_02.Data.Entity;

namespace TaskManager_02.Data.Context;

public partial class TaskManagerContext : DbContext
{
    public TaskManagerContext()
    {
    }

    public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<ProjectTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("PK__Projects__FFEE7431FFE565E3");

            entity.Property(e => e.ProjectName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Projects__Create__3E52440B");

            entity.Property(e => e.TechnicalName)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("TechnicalName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("PK__Users__6965AB5722265939");

            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("ProjectTaskRowId");

            entity.Property(e => e.TaskName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(e => e.Project).WithMany(p => p.Tasks)
                .HasForeignKey(f => f.ProjectId)
                .HasConstraintName("FK__Tasks__Create__0E2R3W2");
            
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
