using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW_Cinemavazut.Models
{
    public class FilmCategorie
    {
        [Key]
        public int id_filmcategorie { get; set; }
        public int id_film { get; set; }
        public int id_categorie { get; set; }
    }
}
