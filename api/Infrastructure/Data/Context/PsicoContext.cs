using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using Domain.Entities;

namespace Infrastructure.Data.Context;

public class PsicoContext : DbContext
{
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Availabilitie> Availabilities => Set<Availabilitie>();
    public DbSet<Consent> Consents => Set<Consent>();
    public DbSet<HealthCareProfissional> HealthCareProfissionals => Set<HealthCareProfissional>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<SessionNote> SessionNotes => Set<SessionNote>();
    public DbSet<SessionSchedule> SessionSchedules => Set<SessionSchedule>();
    public DbSet<Speciality> Specialities => Set<Speciality>();
    public DbSet<TermsAcceptance> TermsAcceptances => Set<TermsAcceptance>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Wait> Waits => Set<Wait>();


    public PsicoContext(DbContextOptions<PsicoContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}