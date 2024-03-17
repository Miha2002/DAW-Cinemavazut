using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW_Cinemavazut.Models
{
    public class Filme_urmatoare
    {
        [Key]
        public int id_urmatoare { get; set; }
        public string? comentariu { get; set; }
        public int id_film { get; set; }
        public int id_utilizator { get; set; }
    }
}
