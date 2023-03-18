using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class SqliteDataContext : DataContext
    {
        public SqliteDataContext(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_configuration.GetConnectionString("DefaultConnection")).UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()));
        }
    }
}
