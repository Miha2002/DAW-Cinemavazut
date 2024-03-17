using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW_Cinemavazut.Models
{
    public class UtilizatorAchievement
    {
        [Key]
        public int id_utilizatorachievement { get; set; }
        public int id_utilizator { get; set; }
        public int id_achievement { get; set; }
    }
}
