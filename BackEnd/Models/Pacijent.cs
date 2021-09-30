using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models
{
    [Table("Pacijent")]
    public class Pacijent
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Ime")]
        public string Ime { get; set; }

        [Column("Prezime")]
        public string Prezime { get; set; }

        [Column("JMBG")]
        [MaxLength(13)]
        public string JMBG { get; set; }

        [Column("Bolest")]
        public string Bolest { get; set; }

        [Column("Stanje")]
        public string Stanje { get; set; }

        [JsonIgnore]
        public Soba BrojSobe { get; set; }

        // [JsonIgnore]
        // public Lekar IzabraniLekar { get; set; }
    }
}