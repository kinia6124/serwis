using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Serwis.Models
{
    public enum Typ_urządzenia
    {
        notebook, PC, drukarka, dysk, monitor, tablet, serwer, inne
    }

    public class Request
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Numer Klienta")]
        public int ClientID { get; set; }
        [Display(Name = "Numer pracownika")]
        public int EmployeeID { get; set; }
        [Display(Name = "Typ urządzenia")]
        public Typ_urządzenia? Typ_urządzenia { get; set; }
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Opis usterki")]
        public string Opis_usterki { get; set; }
        [Display(Name = "Uwagi")]
        public string Uwagi { get; set; }
        [Display(Name = "Jest gwarancja?")]
        public bool Czy_gwarancja { get; set; }

        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Repair_status Repair_status { get; set; }
    }
}