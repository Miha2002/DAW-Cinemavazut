using Proiect_DAW_Cinemavazut.Models;
using Microsoft.EntityFrameworkCore;

namespace Proiect_DAW_Cinemavazut.ContextModels
{
    public class CinemavazutContext : DbContext
    {
        public DbSet<Filme> Filme { get; set; }
        public DbSet<Categorii_filme> Categorii_filme { get; set; }
        public DbSet<Utilizatori> Utilizatori { get; set; }
        public DbSet<Achievements> Achievements { get; set; }
        public DbSet<UtilizatorAchievement> UtilizatorAchievement { get; set; }
        public DbSet<Filme_vazute> Filme_vazute { get; set; }
        public DbSet<Filme_urmatoare> Filme_urmatoare { get; set; }
        public DbSet<Quizuri> Quizuri { get; set; }
        public DbSet<Quizuri_trecute> Quizuri_trecute { get; set; }
        public DbSet<Recenzii> Recenzii { get; set; }
        public DbSet<FilmCategorie> FilmCategorie { get; set; }
        public CinemavazutContext(DbContextOptions options) : base(options) { }
    }
}
