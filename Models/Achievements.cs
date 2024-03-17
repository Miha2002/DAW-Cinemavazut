using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW_Cinemavazut.Models
{
    public class Achievements
    {
        [Key]
        public int id_achievement { get; set; }
        public string denumire { get; set; }
        public int categorie_ach { get; set; }

        // 1-9 - categorii filme maybe?
        // 100 - filme
        // 101 - recenzii
        // 102 - quiz

        public int prag { get; set; }
    }
}
