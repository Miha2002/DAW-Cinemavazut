using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW_Cinemavazut.Models
{
    public class Recenzii
    {
        [Key]
        public int id_recenzie { get; set; }
        public string? titlu { get; set; }
        public string? comentariu { get; set; }
        public int rating { get; set; }
        public int id_film { get; set; }
        public int id_utilizator { get; set; }
    }
}
