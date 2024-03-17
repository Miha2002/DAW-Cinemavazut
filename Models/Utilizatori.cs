using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW_Cinemavazut.Models
{
    public class Utilizatori
    {
        [Key]
        public int id_utilizator { get; set; }
        public string nume { get; set; }
        public string prenume { get; set; }
        public string email { get; set; }
        public string parola { get; set; }
        public string? telefon { get; set; }
        public DateTime data_nastere { get ; set; }
        public DateTime data_inscriere { get; set; }
        public int rol { get; set; }
        public int scor {  get; set; }
    }
}
