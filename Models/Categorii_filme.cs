using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW_Cinemavazut.Models
{
    public class Categorii_filme
    {
        [Key]
        public int id_categorie { get; set; }
        public string denumire { get; set; }
    }
}
