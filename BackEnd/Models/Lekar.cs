// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace BackEnd.Models
// {
//     [Table("Lekar")]
//     public class Lekar
//     {
//         [Key]
//         [Column("ID")]
//         public int ID { get; set; }

//         [Column("Ime")]
//         public string Ime { get; set; }

//         [Column("Prezime")]
//         public string Prezime { get; set; }

//         [Column("StrucnaSprema")]
//         public string StruncaSprema { get; set; }

//         public virtual List<Pacijent> DodeljeniPacijenati { get; set; }
//     }
// }