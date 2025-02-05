﻿using AppEmpleados.Models;
using AppEmpleados.Utilities;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleados.DataAccess
{
    public class EmpleadoDbContext: DbContext
    {
        public DbSet<Empleado> Empleados { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDB = $"Filename={ConexionDB.DevolverRuta("empleados.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(col => col.idEmpleado);
                entity.Property(col => col.idEmpleado).IsRequired().ValueGeneratedOnAdd();
            });
        }
    }
}
