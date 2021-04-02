using Microsoft.EntityFrameworkCore;

namespace EnumIssue.Int
{
    public class IntDbContext : DbContext
    {
        public DbSet<Project> Project { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=intdb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<ListItem>()
                .HasKey(e => new { e.ListTypeId, e.Id });

            builder
                .Entity<ListItem>()
                .HasOne(e => e.ListType)
                .WithMany()
                .HasForeignKey(e => e.ListTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Project>()
                .HasOne(e => e.PhaseListType)
                .WithMany()
                .HasForeignKey(e => new { e.PhaseListTypeId })
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Project>()
                .HasOne(e => e.Phase)
                .WithMany()
                .HasForeignKey(e => new { e.PhaseListTypeId, e.PhaseId })
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Project>()
                .HasOne(e => e.StatusListType)
                .WithMany()
                .HasForeignKey(e => new { e.StatusListTypeId })
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Project>()
                .HasOne(e => e.Status)
                .WithMany()
                .HasForeignKey(e => new { e.StatusListTypeId, e.StatusId })
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public enum EListType
    {
        ProjectPhase,
        ProjectStatus
    }

    public enum EProjectStatus
    {
        NotApplicable = 0,
        InProgress = 1,
        Closed = 2
    }

    public enum EProjectPhase
    {
        Phase1 = 0,
        Phase2 = 1,
        Phase3 = 2
    }

    public class ListType
    {
        public EListType Id { get; set; }

        public string Description { get; set; }
    }

    public class ListItem
    {
        public EListType ListTypeId { get; set; }

        public virtual ListType ListType { get; set; }

        public int Id { get; set; }

        public string Text { get; set; }
    }

    public partial class Project
    {
        public long Id { get; set; }

        public string Name { get; set; }

        // Status

        public EListType StatusListTypeId { get; set; }

        public virtual ListType StatusListType { get; set; }

        public int StatusId { get; set; }

        public virtual ListItem Status { get; set; }

        // Phase

        public EListType PhaseListTypeId { get; set; }

        public virtual ListType PhaseListType { get; set; }

        public int PhaseId { get; set; }

        public virtual ListItem Phase { get; set; }
    }
}