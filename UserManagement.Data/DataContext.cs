﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseInMemoryDatabase("UserManagement.Data.DataContext");

    protected override void OnModelCreating(ModelBuilder model)
        => model.Entity<User>().HasData(new[]
        {
            new User { Id = 1, Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", IsActive = true, DateOfBirth = new DateTime(1999,03,27) },
            new User { Id = 2, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", IsActive = true, DateOfBirth = new DateTime(1998,05,03) },
            new User { Id = 3, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", IsActive = false, DateOfBirth = new DateTime(1997,12,14) },
            new User { Id = 4, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", IsActive = true, DateOfBirth = new DateTime(1996,02,09) },
            new User { Id = 5, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", IsActive = true, DateOfBirth = new DateTime(1995,08,30) },
            new User { Id = 6, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", IsActive = true, DateOfBirth = new DateTime(1994,07,18) },
            new User { Id = 7, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", IsActive = false, DateOfBirth = new DateTime(1993,09,02) },
            new User { Id = 8, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", IsActive = false, DateOfBirth = new DateTime(1992,11,29) },
            new User { Id = 9, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", IsActive = false, DateOfBirth = new DateTime(1991,01,17) },
            new User { Id = 10, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", IsActive = true, DateOfBirth = new DateTime(1990,06,20) },
            new User { Id = 11, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", IsActive = true, DateOfBirth = new DateTime(1989,03,05) },
        });

    public DbSet<User>? Users { get; set; }

    public DbSet<Log>? Logs { get; set; }

    public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        => base.Set<TEntity>();

    public void Create<TEntity>(TEntity entity) where TEntity : class
    {
        base.Add(entity);
        SaveChanges();
    }

    public new void Update<TEntity>(TEntity entity) where TEntity : class
    {
        base.Update(entity);
        SaveChanges();
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : class
    {
        base.Remove(entity);
        SaveChanges();
    }
}
