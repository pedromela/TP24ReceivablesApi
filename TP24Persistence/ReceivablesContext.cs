using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TP24Entities.Models;

namespace TP24Entities
{

    public class ReceivablesContext : DbContext
    {
        public ReceivablesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Receivable> Receivables { get; set; }
    }
}