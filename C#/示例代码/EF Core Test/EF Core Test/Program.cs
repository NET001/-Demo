using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;

namespace EF_Core_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (MyDbContext dbContext = new MyDbContext())
            {
                while (true)
                {
                    var t1 = dbContext.TableTests.AsNoTracking().ToList();
                    foreach (var item in t1)
                    {
                        Console.WriteLine(item.Name);
                    }
                    Console.WriteLine("----");
                    Thread.Sleep(2000);
                }
            }
        }
    }
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
            //初始化数据库
            Database.EnsureCreated();
        }
        public DbSet<TableTest> TableTests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TableTest>()
                .ToTable("TableTest")
                .HasKey(t => t.Id);
            modelBuilder.Entity<TableTest>()
                .HasData(new TableTest()
                {
                    Id = 1,
                    Name = "名称1"
                });
            modelBuilder.Entity<TableTest>()
                .HasData(new TableTest()
                {
                    Id = 2,
                    Name = "名称2"
                });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=127.0.0.1;Port=3306;Database=TableTestDb;User=root;Password=123456;");
        }
    }
    public class TableTest
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
