using SalahProjekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SalahProjekt.DAL
{
    public class TheRedsContext: DbContext
    {
        public TheRedsContext() : base("DefaultConnection")
        {

        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Artykul> Artykuly { get; set; }
        public DbSet<Typ> Typy { get; set; }
        public DbSet<Mecz> Mecze { get; set; }
        public DbSet<Komentarz> Komentarze { get; set; }
        public DbSet<Druzyna> Druzyny { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Komentarz>().HasRequired<Profile>(k => k.Profil)
                .WithMany(p => p.Pkomentarze)
                .HasForeignKey(k => k.ProfileID).WillCascadeOnDelete(false);

            modelBuilder.Entity<Mecz>().HasRequired<Druzyna>(m => m.druzyna1)
                .WithMany(d => d.Lista_Meczy_Gospodarz)
                .HasForeignKey(m => m.Druzyna1ID).WillCascadeOnDelete(false);

            modelBuilder.Entity<Mecz>().HasRequired<Druzyna>(m => m.druzyna2)
               .WithMany(d => d.Lista_Meczy_Goscie)
               .HasForeignKey(m => m.Druzyna2ID).WillCascadeOnDelete(false);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}