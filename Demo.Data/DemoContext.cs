using Demo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;

namespace Demo.Data
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .UseLoggerFactory(ConsoleLoggerFactory)//Show generated Command
        //        .EnableSensitiveDataLogging()
        //        .UseSqlServer(
        //        "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Demo");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GamePlayer>().HasKey(x => new { x.PlayerId, x.GameId });
            modelBuilder.Entity<Resume>()
                    .HasOne(x => x.Player)
                    .WithOne(x => x.Resume)
                    .HasForeignKey<Resume>(x => x.PlayerId);
        }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Game> Games { get; set; }

        public static readonly ILoggerFactory ConsoleLoggerFactory =
            LoggerFactory.Create(builder =>
            {
                builder.AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name &&
                level == LogLevel.Information)
                .AddConsole();
            });
    }
}
