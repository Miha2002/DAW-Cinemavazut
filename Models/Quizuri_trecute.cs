using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW_Cinemavazut.Models
{
    public class Quizuri_trecute
    {
        [Key]
        public int id_quizuri_trecute { get; set; }
        public int id_quiz { get; set; }
        public int id_utilizator { get; set; }
    }
}
