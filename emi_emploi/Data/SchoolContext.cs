using Microsoft.EntityFrameworkCore;
using emi_emploi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace emi_emploi.Data
{
    public class SchoolContext : IdentityDbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Bloc> Blocs { get; set; }
        public DbSet<Disponibilite> Disponibilites { get; set; }
        public DbSet<Enseignant> Enseignants { get; set; }
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Filiere> Filieres { get; set; }
        public DbSet<Groupe> Groupes { get; set; }
        public DbSet<ListFiliere> ListFilieres { get; set; }
        public DbSet<Matiere> Matieres { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Salle> Salles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bloc>().ToTable("Bloc");
            modelBuilder.Entity<Disponibilite>().ToTable("Disponibilite");
            modelBuilder.Entity<Enseignant>().ToTable("Enseignant");
            modelBuilder.Entity<Etudiant>().ToTable("Etudiant");
            modelBuilder.Entity<Filiere>().ToTable("Filiere");
            modelBuilder.Entity<Groupe>().ToTable("Groupe");
            modelBuilder.Entity<ListFiliere>().ToTable("ListFiliere");
            modelBuilder.Entity<Matiere>().ToTable("Matiere");
            modelBuilder.Entity<Promotion>().ToTable("Promotion");
            modelBuilder.Entity<Salle>().ToTable("Salle");
        }
    }
}