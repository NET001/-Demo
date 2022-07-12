using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Query;
using System.Reflection;
using System.Threading;

namespace EF_Core_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Demo2();
            Console.ReadLine();
        }

        static void Demo1()
        {
            using (MyDbContext dbContext = new MyDbContext())
            {
                while (true)
                {
                    var t1 = dbContext.TableTests.ToList();
                    foreach (var item in t1)
                    {
                        Console.WriteLine(item.Name);
                    }
                    Console.WriteLine("----");
                    Thread.Sleep(2000);
                }
            }

        }
        static void Demo2()
        {
            using (MyDbContext dbContext = new MyDbContext())
            {
                var t1 = dbContext.TableTests.Where(t => t.Name.Contains("CJL")).ToSql();
                Console.WriteLine(t1);
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


    static class extend
    {
        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            using (IEnumerator<TEntity> obj = query.Provider.Execute<IEnumerable<TEntity>>(query.Expression).GetEnumerator())
            {
                object obj2 = obj.Private("_relationalCommandCache");
                SelectExpression selectExpression = obj2.Private<SelectExpression>("_selectExpression");
                return obj2.Private<IQuerySqlGeneratorFactory>("_querySqlGeneratorFactory").Create().GetCommand(selectExpression)
                    .CommandText;
            }
        }
        public static object Private(this object obj, string privateField)
        {
            return obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
        }
        public static T Private<T>(this object obj, string privateField)
        {
            return (T)(obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj));
        }
    }
}
