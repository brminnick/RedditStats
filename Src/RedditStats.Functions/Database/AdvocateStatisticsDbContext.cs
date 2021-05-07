using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using RedditStats.Common;

namespace RedditStats.Functions
{
    class AdvocateStatisticsDbContext : DbContext
    {
        public AdvocateStatisticsDbContext(DbContextOptions<AdvocateStatisticsDbContext> options) : base(options)
        {
        }

        public DbSet<RedditSubmission> Submissions => Set<RedditSubmission>();

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            GenerateOnUpdate();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            GenerateOnUpdate();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RedditSubmission>().HasKey(x => x.RedditUri);

            modelBuilder.Entity<RedditSubmission>().Property(x => x.CreatedAt).HasValueGenerator<UtcDateTimeGenerator>().ValueGeneratedOnAdd();
            modelBuilder.Entity<RedditSubmission>().Property(x => x.UpdatedAt).HasValueGenerator<UtcDateTimeGenerator>().ValueGeneratedOnAddOrUpdate();
        }

        //https://github.com/dotnet/efcore/issues/19765#issuecomment-617679987
        void GenerateOnUpdate()
        {
            foreach (EntityEntry entityEntry in ChangeTracker.Entries())
            {
                foreach (PropertyEntry propertyEntry in entityEntry.Properties)
                {
                    var property = propertyEntry.Metadata;
                    var valueGeneratorFactory = property.GetValueGeneratorFactory();

                    var generatedOnUpdate = (property.ValueGenerated & ValueGenerated.OnUpdate) == ValueGenerated.OnUpdate;
                    if (!generatedOnUpdate || valueGeneratorFactory is null)
                        continue;

                    var valueGenerator = valueGeneratorFactory.Invoke(property, entityEntry.Metadata);
                    propertyEntry.CurrentValue = valueGenerator.Next(entityEntry);
                }
            }
        }

        class UtcDateTimeGenerator : ValueGenerator<DateTimeOffset>
        {
            public override bool GeneratesTemporaryValues => false;

            public override DateTimeOffset Next(EntityEntry entry)
            {
                if (entry is null)
                    throw new ArgumentNullException(nameof(entry));

                return DateTimeOffset.UtcNow;
            }
        }
    }
}
