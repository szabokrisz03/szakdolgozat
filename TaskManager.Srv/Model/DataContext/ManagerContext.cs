using Microsoft.EntityFrameworkCore;

using TaskManager.Srv.Model.DataModel;
using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Model.DataContext;

public class ManagerContext : DbContext
{
    public DbSet<Project> Project { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<ProjectUser> ProjectUser { get; set; }
    public DbSet<WiLinkTemplate> WiLinkTemplate { get; set; }
    public DbSet<ProjectTask> ProjectTask { get; set; }

    public ManagerContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var projectUser = modelBuilder.Entity<ProjectUser>();
        projectUser.HasKey(pu => new { pu.ProjectId, pu.UserId });
        projectUser.HasOne(pu => pu.Project).WithMany(p => p.ProjectUsers).HasForeignKey(pu => pu.ProjectId);
        projectUser.HasOne(pu => pu.User).WithMany(p => p.ProjectUsers).HasForeignKey(pu => pu.UserId);
        projectUser.HasIndex(pu => pu.LastVisit);

        var project = modelBuilder.Entity<Project>();
        project.HasMany(p => p.WiLinkTemplates).WithOne(p => p.Project).HasForeignKey(t => t.ProjectId);
        project.HasIndex(p => p.TechnicalName).IsUnique(true);
        project.HasIndex(p => p.Name);
        project.Property(p => p.TechnicalName).HasDefaultValueSql("NEWID()");

        var user = modelBuilder.Entity<User>();
        user.HasIndex(u => u.UserName);

        var projectTask = modelBuilder.Entity<ProjectTask>();
        projectTask.Property(p => p.TechnicalName).HasDefaultValueSql("NEWID()");
        projectTask.HasIndex(p => p.Name);
        project.HasIndex(p => p.TechnicalName);

        base.OnModelCreating(modelBuilder);
    }
}
