using AplicatioDDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDDD.DAL.Context
{
    public class AppDB:DbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employe> Employers { get; set; }
        public DbSet<Departamen> departamens { get; set; }


        public AppDB(DbContextOptions<AppDB> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder model)//тут настраиваем информацию для базы данных
        {
            base.OnModelCreating(model);
            model.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
            model.Entity<Employe>().Property(p=>p.Salary).HasPrecision(18, 2);
        }
    }
}
