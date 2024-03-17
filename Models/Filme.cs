using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW_Cinemavazut.Models
{
    public class Filme
    {
        [Key]
        public int id_film { get; set; }
        public string? titlu { get; set; }
        public int? an_lansare { get; set; }
        public string? descriere { get; set; }
        public string? studio { get; set; }
        public int durata { get; set; }
    }
}
