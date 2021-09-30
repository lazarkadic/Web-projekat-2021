using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    [Table("Klinika")]
    public class Klinika
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        
        [Column("Naziv")]
        public string Naziv { get; set; }

        [Column("BrojSoba")]
        public int BrojSoba { get; set; }
        public virtual List<Soba> Sobe { get; set; }

        public Klinika()
        {
            Sobe = new List<Soba>();
        }

    }
}