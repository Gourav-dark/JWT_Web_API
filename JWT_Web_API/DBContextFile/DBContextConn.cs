using JWT_Web_API.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT_Web_API.DBContextFile
{
    public class DBContextConn:DbContext
    {
        public DBContextConn(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Item> Items { get; set; }
    }
}
