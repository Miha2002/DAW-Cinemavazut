using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW_Cinemavazut.Models
{
    public class Quizuri
    {
        [Key]
        public int id_quiz { get; set; }
        public int id_film { get; set; }

        public string intrebare1 { get; set; }
        public string raspuns1_corect { get; set; }
        public string raspuns1_gresit1 { get; set; }
        public string? raspuns1_gresit2 { get; set; }

        public string intrebare2 { get; set; }
        public string raspuns2_corect { get; set; }
        public string raspuns2_gresit1 { get; set; }
        public string? raspuns2_gresit2 { get; set; }

        public string? intrebare3 { get; set; }
        public string? raspuns3_corect { get; set; }
        public string? raspuns3_gresit1 { get; set; }
        public string? raspuns3_gresit2 { get; set; }

        public string? intrebare4 { get; set; }
        public string? raspuns4_corect { get; set; }
        public string? raspuns4_gresit1 { get; set; }
        public string? raspuns4_gresit2 { get; set; }

        public string? intrebare5 { get; set; }
        public string? raspuns5_corect { get; set; }
        public string? raspuns5_gresit1 { get; set; }
        public string? raspuns5_gresit2 { get; set; }
    }
}
