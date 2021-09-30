using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models
{
    [Table("Soba")]
    public class Soba
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("BrSobe")]
        public int BrSobe { get; set; }

        [Column("BrKreveta")]
        public int BrKreveta { get; set; }
        public virtual List<Pacijent> ListaPacijenata { get; set; }

        [JsonIgnore]
        public Klinika PripadaKlinici { get; set; }

    }
}